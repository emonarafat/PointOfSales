@customers
Feature: Add customer
	In order suggest discounts and spam
	As a salesman
	I want to be able to enter the details of a customer in the system

Scenario: Add customer
	Given there are no customers in the shop
	When I add following customer
	| Field        | Value         |
	| FirstName    | Jack          |
	| LastName     | Finney        |
	| EmailAddress | jack@mail.com |
	Then I see following customers
	| FirstName | LastName | EmailAddress  |
	| Jack      | Finney   | jack@mail.com |