혼자서도 쉽게 익힐 수 있는 자기주도 C언어 프로그래밍
page. 126
형평 4


#include <stdio.h>

int main() {
    int n, cnt = 0;

    while (1) {
        scanf("%d", &n);

        if (n == 0) break;

        if (n % 3 != 0 && n % 5 != 0) {
            cnt++;
        }
    }

    printf("%d\n", cnt);

    return 0;
}


------------------------------------------------------------------------


p.127
형평 5


#include<stdio.h>
int main() {

    int b, h;
    double t;
    char y;

    do {
        printf("Base = ");
        scanf("%d", &b);

        printf("Height = ");
        scanf("%d", &h);

        t = (double)b * h / 2;
        printf("Triangle width = %.1f\n", t);

        printf("Continue? ");
        scanf(" %c", &y);


    } while ((y == 'y') || (y == 'Y'));
    return 0;
}
------------------------------------------------------------------------