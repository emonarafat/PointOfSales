@orders
Feature: Add products to order
	In order to sell products
	As a salesman
	I want to be able to add a products to an order

Background: 
	Given there are following products in shop
	| Name           | Price |
	| Apple iPhone 5 | 500   |
	| Nokia 3310     | 100   |

Scenario: Add product to empty order
	Given I have an empty order	
	When I add 'Apple iPhone 5' to this order
	Then order should have following lines
	| ProductName    | Quantity |
	| Apple iPhone 5 | 1        |
	And total price should be 500

Scenario: Add multiple products to empty order
	Given I have an empty order
	When I add 'Apple iPhone 5' to this order
	 And I add 'Nokia 3310' to this order
	Then order should have following lines
	| ProductName    | Quantity |
	| Apple iPhone 5 | 1        |
	| Nokia 3310     | 1        |
	And total price should be 600

Scenario: Add same product multiple times to empty order
	Given I have an empty order
	When I add 'Nokia 3310' to this order
     And I add 'Nokia 3310' to this order
	Then order should have following lines
	| ProductName    | Quantity |	
	| Nokia 3310     | 2        |
	And total price should be 200