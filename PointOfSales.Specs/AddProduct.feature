@products
Feature: Add Product
	In order to sell new products
	As a salesman
	I want to be able to enter the details of a product in the system

Scenario: Add product
	Given there are no products in shop
	When I add following product
	| Name       | Description  |
	| Nokia 3310 | Mobile phone |
	Then I see following products
	| Name       | Description  |
	| Nokia 3310 | Mobile phone |
