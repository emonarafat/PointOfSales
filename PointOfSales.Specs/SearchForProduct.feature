@products
Feature: ProductsSearch
	In order to quickly sell products
	As a salesman
	I want to be able to search for a product

Scenario: Search by name
	Given I have some products	  
	When I search products by name
	Then I see products with names containing search string

Scenario: Search by description
	Given I have some products	  
	When I search products by description
	Then I see products with description containing search string

Scenario: Search by name or description
	Given I have some products	  
	When I search products by name or description
	Then I see products with either name or description containing search string
