/*
 * Sign
   Alekseev Aleksei, group 171.
*/


#include <stdio.h>

int sign(int n) {
   int const dimension = 10;
   return (n >> dimension | (!!n));
}

int main(void)
{
    int n;
    scanf("%d", &n);
    printf("%d", sign(n));
    return 0;
}
