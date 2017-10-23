// http://www0.cs.ucl.ac.uk/staff/ucacbbl/getenv/

#include <stdio.h>
// need stdlib.h to use getenv()
#include <stdlib.h>

int main(int argc, char **argv) {
	const char* s = getenv(argv[1]);
	printf("PATH :%s\n",(s!=NULL)? s : "getenv returned NULL");
	return 0;
}
