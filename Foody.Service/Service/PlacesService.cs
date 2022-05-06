using Foody.Data.Entities;
using Foody.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foody.Service.Service
{
    public interface IPlacesService
    {
        Task<List<Places>> GetPlacesByCode(int code);
    }

    public class PlacesService : IPlacesService
    {
        private readonly IPlacesRepository _placeRepository;

        public PlacesService(IPlacesRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<List<Places>> GetPlacesByCode(int code)
        {
            return await _placeRepository.GetPlacesByCode(code);
        }
    }
}
