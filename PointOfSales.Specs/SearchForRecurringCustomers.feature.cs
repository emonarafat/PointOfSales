﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.18444
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace PointOfSales.Specs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class SearchForRecurringCustomersFeature : Xunit.IUseFixture<SearchForRecurringCustomersFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SearchForRecurringCustomers.feature"
#line hidden
        
        public SearchForRecurringCustomersFeature()
        {
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Search for recurring customers", "In order to avoid entering customer details twice\nAs a salesman\nI want to be able" +
                    " to search for recurring customers by string", ProgrammingLanguage.CSharp, new string[] {
                        "customers"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 7
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "FirstName",
                        "LastName",
                        "EmailAddress"});
            table1.AddRow(new string[] {
                        "John",
                        "Doe",
                        "john.doe@mail.com"});
            table1.AddRow(new string[] {
                        "Jack",
                        "Finney",
                        "jack@mail.com"});
            table1.AddRow(new string[] {
                        "Bill",
                        "Doe",
                        "bill@mail.com"});
#line 8
 testRunner.Given("there are following customers in the shop", ((string)(null)), table1, "Given ");
#line hidden
        }
        
        public virtual void SetFixture(SearchForRecurringCustomersFeature.FixtureData fixtureData)
        {
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Search for recurring customers")]
        [Xunit.TraitAttribute("Description", "Customer is not added yet")]
        public virtual void CustomerIsNotAddedYet()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Customer is not added yet", ((string[])(null)));
#line 14
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 15
 testRunner.When("I search for recurring customer \'Mike\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 16
 testRunner.Then("I do not see any customers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Search for recurring customers")]
        [Xunit.TraitAttribute("Description", "Single customer found")]
        public virtual void SingleCustomerFound()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Single customer found", ((string[])(null)));
#line 18
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 19
 testRunner.When("I search for recurring customer \'Jack\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "FirstName",
                        "LastName",
                        "EmailAddress"});
            table2.AddRow(new string[] {
                        "Jack",
                        "Finney",
                        "jack@mail.com"});
#line 20
 testRunner.Then("I see only these customers", ((string)(null)), table2, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Search for recurring customers")]
        [Xunit.TraitAttribute("Description", "Several customers found")]
        public virtual void SeveralCustomersFound()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Several customers found", ((string[])(null)));
#line 24
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 25
 testRunner.When("I search for recurring customer \'Doe\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "FirstName",
                        "LastName",
                        "EmailAddress"});
            table3.AddRow(new string[] {
                        "John",
                        "Doe",
                        "john.doe@mail.com"});
            table3.AddRow(new string[] {
                        "Bill",
                        "Doe",
                        "bill@mail.com"});
#line 26
 testRunner.Then("I see only these customers", ((string)(null)), table3, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                SearchForRecurringCustomersFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                SearchForRecurringCustomersFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
