using System.Threading.Tasks;
using Foody.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Models;

namespace Foody.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]LoginRequest request)
        {
            TokenApiModel tokenApiModel = await _loginService.Authenticate(request.UserName, request.Password);
            return Ok(tokenApiModel);
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody]TokenApiModel tokenApiModel)
        {
            TokenApiModel newTokenApiModel = await _loginService.RefreshToken(tokenApiModel);
            return Ok(newTokenApiModel);
        }

        [HttpPost("Revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            string username = User.Identity.Name;
            _ = await _loginService.RevokeToken(username);
            return NoContent();
        }

        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody]SignUpRequest request)
        {
            int result = await _loginService.SignUp(request);
            return Ok(result);
        }
    }
}
