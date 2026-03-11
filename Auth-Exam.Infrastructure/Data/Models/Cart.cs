using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth_Exam.Infrastructure.Data.Models
{
    public class Cart
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }

        public User? User { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}