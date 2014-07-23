@products
Feature: Products Search
	In order to quickly allocate products by something I remember from them
	As a salesman
	I want to search for products by string

Background:
	Given there are following products in shop
	| Name             | Description                                 |
	| Apple iPhone 5   | Touchscreen smartphone                      |
	| Nokia Lumia 1020 | Touchscreen smartphone with PureView camera |
	| Belkin Charge    | Charger for iPhone or iPod                  |
	| Nokia 3310       | Mobile phone                                |

Scenario: Search products by name	
	When I search products by 'nokia'
	Then I see only these products: 'Nokia Lumia 1020', 'Nokia 3310'
	

Scenario: Search products by description	
	When I search products by 'smartphone'
	Then I see only these products: 'Apple iPhone 5', 'Nokia Lumia 1020'

Scenario: Search products by name or description	
	When I search products by 'iphone'
	Then I see only these products: 'Apple iPhone 5', 'Belkin Charge'