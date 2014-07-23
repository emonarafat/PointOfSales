@products
Feature: View all available products
	In order see what products there are
	As a salesman
	I want to view all available products

Scenario: No products available
	Given there are no products in shop	
	When I view all available products
	Then I do not see any products

Scenario: Products available
	Given there are 5 products in shop
	When I view all available products
	Then I see 5 products