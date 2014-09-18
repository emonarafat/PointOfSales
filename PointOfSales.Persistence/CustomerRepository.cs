using NLog;
using PointOfSales.Domain.Model;
using PointOfSales.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace PointOfSales.Persistence
{
    public class CustomerRepository : Repository, ICustomerRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public IEnumerable<Customer> GetAll()
        {
            var sql = "SELECT * FROM Customers";

            using (var conn = GetConnection())
            {
                var customers = conn.Query<Customer>(sql).ToList();
                Logger.Trace("{0} customers found", customers.Count);
                return customers;
            }
        }

        public Customer Add(Customer customer)
        {
            // TODO: Email should be unique
            var sql = @"INSERT INTO Customers (FirstName, LastName, MiddleName, EmailAddress, Street, HouseNumber, PostalCode, City, EntryDate)
                        OUTPUT INSERTED.CustomerID, INSERTED.EntryDate
                        VALUES (@firstName, @lastName, @middleName, @emailAddress, @street, @houseNumber, @postalCode, @city, GETDATE())";

            using (var conn = GetConnection())
            {
                var result = conn.Query(sql, customer).Single();
                customer.CustomerId = result.CustomerID;
                customer.EntryDate = result.EntryDate;
                return customer;
            }
        }

        public IEnumerable<Customer> GetByName(string search)
        {
            Logger.Debug("Getting customers by name '{0}'", search);
            var sql = @"SELECT * FROM Customers WHERE FirstName LIKE @search OR LastName LIKE @search";

            using (var conn = GetConnection())
            {
                var customers = conn.Query<Customer>(sql, new { search = String.Format("{0}%", search) }).ToList();
                Logger.Trace("{0} customers found", customers.Count);
                return customers;
            }
        }

        public Customer GetById(int id)
        {
            var sql = "SELECT * FROM Customers WHERE CustomerID = @id";

            using (var conn = GetConnection())
            {
                var customer = conn.Query<Customer>(sql, new { id = id }).SingleOrDefault();

                if (customer == null)
                    Logger.Warn("Customer not found");

                return customer;
            }
        }

        public bool Update(Customer customer)
        {            
            Logger.Debug("Updating customer {0}", customer.CustomerId);
            var sql = @"UPDATE Customers SET
                           FirstName = @firstName,
                           LastName = @lastName,
                           MiddleName = @middleName,
                           EmailAddress = @emailAddress,
                           City = @city,
                           Street = @street,
                           HouseNumber = @houseNumber,
                           PostalCode = @postalCode
                         WHERE CustomerID = @customerId";

            using (var conn = GetConnection())
            {
                int updatedCustomersCount = conn.Execute(sql, customer);
                return updatedCustomersCount == 1;
            }
        }
    }
}
