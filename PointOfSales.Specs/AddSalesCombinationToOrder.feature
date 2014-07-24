Feature: Add sales combination to order
	In order to quickly fill order with products on sale
	As a salesman
	I want to be able to add a sales combination to an order

Background: 
	Given there are following products in shop
	| Name                 | Price |
	| Apple iPhone 5       | 500   |
	| Belkin Charge        | 50    |
	| Speck SmartFlex Case | 100   |
	And there are following sales combinations in shop
	| MainProduct    | SubProduct           | Discount |
	| Apple iPhone 5 | Belkin Charge        | 5        |
	| Apple iPhone 5 | Speck SmartFlex Case | 20       |

Scenario: Adding sales combination with new products adds both products and applies discount
	Given I have an empty order	
	When I add following sales combination to this order
	| MainProduct          | SubProduct           |
	| Apple iPhone 5       | Belkin Charge        |
	Then order should have following lines
	| ProductName    | Quantity |
	| Apple iPhone 5 | 1        |
	| Belkin Charge  | 1        |
	 And total price should be 545

Scenario: Adding sales combination for already added product increases product quantity and adds second product with discount
	Given I have an empty order
	When I add 'Apple iPhone 5' to this order
	 And I add following sales combination to this order
	| MainProduct          | SubProduct           |
	| Apple iPhone 5       | Belkin Charge        |
	Then order should have following lines
	| ProductName    | Quantity |
	| Apple iPhone 5 | 2        |
	| Belkin Charge  | 1        |
 	 And total price should be 1045

Scenario: Adding two products from sales combination automatically applies discount
	Given I have an empty order
	When I add 'Apple iPhone 5' to this order
	And I add 'Belkin Charge' to this order
	Then order should have following lines
	| ProductName    | Quantity |
	| Apple iPhone 5 | 1        |
	| Belkin Charge  | 1        |
	 And total price should be 545

Scenario: Adding sales combination for already added products increases both quantities and applies discount
	Given I have an empty order
	When I add 'Apple iPhone 5' to this order
	And I add 'Belkin Charge' to this order
	And I add following sales combination to this order
	| MainProduct          | SubProduct           |
	| Apple iPhone 5       | Belkin Charge        |
	Then order should have following lines
	| ProductName    | Quantity |
	| Apple iPhone 5 | 2        |
	| Belkin Charge  | 2        |
	 And total price should be 1090
