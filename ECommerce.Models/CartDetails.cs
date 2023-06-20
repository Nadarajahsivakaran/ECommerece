using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class CartDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? CartId { get; set; }

        [Required]
        public string? ProductId { get; set;}
        public Product? Product { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public double SubTotal { get; set; }
    }
}
