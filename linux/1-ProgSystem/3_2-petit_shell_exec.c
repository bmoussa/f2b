// https://stackoverflow.com/questions/32771494/how-can-i-use-execl-function-or-other-kinds-of-exec-functions-to-perform-ech
// http://www.programacaoprogressiva.net/2014/09/A-Chamada-de-Sistema-fork-Como-Criar-e-Gerenciar-Processos.html

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
	// using execlp instead of execl to search for the executable in PATH
	execlp(tab, tab, NULL);
    } else {
	wait(NULL);
    }
  }
}
