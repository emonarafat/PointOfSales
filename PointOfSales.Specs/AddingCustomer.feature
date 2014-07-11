Feature: AddingCustomer
	In order suggest discounts and spam
	As a salesman
	I want to be able to enter the details of a customer in the system

Scenario: Adding new customer
	Given I don't have any customers	
	When I add customer
	Then customer should exist in the system
