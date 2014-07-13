using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.Domain.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        int Add(Customer customer);

        IEnumerable<Customer> GetRecurringCustomers();
    }
}
