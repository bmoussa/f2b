
#include <stdio.h>

// system variable environ points to a list of char*, 
// which each char* is a environment variable
extern char **environ;

int main(int argc, char **argv) {
	int i;
	char *s = *environ;

	for (i=1; s; i++) {
		printf("%s\n", s);
		s = *(environ+i);
	}

	return 0;
}
