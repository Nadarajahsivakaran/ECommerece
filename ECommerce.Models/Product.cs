using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ECommerce.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Product Name")]
        public string? Name { get; set; }

        public string? Description { get; set; } 

        [Required,Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set;}

        [Required, Display(Name = "Unit Price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than 0")]
        public double UnitPrice { get; set; } 

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdateddAt { get; set; } = DateTime.Now;
    }
}
