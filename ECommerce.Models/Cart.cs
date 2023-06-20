using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public string? UserId { get; set; }

        [Required]
        public double Total { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
