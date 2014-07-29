@customers @orders
Feature: View purchase history of customer
	In order to find customers which spend money
	As a salesman
	I want to be able to view the purchase history of a customer

Background: 
	Given there are following customers in the shop
	| FirstName | LastName | EmailAddress      |
	| John      | Doe      | john.doe@mail.com |
	| Jack      | Finney   | jack@mail.com     |
	And there are following orders in the shop
	| Customer | Date       |
	| John Doe | 2014-06-04 |
	| John Doe | 2014-07-29 |


Scenario: Customer without orders does not have purchase history
	When I view purchase history of 'Jack Finney'
	Then I do not see any orders

Scenario: Customer with orders have pruchase history
	When I view purchase history of 'John Doe'
	Then I see following orders
	| Date       |
	| 2014-06-04 |
	| 2014-07-29 |