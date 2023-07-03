using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models.ViewModels
{
    public class ProductVM // Product View Model, Access Product and CustomerList from the same class
    {
        public Product Product { get; set; }
        [ValidateNever]
        // Will create null pointer exception during CRUD
        public IEnumerable<SelectListItem> CustomerList { get; set; }
    }
}
