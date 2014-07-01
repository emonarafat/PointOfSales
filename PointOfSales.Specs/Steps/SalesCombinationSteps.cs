using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace PointOfSales.Specs.Steps
{
    [Binding]
    public class SalesCombinationSteps
    {
        [Given(@"I have selected product without sales combinaions")]
        public void GivenIHaveSelectedProductWithoutSalesCombinaions()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I am trying to see available sales combinations")]
        public void WhenIAmTryingToSeeAvailableSalesCombinations()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I do not see any available sales combinations")]
        public void ThenIDoNotSeeAnyAvailableSalesCombinations()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
