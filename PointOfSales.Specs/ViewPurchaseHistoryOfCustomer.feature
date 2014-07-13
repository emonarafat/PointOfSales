Feature: ViewPurchaseHistoryOfCustomer
	In order to find customers which spend money
	As a salesman
	I want to be able to view the purchase history of a customer

Scenario: Customer without orders does not have purchase history
	Given customer without orders	
	When I view purchase history
	Then I do not see any orders

Scenario: Customer with orders have pruchase history
    Given cusomer with 2 orders
	When I view purchase history
	Then I see 2 orders