/*
 * Binary representation
   Alekseev Aleksei, group 171.
*/


#include <stdio.h>

int binFunction(int n, int i) {
    return ((n >> i) & 1);
}

int main(void) {
    int n;
    scanf("%d",&n);
    int i;
    for (i = 31; i >= 0; i--) {
        printf("%d",binFunction(n,i));
    }
    return 0;
}

