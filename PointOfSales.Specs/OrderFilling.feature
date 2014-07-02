Feature: OrderFilling
	In order to sell products
	As a salesman
	I want to be able to add a product to an order

Scenario: Add product to empty order
	Given I have an empty order	
	When I add product to order
	Then order should have order line with product