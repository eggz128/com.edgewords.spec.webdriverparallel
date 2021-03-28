Feature: SixthFeature

Scenario: Opens Google with a shared context browser
	Given I am on the Google Home page
	When I put 'FooBar' in the SomeDataToPassAround key
	Then the new value is here

Scenario: Opens Bing with a shared context browser
	Given I am on the Bing Home page
	Then the SomeDataToPassAround value is still Hello World