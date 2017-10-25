// https://linux.die.net/man/3/getpwuid

/*
 * Auteur(s):
 *
 * Cet programme refait ce que fait la commande "ls". Il donne des
 * informnations sur les caracteristiques de fichiers dont le nom est passe
 * en parametre.
 *
 * Utilisation de la primitive stat(2) et de la fonction getpwuid(3).
 */

#include <sys/types.h>
#include <sys/stat.h>
#include <pwd.h>
#include <stdio.h>
#include <stdlib.h>

/* Petite fonction qui se charge d'envoyer les messages d'erreur
   et qui ensuite "suicide" le processus. */

/*
struct passwd {
    char   *pw_name;       // username
    char   *pw_passwd;     // user password
    uid_t   pw_uid;        // user ID 
    gid_t   pw_gid;        // group ID
    char   *pw_gecos;      // user information
    char   *pw_dir;        // home directory
    char   *pw_shell;      // shell program
};
*/

void erreur_grave(char *msg) {
  perror(msg);
  exit(EXIT_FAILURE);
}

/* Fonction principale (fournie avec erreur(s?)) */

int main(int argc, char **argv) {
  struct stat status, *buffer;
  int r;
  char *username, *fake_user;

  // ex 1.1 = initialize pointer before using it
  buffer = &status;

  r = stat(argv[1], buffer);
  if (r < 0)
    erreur_grave("Stat");

  // ex 1.2 = get process username from userid
  username = getpwuid(buffer->st_uid)->pw_name;

  printf("Fichier %s:  mode: %X  Taille: %ld  Proprietaire: %s\n",
	argv[1], buffer->st_mode, buffer->st_size, username);

  // ex 1.3 = try a fake id
  // obs: as the variable username is a pointer, calling the function getpwuid(1000) also modifies username
  fake_user = getpwuid(1000)->pw_name; 
  printf("Fake user: %s  Proprietaire: %s\n", fake_user, username);

  // ex 1.4 = because the function stat() returns only the owner id, 
  exit(EXIT_SUCCESS);
}
