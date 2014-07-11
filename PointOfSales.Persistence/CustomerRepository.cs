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

        public void Add(Customer customer)
        {
            Logger.Debug("Adding customer");
            var sql = @"INSERT INTO Customers (FirstName, LastName, MiddleName, EmailAddress, Street, HouseNumber, PostalCode, City, EntryDate)
                        VALUES (@firstName, @lastName, @middleName, @emailAddress, @street, @houseNumber, @postalCode, @city, GETDATE())";

            using (var conn = GetConnection())
                conn.Execute(sql, customer);
        }
    }
}
