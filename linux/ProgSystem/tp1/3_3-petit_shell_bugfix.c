// https://linux.die.net/man/3/explain_execlp

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>

#define TABSIZE 512

extern char **environ;

int main() {
  int pid, longueur;
  char tab[TABSIZE], *s;

  while(1) {
    fputs("petit_shell...> ", stdout);	/* Affichage d'un prompt */
    s = fgets(tab, TABSIZE, stdin);

    if (s == NULL) {
      exit(EXIT_SUCCESS);
    }

    longueur = strlen(s);
    tab[longueur - 1] = '\0';

    pid = fork();

    /* Actions:
     * 
     * Si dans pere alors
     *   wait(NULL);
     * sinon alors
     *   execution de la commande recuperee dans tab;
     *   message d'erreur: fprintf(stderr, "Erreur dans le exec\n")
     * fin si
     */

    // child has pid == 0
    if(pid==0) { 
	printf("tab=%s\n", tab);

	// exit when exec gives an error
	if (execlp(tab, tab, NULL) < 0) {
		fprintf(stderr, "deu ruim\n"); 
		exit(EXIT_FAILURE);
	}
    } else {
	wait(NULL);
    }
  }
}
