using System.Threading.Tasks;
using Foody.API.Models;
using Foody.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IPlacesService _placeService;
        private readonly IPlaceDishesService _placeDishesService;
        private readonly IOrdersService _ordersService;

        public DataController(IPlacesService placeService, IPlaceDishesService placeDishesService, IOrdersService ordersService)
        {
            _placeService = placeService;
            _placeDishesService = placeDishesService;
            _ordersService = ordersService;
        }

        [HttpGet("PlacesByCode")]
        public async Task<IActionResult> GetPlacesByCode(int code)
        {
            var data = await _placeService.GetPlacesByCode(code);
            return Ok(data);
        }

        [HttpGet("PlaceDishesByPlaceID")]
        public async Task<IActionResult> PlaceDishesByPlaceID(int placeid)
        {
            var data = await _placeDishesService.GetPlaceDishesByPlaceID(placeid);
            return Ok(data);
        }

        [HttpPost("RequestOrders")]
        public async Task<IActionResult> RequestOrders(RequestOrdersModel request)
        {
            var result = await _ordersService.RequestOrders(request.order, request.orderDetail);
            return Ok(result);
        }
    }
}
