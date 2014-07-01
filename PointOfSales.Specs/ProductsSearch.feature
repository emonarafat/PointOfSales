Feature: ProductsSearch
	In order to quickly sell products
	As a salesman
	I want to be able to search for a product

@products
Scenario: Search by name
	Given I have some products	  
	When I search products by name
	Then I see products with names containing search string
