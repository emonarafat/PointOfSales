Feature: Edit detais of customer
	In order to keep data up-to date
	As a salesman
	I want to be able to edit the details of a customer

Scenario: Edit existing customer
	Given there is following customer in shop
	| Field        | Value             |
	| FirstName    | John              |
	| LastName     | Doe               |
	| MiddleName   |                   |
	| EmailAddress | john.doe@mail.com |
	| City         | Washington        |
	| Street       | Pennsylvania Ave  |
	| HouseNumber  | 1600              |
	| PostalCode   | 20500             |
	When I edit details of this customer
	| Field        | Value             |
	| FirstName    | Jack              |
	| LastName     | Finney            |
	| MiddleName   |                   |
	| EmailAddress | john.doe@mail.com |
	| City         | New York          |
	| Street       | West 72nd Street  |
	| HouseNumber  | 1                 |
	| PostalCode   | 10023             |
	Then I see updated customer details
	| Field        | Value             |
	| FirstName    | Jack              |
	| LastName     | Finney            |
	| MiddleName   |                   |
	| EmailAddress | john.doe@mail.com |
	| City         | New York          |
	| Street       | West 72nd Street  |
	| HouseNumber  | 1                 |
	| PostalCode   | 10023             |