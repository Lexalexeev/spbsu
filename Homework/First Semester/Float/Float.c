/*
* Float
Alekseev Aleksei, group 171.
*/

#include <stdio.h>
#include <string.h>

struct floatNumber {
	int sign;
	int exponent;
	int mantissa;
} fN;

void print(floatNumber) {
	const int maxNum = 255;
	if (fN.sign) 
		fN.sign = -1; 
	else 
		fN.sign = 1;
	if (fN.exponent == 0 && fN.mantissa == 0) 
		printf("Zero\n");
	else if (fN.exponent == maxNum && fN.mantissa == 0) {
		if (fN.sign > 0) 
			printf("+Infinity\n");
		else 
			printf("-Infinity\n");
	}
	else if (fN.exponent == maxNum && fN.mantissa != 0) 
		printf("NaN\n");
	else 
		printf("%d * 2^%d * %f\n", fN.sign, fN.exponent - 127, 1 + ((float)fN.mantissa) / (1 << 23));
}

void calculate(int bits) {
	int sign = (bits >> 31) & 1;
	int exponent = (bits >> 23) & ((1 << 8) - 1);
	int mantissa = bits & ((1 << 23) - 1);
	fN.sign = sign;
	fN.exponent = exponent;
	fN.mantissa = mantissa;
	print(fN);
}

void firstWay() {
	float number;
	scanf("%f", &number);
	calculate(*(int *)(&number));
}

void secondWay() {
	union {
		float floatValue;
		int intValue;
	} floatNum;
    scanf("%f", &floatNum.intValue);
	calculate(floatNum.intValue);
}

void thirdWay() {
	union {
		float floatValue;
		struct {
			unsigned s : 1;
			unsigned e : 8;
			unsigned m : 23;	
		} bitField;
	} floatNum;
    scanf("%f", &floatNum.floatValue);
	fN.sign = floatNum.bitField.s;
	fN.exponent = floatNum.bitField.e;
	fN.mantissa = floatNum.bitField.m;
	print(fN);
}

int main(void) {
	firstWay();
	secondWay();
	thirdWay();
	scanf("%d");
	return 0;
}