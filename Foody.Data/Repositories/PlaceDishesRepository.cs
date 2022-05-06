using Foody.Data.EF;
using Foody.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Foody.Data.Repositories
{
    public interface IPlaceDishesRepository
    {
        Task<List<PlaceDishes>> GetPlaceDishesByPlaceID(int placeid);
    }

    public class PlaceDishesRepository : IPlaceDishesRepository
    {
        private readonly FoodyContext _context;

        public PlaceDishesRepository(FoodyContext context)
        {
            _context = context;
        }

        public async Task<List<PlaceDishes>> GetPlaceDishesByPlaceID(int placeid)
        {
            return await _context.PlaceDishes.Where(n => n.PlaceID == placeid).ToListAsync();
        }
    }
}
