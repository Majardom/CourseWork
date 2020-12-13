Feature: LogIn

@mytag
Scenario: LogIn with correct credentials
	Given Launch Firefox
	And Navigate to Web Frontend
	When Enter admin for login and password
	And Click on LogIn button
	And Click on Validatio button
	Then Token and valdation result should be visible