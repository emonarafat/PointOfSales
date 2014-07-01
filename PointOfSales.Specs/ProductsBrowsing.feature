Feature: ProductsBrowsing
	In order to manage products
	As a salesman
	I want to see all available products

@products
Scenario: No products available
	Given I have no products	
	When I trying to see all available products
	Then I do not see any products
