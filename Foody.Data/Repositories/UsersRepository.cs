using Foody.Data.EF;
using Foody.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Foody.Data.Enums;

namespace Foody.Data.Repositories
{
    public interface IUsersRepository
    {
        Task<Users> FindByUserName(string username);

        Task<Users> FindByEmail(string email);

        Task<Users> Authenticate(string username, string password);

        Task<int> SecurityToken(Users user, string refreshtoken, long seconds);

        Task<int> RevokeToken(string username);

        Task<int> SignUp(Users user);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly FoodyContext _context;

        public UsersRepository(FoodyContext context)
        {
            _context = context;
        }

        public async Task<Users> Authenticate(string username, string password)
        {
            Users user = await FindByUserName(username);
            if (user == null) return null;

            PasswordHasher<Users> hasher = new PasswordHasher<Users>();
            PasswordVerificationResult result = hasher.VerifyHashedPassword(user, user.Password, password);
            if (result == PasswordVerificationResult.Failed) return null;

            return user;
        }

        public async Task<Users> FindByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(n => n.Email == email);
        }

        public async Task<Users> FindByUserName(string username)
        {
            Users user = await _context.Users.SingleOrDefaultAsync(n => n.UserName == username);
            return user;
        }

        public async Task<int> RevokeToken(string username)
        {
            Users user = await FindByUserName(username);
            if (user == null) return 400;
            user.RefreshToken = null;
            await _context.SaveChangesAsync();
            return 200;
        }

        public async Task<int> SecurityToken(Users user, string refreshtoken, long seconds)
        {
            try
            {
                user.RefreshToken = refreshtoken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddSeconds(seconds);

                await _context.SaveChangesAsync();

                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        public async Task<int> SignUp(Users user)
        {
            try
            {
                if (await FindByEmail(user.Email) != null)
                {
                    return (int)SignUpStatus.Email;
                }

                if(await FindByUserName(user.UserName) != null)
                {
                    return (int)SignUpStatus.UserName;
                }

                await _context.AddAsync(user);
                await _context.SaveChangesAsync();

                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }
    }
}
