Feature: AvailableSalesCombinations
	In order to suggest a discount
	As a salesman
	I want to see the available sales combinations when selecting a product

Background: 
	Given I have products and sales combinaions	

Scenario: Product does not have sales
	When I am trying to see available sales combinations of product without sales
	Then I do not see any available sales combinations

Scenario: Sub-products available for sales
	When I am trying to see available sales combinations of product with sub-product sales
	Then I see sub-products sales combinations
