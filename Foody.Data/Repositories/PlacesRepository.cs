using Foody.Data.EF;
using Foody.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Foody.Data.Repositories
{
    public interface IPlacesRepository
    {
        Task<List<Places>> GetPlacesByCode(int code);
    }

    public class PlacesRepository : IPlacesRepository
    {
        private readonly FoodyContext _context;

        public PlacesRepository(FoodyContext context)
        {
            _context = context;
        }

        public async Task<List<Places>> GetPlacesByCode(int code)
        {
            return await _context.Places.Where(n => n.City == code).ToListAsync();
        }
    }
}
