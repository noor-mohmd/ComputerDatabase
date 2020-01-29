

Feature: ComputerDatabase

Scenario Outline: Computer Library
	Given the user is on home page
	When the user adds a new computer with following data
	| Computer Name   | Introduced date   | Discontinued date   | Company   |
	| <Computer Name> | <Introduced date> | <Discontinued date> | <Company> |
	Then the computer should be added with name '<Computer Name>'

	And the user updates existing computer with following data
	| New Computer Name   | New Introduced date   | New Discontinued date   | New Company   | Computer Name   | Introduced date   | Discontinued date   | Company   |
	| <New Computer Name> | <New Introduced date> | <New Discontinued date> | <New Company> | <Computer Name> | <Introduced date> | <Discontinued date> | <Company> |
	Then the computer should be updated with name '<New Computer Name>'

	And the user deletes the computer
	Then the computer should be deleted with name '<New Computer Name>'
	Examples:
	| Computer Name | Introduced date | Discontinued date | Company          | New Computer Name | New Introduced date | New Discontinued date | New Company |
	| Mainframe     |                 |                   |                  | Mainframe         |                     |                       | Canon       |
	| Sparc         | 1998-12-28      |                   |                  | Sparc             | 1999-12-18          |                       |             |
	| PowerPC       | 1998-12-28      |                   | Sun Microsystems | PowerPC           | 1998-12-28          | 2011-01-14            | Nokia       |
	| ServerMachine |                 | 2000-11-13        | Sony             | NewServerMachine  | 1989-05-09          | 2000-11-13            |             |
	| AS400         | 2000-10-16      | 2010-06-20        | IBM              | AS(&400)          | 2000-10-16          |                       | Apple Inc.  |

Scenario Outline: Computer Not added
	Given the user is on home page
	When the user tries to add a new computer with following data
	| Computer Name   | Introduced date   | Discontinued date   | Company   | ErrorOnField   |
	| <Computer Name> | <Introduced date> | <Discontinued date> | <Company> | <ErrorOnField> |
	Then the computer should not be added
	Examples: 
	| Computer Name | Introduced date | Discontinued date | Company | ErrorOnField |
	|               | 1998-12-28      | 2010-06-20        | Nokia   | 1            |
	| Mainframe     | 2019-02-29      | 2020-01-10        | Canon   | 2            |
	| Mainframe     |                 | 2019-06-31        |         | 3            |
