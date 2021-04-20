Feature: GetShops
	I will like to validate that the shop Koge exist
	From the list of all shops

@mytag
Scenario: Validate Shop Koge Exist 
	Given I navigate to the website and I send a POST request to get the bearer token
	When I send a GET request to receive the list of shops 
	Then I should receive a response that shop Koge exist