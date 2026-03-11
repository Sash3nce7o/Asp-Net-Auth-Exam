using System.Net;
using Auth_Exam.Core.Contracts;
using Auth_Exam.Core.Models.Auth;
using Auth_Exam.Core.Models.HTTP;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Exam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginFormViewModel model)
        {
            // Call AuthService to authenticate
            Response<LoginResponseViewModel> response = await _authService.Login(model);

            // Check for errors
            switch (response.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                    return StatusCode(500, response);
            }

            // Create cookie options (HTTP-only = secure)
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true
            };

            // Add token to response as HTTP-only cookie
            Response.Cookies.Append("token", response.Data!.Token, cookieOptions);

            // Return success response
            return Ok(response);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Check if cookie exists and delete it
            if (Request.Cookies.TryGetValue("token", out string? cookie))
            {
                Response.Cookies.Delete("token");
            }

            return Ok();
        }
    }
}
