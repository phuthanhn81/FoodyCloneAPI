using Foody.Data.Entities;
using Foody.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foody.Service.Service
{
    public interface IPlaceDishesService
    {
        Task<List<PlaceDishes>> GetPlaceDishesByPlaceID(int placeid);
    }

    public class PlaceDishesService : IPlaceDishesService
    {
        private readonly IPlaceDishesRepository _placeRepository;

        public PlaceDishesService(IPlaceDishesRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<List<PlaceDishes>> GetPlaceDishesByPlaceID(int placeid)
        {
            return await _placeRepository.GetPlaceDishesByPlaceID(placeid);
        }
    }
}
