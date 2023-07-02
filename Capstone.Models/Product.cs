﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [StringLength(6, MinimumLength = 6)]
        public string? UNnumber { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")] // Limit to two decimal places
        public double Price { get; set; }
        [Required]
        [DisplayName("Price at 10+ purchased")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public double BulkRate10 { get; set; }
        [Required]
        [DisplayName("Price at 100+ purchased")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        public double BulkRate100 { get; set;}
        [Required]
        public int Inventory { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}