/*
* Substring search
Alekseev Aleksei, group 171.
*/

#include <stdio.h>
#include <stdlib.h>

enum
{
	MAX_LENGTH = 100
};

int main(void) {
	char str[MAX_LENGTH], subStr[MAX_LENGTH];
	int strLength = 0, subStrLength = 0, i = 0, j = 0, counter = 0;

	printf("Enter the string (no more than %d characters):\n", MAX_LENGTH);
	fgets(str, MAX_LENGTH, stdin);
	printf("Enter the substring (no more than %d characters):\n", MAX_LENGTH);
	fgets(subStr, MAX_LENGTH, stdin);

	str[strlen(str) - 1] = '\0';
	subStr[strlen(subStr) - 1] = '\0';
	strLength = strlen(str);
	subStrLength = strlen(subStr);

	while (i <= strLength - 1) {
		while (str[i] == ' ') {
			i++;
		}
		if (str[i] == subStr[j]) {
			if (j == strlen(subStr) - 1) {
				counter++;
				j = 0;
			}
			else
				j++;
		}
		else 
			j = 0;	
		i++;	
	}
	printf("The number of occurrences is: %d\n", counter);	
	return 0;
}
