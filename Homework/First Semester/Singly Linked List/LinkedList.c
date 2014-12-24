/*
* Linked list
Alekseev Aleksei, group 171.
*/

#include <stdio.h>
#include <stdlib.h>

typedef struct Node {
	int value;
	struct Node *next;
} Node;

void addElem(Node **head, int data) {
	Node *temp = (Node*)malloc(sizeof(Node));
	if (!temp) {
		return;
	}
	temp->value = data;
	temp->next = (*head);
	(*head) = temp;
}

/*
int delElem(Node **head) {
Node *previous = NULL;
int val;
if (head == NULL) {
exit(-1);
}
previous = (*head);
val = previous->value;
(*head) = (*head)->next;
free(previous);
return val;
}
*/

void delFirst(Node **head, int data) {
	Node *temp = *head;
	Node *previous = NULL;
	int finished = 0;
	while (temp != NULL) {
		if (temp->value == data) {
			if (temp == *head)
				*head = (*head)->next;
			else
				previous->next = temp->next;
			finished = 1;
			break;
		}
		previous = temp;
		temp = temp->next;
		if (finished) 
			break;
	}
	free(temp);
	return;
}

void printList(const Node **head) {
	Node *temp = *head;
	if (temp == NULL)
		printf("The list is empty");
	while (temp != NULL) {
		if (temp->next == NULL)
			printf("%d.", temp->value);
		else
			printf("%d, ", temp->value);
		temp = temp->next;
	}
	printf("\n");
}

void input(Node **list) {
	int number; char command = ' '; char temp = ' ';
	while (command != 'q') {
		scanf("%c", &command);
		if (command == 'q') 
			break;
		scanf("%c", &temp);
		if (command != 'p') {
			scanf("%d", &number);
			scanf("%c", &temp);
		}
		switch (command)
		{
		case 'a':
			addElem(list, number);
			break;
		case 'p':
			printList(list);
			break;
		case 'r':
			delFirst(list, number);
			break;
		}
	}
}

int main(void) {
	Node *head = NULL;
	input(&head);
	return 0;
}




