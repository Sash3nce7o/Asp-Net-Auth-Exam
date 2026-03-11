using System.Net;
using Microsoft.AspNetCore.Identity;
using Auth_Exam.Core.Contracts;
using Auth_Exam.Core.Models.Auth;
using Auth_Exam.Core.Models.HTTP;
using Auth_Exam.Core.TokenProviders;
using Auth_Exam.Infrastructure.Data.Models;

namespace Auth_Exam.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenProvider _tokenProvider;
        
        public AuthService(UserManager<User> userManager, TokenProvider tokenProvider)
        {
            _userManager = userManager;
            _tokenProvider = tokenProvider;
        }
        
        public async Task<Response<LoginResponseViewModel>> Login(LoginFormViewModel model)
        {
            Response<LoginResponseViewModel> response = new Response<LoginResponseViewModel>();
            
            try
            {
                // Step 1: Find user by email
                User? user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                
                // Step 2: Verify password
                var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!passwordValid)
                {
                    response.Message = "Invalid email or password";
                    return response;
                }
                
                // Step 3: Get user roles
                IList<string> userRoles = await _userManager.GetRolesAsync(user);
                
                // Step 4: Generate token
                var token = _tokenProvider.GenerateToken(user, userRoles);
                
                // Step 5: Build success response
                response.Data = new LoginResponseViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = userRoles,
                    Token = token
                };
                
                return response;
            }
            catch (Exception)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Something went wrong";
                return response;
            }
        }
    }
}