using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Exam.Core.Models.Product
{
    public class UpdateProductFormViewModel
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; } = 0;
        
        

        
        
        
    }
}