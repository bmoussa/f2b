#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"

#if defined(__GNUC__) && (__GNUC__ >= 3)
#define ATTRIBUTE(x) __attribute__(x)
#else
#define ATTRIBUTE(x) /**/
#endif

#define MIN(_x, _y) ((_x) > (_y) ? (_y) : (_x))
#define ABS(_x) ((_x) >= 0 ? (_x) : -(_x))


/* N'hesitez pas a changer MAXX .*/
#define MAXX  50
#define MAXY (MAXX * 3 / 4)

#define NX (2 * MAXX + 1)
#define NY (2 * MAXY + 1)

#define NBITER 550
#define DATATAG 150

static int mandel(double, double);

int dump_ppm(const char *, int[NX][NY]);
int cases[NX][NY];


void print_array(int *v, int n){
	int i;
	printf("[%d", v[0]);
	for(i=1; i <n; i++) {
		printf(",%d", v[i]);
	}
	printf("]\n");
}


int main(int argc, char *argv[])
{
  MPI_Status status;
  int i,j, num, rank, size, nbslaves;
  char inputstr [100],outstr [100];

  /* Start up MPI */

  MPI_Init(&argc, &argv);
  MPI_Comm_rank(MPI_COMM_WORLD, &rank);
  MPI_Comm_size(MPI_COMM_WORLD, &size);

  nbslaves = size -1;

  if (rank == 0) {

    int res[NY];

    /* Begin User Program  - the master */

   for(i = -MAXX; i <= MAXX; i++) {

	// my error: I tried to exchange data this way (using pointers) but I found that I cant use pointers because im using two different machines, with differente memories
	// MPI_Recv(res, 1, MPI_INT, 1, DATATAG, MPI_COMM_WORLD, &status);
    
    	// correction:
	MPI_Recv(res, NY, MPI_INT, 1, DATATAG, MPI_COMM_WORLD, &status);
    
	printf("master %d received results = ", rank);
	print_array(res, NY);
    
    for(j = -MAXY; j <= MAXY; j++) {
      cases[i + MAXX][j + MAXY] = res[j + MAXY];
    }
   }
    dump_ppm("mandel.ppm", cases);
    printf("Fini.\n");
  }

  else {

    /* On est l'un des fils */
    double x, y;
    int i, j, res[NY], rc;
    for(i = -MAXX; i <= MAXX; i++) {
      for(j = -MAXY; j <= MAXY; j++) {
	x = 2 * i / (double)MAXX;
	y = 1.5 * j / (double)MAXY;
	printf("son %d is using (x, y) = (%f, %f) \n", rank, x, y);
	
	res[j + MAXY] = mandel(x, y);
      }
      printf("son %d sends results = ", rank);
      print_array(res, NY);
      MPI_Send(res, NY, MPI_INT, 0, DATATAG, MPI_COMM_WORLD); 
    }
  }

  MPI_Finalize();
  return 0;
}

/* function to compute a point - the number of iterations 
   plays a central role here */

int
mandel(double cx, double cy)
{
  int i;
  double zx, zy;
  zx = 0.0; zy = 0.0;
  for(i = 0; i < NBITER; i++) {
    double zxx = zx, zyy = zy;
    zx = zxx * zxx - zyy * zyy + cx;
    zy = 2 * zxx * zyy + cy;
    if(zx * zx + zy * zy > 4.0)
      return i;
  }
  return -1;
}

/* the image commputer can be transformed in a ppm file so
   to be seen with xv */

int
dump_ppm(const char *filename, int valeurs[NX][NY])
{
  FILE *f;
  int i, j, rc;

  f = fopen(filename, "w");
  if(f == NULL) { perror("fopen"); exit(1); }
  fprintf(f, "P6\n");
  fprintf(f, "%d %d\n", NX, NY);
  fprintf(f, "%d\n", 255);
  for(j = NY - 1; j >= 0; j--) {
    for(i = 0; i < NX; i++) {
      unsigned char pixel[3];
      if(valeurs[i][j] < 0) {
	pixel[0] = pixel[1] = pixel[2] = 0;
      } else {
	unsigned char val = MIN(valeurs[i][j] * 12, 255);
	pixel[0] = val;
	pixel[1] = 0;
	pixel[2] = 255 - val;
      }
      rc = fwrite(pixel, 1, 3, f);
      if(rc < 0) { perror("fwrite"); exit(1); }
    }
  }
  fclose(f);
  return 0;
}
 
