@sales
Feature: View available sales combinations of product
	In order to suggest a discount
	As a salesman
	I want to view the available sales combinations when selecting a product

Background: 
	Given there are following products in shop
	| Name                 |
	| Apple iPhone 5       |
	| Nokia 3310           |
	| Belkin Charge        |
	| Speck SmartFlex Case |
	| Speck GemShell Case  |
	And there are following sales combinations in shop
	| MainProduct          | SubProduct           |
	| Apple iPhone 5       | Belkin Charge        |
	| Apple iPhone 5       | Speck SmartFlex Case |
	| Speck SmartFlex Case | Speck GemShell Case  |

Scenario: Product does not have available sales combinations 
	When I view available sales combinations of product 'Nokia 3310'
	Then I do not see any sales combinations

Scenario: Product is the main product of sales combinations
	When I view available sales combinations of product 'Apple iPhone 5'
	Then I see following sales combinations
	| MainProduct    | SubProduct           |
	| Apple iPhone 5 | Belkin Charge        |
	| Apple iPhone 5 | Speck SmartFlex Case |

Scenario: Product is the sub product of sales combination
	When I view available sales combinations of product 'Belkin Charge'
	Then I see following sales combinations
	| MainProduct    | SubProduct           |
	| Apple iPhone 5 | Belkin Charge        |

Scenario: Product is both main and sub product of sales combinations
	When I view available sales combinations of product 'Speck SmartFlex Case'
	Then I see following sales combinations
	| MainProduct          | SubProduct           |
	| Apple iPhone 5       | Speck SmartFlex Case |
	| Speck SmartFlex Case | Speck GemShell Case  |