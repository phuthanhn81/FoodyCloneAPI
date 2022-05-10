using System.Threading.Tasks;
using Foody.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Models;

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
        [Authorize]
        public async Task<IActionResult> GetPlacesByCode(int code)
        {
            var data = await _placeService.GetPlacesByCode(code);
            return Ok(data);
        }

        [HttpGet("PlaceDishesByPlaceID")]
        [Authorize]
        public async Task<IActionResult> PlaceDishesByPlaceID(int placeid)
        {
            var data = await _placeDishesService.GetPlaceDishesByPlaceID(placeid);
            return Ok(data);
        }

        [HttpPost("RequestOrders")]
        [Authorize]
        public async Task<IActionResult> RequestOrders(RequestOrdersModel request)
        {
            string username = User.Identity.Name;
            var result = await _ordersService.RequestOrders(request.order, request.orderDetail, username);
            return Ok(result);
        }
    }
}
