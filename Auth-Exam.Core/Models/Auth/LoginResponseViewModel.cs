using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Auth_Exam.Core.Models.Auth
{
    public class LoginResponseViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();

        [JsonIgnore]
        public string Token { get; set; } = string.Empty;
        
        
        
        
        
        
        
        
    }
}