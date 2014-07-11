using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace PointOfSales.Specs.Steps
{
    [Binding]
    public class CustomerSteps
    {
        [Given(@"I don't have any customers")]
        public void GivenIDonTHaveAnyCustomers()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I add customer")]
        public void WhenIAddCustomer()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"customer should exist in the system")]
        public void ThenCustomerShouldExistInTheSystem()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
