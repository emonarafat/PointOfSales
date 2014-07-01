Feature: AvailableSalesCombinations
	In order to suggest a discount
	As a salesman
	I want to see the available sales combinations when selecting a product

Scenario: No sales combinations available
	Given I have selected product without sales combinaions	
	When I am trying to see available sales combinations
	Then I do not see any available sales combinations
