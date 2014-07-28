Feature: Search for recurring customers
	In order to avoid entering customer details twice
	As a salesman
	I want to be able to search for recurring customers by string

Background:
	Given there are following customers in the shop
	| FirstName | LastName | EmailAddress      |
	| John      | Doe      | john.doe@mail.com |
	| Jack      | Finney   | jack@mail.com     |
	| Bill      | Doe      | bill@mail.com     |

Scenario: Customer is not added yet
	When I search for recurring customer 'Mike'
	Then I do not see any customers

Scenario: Single customer found
	When I search for recurring customer 'Jack'
	Then I see only these customers
	| FirstName | LastName | EmailAddress  |
	| Jack      | Finney   | jack@mail.com |

Scenario: Several customers found
	When I search for recurring customer 'Doe'
	Then I see only these customers
	| FirstName | LastName | EmailAddress  |
	| John      | Doe      | john.doe@mail.com |
	| Bill      | Doe      | bill@mail.com     |