using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DataAccess.Repository.IRepository
{
    public interface ICustomerRepository
    {
        void Update(Customer customer);
    }
}
