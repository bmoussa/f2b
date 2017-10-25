/* 
 * Auteur(s):
 */

#include <signal.h>
#include <stdio.h>
#include <sys/types.h>
#include <unistd.h>
#include <string.h>
#include <sys/wait.h>
#include <stdlib.h>

int msg_number=0;

void (*sig_avant)(int);		/* pour la question 4.3 */

void hdl_sys1(int n) {
  printf("hdl_sys1: Signal recu: %d\n", n);

}


void travail() {
  /* Je travaille tres intensement !    */
  /* Ne cherchez pas a comprendre ;-) */
  /* Il n'y a rien a modifier ici     */
  const char msg[] = "-\\|/";
  const int sz = strlen(msg);
  int i = 0;

  for (;;) {
    write(STDOUT_FILENO, "\r", 1);
    usleep(100000);
    write(STDOUT_FILENO, " => ", 4);
    write(STDOUT_FILENO, &msg[i++], 1);
    if (i == sz) i = 0;
  }
}
void travail() __attribute__((noreturn));
/* Petit raffinement pour le compilateur: cette fonction ne termine pas */


void my_handler_int(int signal){
	printf("returned %d\n", signal);
	printf("%s\n", msg_number==0? "caio" : "bouba");
}

void my_handler_quit(int signal){
	printf("returned %d\n", signal);
	if(msg_number == 0)
		msg_number = 1;
	else
		msg_number = 0;
	// msg_number = msg_number==0 ? 1 : 0;
}

int main() {
	printf("PID: %d\n", getpid());
	
	// redirect handlers to own handlers
	signal(SIGINT, my_handler_int);
	signal(SIGQUIT, my_handler_quit);
	
	travail();
}
