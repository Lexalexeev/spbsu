/*
 * Exponentiation
   Alekseev Aleksei, group 171.
*/


#include <stdio.h>

int power (int a, int n)
{
   int result = 1;
      while (n) {
          if (n & 1) {
            result *= a;
          }
          a *= a;
          n >>= 1;
      }
    return result;
}

int main(void)
{
    int a,n;
    scanf("%d%d", &a, &n);
    printf("%d", power(a,n));
    return 0;
}

