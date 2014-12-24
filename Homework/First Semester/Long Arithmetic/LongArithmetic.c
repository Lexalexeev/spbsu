/*
* Long arithmetic
Alekseev Aleksei, group 171.
*/

#include <stdio.h>
#include <stdlib.h>

typedef struct Node {
	int value;
	struct Node *next;
} Node;

void addNumber(Node **head, int data) {
	Node *temp = (Node*)malloc(sizeof(Node));
	if (!temp) {
		return;
	}
	temp->value = data;
	temp->next = (*head);
	(*head) = temp;
}

void printNumber(const Node **head) {
	Node *temp = *head;
	printf("Result:");
	while (temp != NULL) {
			printf("%d", temp->value);
		temp = temp->next;
	}
	printf("\n");
}

void inputNumber(Node **number) {
	int counter = 0; char ch = ' ';

    while (ch != 'q') {
		if (counter == 0) {
			printf("Enter the number:\n");
			counter++;
		}
		if (counter == 1) {
			scanf("%c", &ch);
			if ((ch >= 48) && (ch <= 57)) {
				addNumber(number, (int)(ch - '0'));
			}
			if (ch == '\n')
				break;
        }
	}
}

void operation(Node **head1, Node **head2, Node **head3) {
	char ch; int result = 0, jump = 0;
	printf("Enter the operation (+ or - or * or /):\n");
	scanf("%c", &ch);	

	if (ch == '+') {
		Node *temp1 = *head1;
		Node *temp2 = *head2;
		while (temp1 != NULL || temp2 != NULL) {
			if (temp1 == NULL) {
				result += temp2->value;
				if (result > 9) {
					result %= 10;
					jump++;
				}
				temp2 = temp2->next;
			} 
			else if (temp2 == NULL) {
				result += temp1->value;
				if (result > 9) {
					result %= 10;
					jump++;
				}
				temp1 = temp1->next;
			}
			else {
				result += temp1->value + temp2->value;
				if (result > 9) {
					result %= 10;
					jump++;
				}
				temp1 = temp1->next;
				temp2 = temp2->next;
			}
			addNumber(head3, result);
			if (temp1 == NULL && temp2 == NULL && jump == 1) {
				addNumber(head3, jump);
			}
			result = jump;
			jump = 0;
		}
		printNumber(head3);
	}
}

int main(void) {
	Node *Number1 = NULL;
	Node *Number2 = NULL;
	Node *Number3 = NULL;
	inputNumber(&Number1);
	inputNumber(&Number2);
	operation(&Number1, &Number2, &Number3);
	return 0;
}




