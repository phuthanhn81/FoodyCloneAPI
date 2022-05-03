using Foody.Data.EF;
using Foody.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Foody.Data.Repositories
{
    public interface IPlaceRepository
    {
        Task<List<Place>> GetPlacesByCode(int code);
    }

    public class PlaceRepository : IPlaceRepository
    {
        private readonly FoodyContext _context;

        public PlaceRepository(FoodyContext context)
        {
            _context = context;
        }

        public async Task<List<Place>> GetPlacesByCode(int code)
        {
            return await _context.Place.Where(n => n.City == code).ToListAsync();
        }
    }
}
