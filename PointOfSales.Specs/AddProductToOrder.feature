Feature: OrderFilling
	In order to sell products
	As a salesman
	I want to be able to add a product to an order

Scenario: Add product to empty order
	Given I have an empty order	
	When I add product to order
	Then order should have order line with product

Scenario: Add multiple products
	Given I have an empty order
	When I add two products to order
	Then order should contain both products

Scenario: Add same product multiple times
	Given I have an empty order
	When I add product to order
     And I add product to order
	Then order should have order line with product
	 And order line quantity should be 2