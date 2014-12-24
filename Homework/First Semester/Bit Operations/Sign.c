/*
 * Sign
   Alekseev Aleksei, group 171.
*/


#include <stdio.h>

int sign(int n) {
   return (n >> 31 | (!!n));
}

int main(void)
{
    int n;
    scanf("%d", &n);
    printf("%d", sign(n));
    return 0;
}
