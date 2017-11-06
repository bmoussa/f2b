/* 
 * Auteur(s):
 *
 * Programme a appeler avec ou sans l'option "true"
 * Lancer "top" auparavant (pour terminer taper "q").
 *
 * Avec l'option "true", le flag O_NONBLOCK est positionne ce qui rend le
 * read non bloquant (il rend -1 et errno est positionne a EAGAIN).  On
 * constate alors que la charge cpu monte...  On n'arrete pas d'appeler
 * read.
 * 
 * Sans l'option "true", la charge cpu n'augmente pas, le read est bloquant. 
 * Le process s'endort en attendant que des caracteres soient tapes au
 * clavier.
 *
 * On peut aussi lancer ce programme avec ou sans l'option "true" a l'aide
 * de la commande strace : strace read_on_delay [true].
 * 
 * Sous Solaris on utilisera truss au lieu de strace
*/

#include <signal.h>
#include <sys/fcntl.h>
#include <unistd.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>

// fd 0 it's the keyboard 

void my_handler_int(int signal){
	int flag;
	
	flag = fcntl(0, F_GETFL, 0);
	flag = fcntl(0, F_SETFL, (flag & ~O_NONBLOCK));
	
}

int main(int argc, char **argv) {
  int flag, r;
  char buf[10];
  
  signal(SIGINT, my_handler_int);

  if ((argc > 1) && (strcmp(argv[1], "true") == 0)) {
    // 0 is useless 
    flag = fcntl(0, F_GETFL, 0);
    if (flag < 0) {
      perror("Fcntl F_GETFL");
      exit(EXIT_FAILURE);
    }
    /* positionner maintenant le flag avec O_NONBLOCK */
    // we want to set the flag O_NONBLOCK true zithout changing the others
    // so we do a logical OR between the old flag and the O_NONBLOCK flag
    flag = fcntl(0, F_SETFL, (flag | O_NONBLOCK) );

    if (flag < 0) {
      perror("Fcntl F_SETFL");
      exit(EXIT_FAILURE);
    }
  }

  for (;;) {
    r = read(0, buf, 10);
    if ((r > 0) && (strncmp(buf, "quit", 4) == 0)) {
      exit(EXIT_SUCCESS);
    }
  }

}

// if the flag is set to true the read is non-blocking, 
// so the program tries to re	ad the keyboard many times, and the cpu usage goes up

/*
how to test:
open a terminal with top
launch the program with the command 6_2 true
find the program in top, it may be using too much cpu
go to the program and send a ctrl+c (signal INT)
find the program in top, it may stop to using the cpu


