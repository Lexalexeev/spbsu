/*
* Buffer overflow
Alekseev Aleksei, group 171.
*/

#include <stdio.h>
#include <string.h>

void overflow(char str[]) {
	char buffer[4];
	strcpy(buffer, str);
}

void extrafunc() {
	printf("\nExtraneous function was called\n");
}

int main(void) {
	char str[] = "rubbishrubbi"
	"\xef\x11\x41\x00";
	printf("address: 0x%x\n", (int)(&extrafunc));
	overflow(str);
	return 0;
}