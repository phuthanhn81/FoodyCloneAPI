using System.Threading.Tasks;
using Foody.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IPlaceService _placeService;

        public DataController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet("PlacesByCode")]
        public async Task<IActionResult> GetPlacesByCode(int code)
        {
            var data = await _placeService.GetPlacesByCode(code);
            return Ok(data);
        }
    }
}
