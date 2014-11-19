/*
 * FitBits
   Alekseev Aleksei, group 171.
*/


#include <stdio.h>

int fitBits(int x, int n)
{
   return !((x >> (n - 1)) + (!!(x >> (n-1))));

}


int main(void)
{
    int x, n;
    scanf("%d" "%d", &x, &n);
    printf("%d", fitBits(x, n));
    return 0;
}
