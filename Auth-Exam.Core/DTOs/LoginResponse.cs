using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Exam.Core.DTOs
{
    public class LoginResponse
    {
        public LoginResponse()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string? Email { get; set; } = string.Empty;
        public IList<string>? Roles { get; set; }
        public string Token { get; set; } = string.Empty;
        
        
        
        
        
    }
}