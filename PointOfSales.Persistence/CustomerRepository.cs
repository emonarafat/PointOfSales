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
            Logger.Debug("Getting all customers");
            var sql = "SELECT * FROM Customers";

            using (var conn = GetConnection())
                return conn.Query<Customer>(sql);
        }

        public int Add(Customer customer)
        {
            // TODO: Email should be unique
            Logger.Debug("Adding customer");
            var sql = @"INSERT INTO Customers (FirstName, LastName, MiddleName, EmailAddress, Street, HouseNumber, PostalCode, City, EntryDate)
                        VALUES (@firstName, @lastName, @middleName, @emailAddress, @street, @houseNumber, @postalCode, @city, GETDATE());
                        SELECT CAST(SCOPE_IDENTITY() AS INT)";

            using (var conn = GetConnection())
            {
                int id = conn.Query<int>(sql, customer).Single();
                Logger.Trace("Customer {0} created", id);
                return id;
            }
        }

        public IEnumerable<Customer> GetByName(string search)
        {
            Logger.Debug("Getting customers by name '{0}'", search);
            var sql = @"SELECT * FROM Customers WHERE FirstName LIKE @search OR LastName LIKE @search";

            using (var conn = GetConnection())
                return conn.Query<Customer>(sql, new { search = String.Format("{0}%", search) });
        }

        public Customer GetById(int id)
        {
            Logger.Debug("Getting customer {0}", id);
            var sql = "SELECT * FROM Customers WHERE CustomerID = @id";

            using (var conn = GetConnection())
                return conn.Query<Customer>(sql, new { id = id }).SingleOrDefault();
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
                return conn.Execute(sql, customer) == 1;
        }
    }
}
