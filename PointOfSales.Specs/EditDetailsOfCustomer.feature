Feature: Edit Detais Of Customer
	In order to keep data up-to date
	As a salesman
	I want to be able to edit the details of a customer

Scenario: Edit existing customer
	Given I have some customers	
	When I edit details of a customer
	Then I should see updated customer
