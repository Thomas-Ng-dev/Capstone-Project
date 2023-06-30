using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Company Name")] // Tag helper, will display this name when this property is used elsewhere, server side
        public string Name { get; set; }
        [Required]
        [DisplayName("Street Address")]
        public string Address { get; set; }
        [Required]
        [DisplayName("City")]
        public string City { get; set; }
        [Required]
        [DisplayName("State/Province")]
        public string Region { get; set; } // State or Province
        [Required]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        [Required]
        [DisplayName("Country")]
        public string Country { get; set; }
        [Required]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }
        [Required]
        [DisplayName("E-mail")]
        public string Email { get; set; }
    }
}
