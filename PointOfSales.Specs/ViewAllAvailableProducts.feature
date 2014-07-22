﻿Feature: View all available products
	In order see what products there are
	As a salesman
	I want to view all available products

Scenario: No products available
	Given I have no products	
	When I am trying to see all available products
	Then I do not see any products

Scenario: Products available
	Given I have some products
	When I am trying to see all available products
	Then I see all products