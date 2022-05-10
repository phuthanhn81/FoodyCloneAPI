using Foody.Data.Entities;
using Foody.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ViewModels.Models;

namespace Foody.Service.Service
{
    public interface ILoginService
    {
        Task<TokenApiModel> Authenticate(string username, string password);

        Task<TokenApiModel> RefreshToken(TokenApiModel tokenApiModel);

        Task<int> RevokeToken(string username);

        Task<int> SignUp(SignUpRequest request);
    }

    public class LoginService : ILoginService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokenRepository _tokenRepository;

        public LoginService(IUsersRepository usersRepository, ITokenRepository tokenRepository)
        {
            _usersRepository = usersRepository;
            _tokenRepository = tokenRepository;
        }

        public async Task<TokenApiModel> Authenticate(string username, string password)
        {
            TokenApiModel tokenApiModel = new TokenApiModel();

            Users user = await _usersRepository.Authenticate(username, password);
            if (user == null)
            {
                tokenApiModel.AccessToken = null;
                tokenApiModel.RefreshToken = null;
                tokenApiModel.Status = 404;
                return tokenApiModel;
            }
            else
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                };

                string accessToken = _tokenRepository.GenerateAccessToken(claims);
                string refreshToken = _tokenRepository.GenerateRefreshToken();

                int status = await _usersRepository.SecurityToken(user, refreshToken, 86400);

                tokenApiModel.AccessToken = accessToken;
                tokenApiModel.RefreshToken = refreshToken;
                tokenApiModel.Status = status;
            }
            return tokenApiModel;
        }

        public async Task<TokenApiModel> RefreshToken(TokenApiModel tokenApiModel)
        {
            TokenApiModel newTokenApiModel = new TokenApiModel();

            ClaimsPrincipal principal = _tokenRepository.GetPrincipalFromExpiredToken(tokenApiModel.AccessToken);
            string username = principal.Identity.Name;
            Users user = await _usersRepository.FindByUserName(username);

            if (user is null || user.RefreshToken != tokenApiModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                newTokenApiModel.Status = 400;
                return newTokenApiModel;
            }

            string newAccessToken = _tokenRepository.GenerateAccessToken(principal.Claims);
            string newRefreshToken = _tokenRepository.GenerateRefreshToken();

            int status = await _usersRepository.SecurityToken(user, newRefreshToken, 86400);

            newTokenApiModel.AccessToken = newAccessToken;
            newTokenApiModel.RefreshToken = newRefreshToken;
            newTokenApiModel.Status = status;

            return newTokenApiModel;
        }

        public async Task<int> RevokeToken(string username)
        {
            return await _usersRepository.RevokeToken(username);
        }

        public async Task<int> SignUp(SignUpRequest request)
        {
            PasswordHasher<Users> hasher = new PasswordHasher<Users>();

            Users user = new Users();
            user.UserName = request.UserName;
            user.Password = hasher.HashPassword(null, request.Password);
            user.Activated = false;
            user.RefreshTokenExpiryTime = DateTime.Now;
            user.Email = request.Email;

            return await _usersRepository.SignUp(user);
        }
    }
}
