Feature: Search For Recurring Customers
	In order to avoid entering customer details twice
	As a salesman
	I want to be able to search for recurring customers

Scenario: No customers in the system
	Given I have no customers
	When I search for recurring customer 'Doe'
	Then I do not see any customers

Scenario: No recurring customers matching search
	Given I have some customers
	When I search for recurring customer 'Jim'
	Then I do not see any customers

Scenario: Recurring customers exist
	Given I have some customers
	When I search for recurring customer 'John'
	Then I see all customers with names containing search string
	