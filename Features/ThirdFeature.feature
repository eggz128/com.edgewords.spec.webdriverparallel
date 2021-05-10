Feature: Do something completely different
#Step definitions for these scenarios are in FirstSteps.cs and SecondSteps.cs

Scenario: Login to edgewords webdriver site
	Given I am on the Edgewords Login Page
	When I use the credentials
	| Username  | Password     |
	| edgewords | edgewords123 |
	Then I can login

Scenario Outline: Searching Google
	Given I am on the Google Home page
	When I search for '<search term>'
	Then '<search term>' should be in the top result
	Examples:
	| search term |
	| edgewords   |
	| webdriver   |