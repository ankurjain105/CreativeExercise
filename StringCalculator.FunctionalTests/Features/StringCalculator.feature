Feature: StringCalculator
	In order to add numbers represented as string
	As a user
	I *want* to be told the numbers

@mytag
Scenario Outline: Add numbers represented in string
	Given the numbers represented as "<input1>" and "<inputLine2>"
	When calculator add method is invoked
	Then the result should be <result>
Examples: 
	| input1    | inputLine2 | result |
	|           |            | 0      |
	| 1,2       |            | 3      |
	| 1         |            | 1      |
	| 1         | 2,3        | 6      |
	| 1,1002, 4 |            | 5      |
	| //;       | 1;2        | 3      |
	| //;#      | 1;2#8      | 11     |
	| //*%      | 1*2%4      | 7      |


Scenario Outline: Throw exception for numbers represented in invalid string
	Given the numbers represented as "<input1>" and "<inputLine2>"
	When calculator add method is invoked
	Then exception should be thrown with message "<message>"
Examples: 
	| input1  | inputLine2 | message                             |
	| 1,-1    |            | Negatives not allowed - -1          |
	| 1,-2,-3 |            | Negatives not allowed - -2,-3       |
	| 2,      | 3          | Consecutive Delimiters not allowed. |