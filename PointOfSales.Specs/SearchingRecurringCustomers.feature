Feature: SearchingRecurringCustomers
	In order to sell more products
	As a salesman
	I want to be able to search for recurring customers

Scenario: Customer without orders is not a recurring customer
	Given customer without orders	
	When I search recurring customers
	Then I don't see any customers

Scenario: Customer with single order is not a recurring customer
	Given cusomer with 1 orders
	When I search recurring customers
	Then I don't see any customers

Scenario: Customer with several orders is a recurring customer
	Given cusomer with 2 orders
	When I search recurring customers
	Then I see 1 customer
	