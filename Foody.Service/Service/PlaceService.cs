using Foody.Data.Entities;
using Foody.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foody.Service.Service
{
    public interface IPlaceService
    {
        Task<List<Place>> GetPlacesByCode(int code);
    }

    public class PlaceService : IPlaceService
    {
        private readonly IPlaceRepository _placeRepository;

        public PlaceService(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<List<Place>> GetPlacesByCode(int code)
        {
            return await _placeRepository.GetPlacesByCode(code);
        }
    }
}
