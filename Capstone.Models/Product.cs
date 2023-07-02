using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public double NetWeight { get; set; }
        [Required]
        public double GrossWeight { get; set; }
        [Required]
        public bool IsHazardous { get; set; }
        [Required]
        public int Inventory { get; set; }
    }
}
