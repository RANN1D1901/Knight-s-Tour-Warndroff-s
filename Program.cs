using System;
using System.Windows;
namespace knighttour
{

	class Program
    {
		
		public static int N=8;
		public static int[] cx= { 1, 1, 2, 2, -1, -1, -2, -2 };
		public static int[] cy = { 2, -2, 1, -1, 2, -2, 1, -1 };

		internal class Cell
		{
			public int x;
			public int y;

			public Cell(int x, int y)
			{
				this.x = x;
				this.y = y;
			}
		}
		//Define Limits of movements of the knight.
		public bool _movements(int x,int y)
        {
			if((x>=0&&y>=0)&&(x<N&&y<N))
            {
				return true;
            }
			return false;

        }
		//Check the square the knight is moving is not empty
		public bool _isEmpty(int[] a, int x, int y)
        {
			if(_movements(x,y)&&(a[y*N+x])<0)
            {
				return true;
            }
			return false;
        }
		//Find the number of empty squares adjacent to (x,y)
		public int EmptySquares(int[] a, int x, int y)
		{
			int count = 0;
			for (int i = 0; i < N; ++i)
			{
				if (_isEmpty(a, (x + cx[i]), (y + cy[i])))
				{
					count++;
				}
			}
			return count;
		}
		//pick the next move using Warnsdorff's Heuristic approach for optimized solution returns the cell to which the knight moves
		public Cell Move(int[] a, Cell cell)
        {
			int min_index = -1;
			int c;
			int min_deg = (N + 1);
			int nx, ny;
			Random random = new Random();

			int start = random.Next(1000) % N;
			for (int count = 0; count < N; ++count)
			{
				int i = (start + count) % N;
				nx = cell.x + cx[i];
				ny = cell.y + cy[i];
				if ((_isEmpty(a, nx, ny)) &&
					(c = EmptySquares(a, nx, ny)) < min_deg)
				{
					min_index= i;
					min_deg = c;
				}
			}

			// IF we could not find a next cell since no possible move is found
			if (min_index == -1)
				return null;

			// Store coordinates of next point if the next move is possible
			nx = cell.x + cx[min_index];
			ny = cell.y + cy[min_index];

			// Mark next move on the matrix for visualisation later
			a[ny * N + nx] = a[(cell.y) * N +
							(cell.x)] + 1;

			// Update next point 
			cell.x = nx;
			cell.y = ny;

			return cell;
		}
		/* checks its neighbouring sqaures */
		/* If the knight ends on a square that is one 
		knight's move from the beginning square, 
		then tour is closed */

		public bool neighbour(int x, int y, int xx, int yy)
		{
			for (int i = 0; i < N; ++i)
				if (((x + cx[i]) == xx) &&
					((y + cy[i]) == yy))
					return true;

			return false;
		}

		/* displays the chessboard with all the 
		legal knight's moves */
		public void Draw(int[] a)
        {
			for (int i = 0; i < N; ++i)
			{
				
				for (int j = 0; j < N; ++j)
				{
					if(j%2==0)
                    {
						
						Console.BackgroundColor = ConsoleColor.Black;
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write(a[j * N + i] + "    ");
					}
					else
                    {
						Console.BackgroundColor = ConsoleColor.White;
						Console.ForegroundColor = ConsoleColor.Black;
						Console.Write(a[j * N + i] + "   ");
					}
                    

				}
					
				Console.WriteLine();
				Console.WriteLine();
			}

		}

		/* Generates the legal moves using warnsdorff's 
		heuristics. Returns false if not possible */
		public bool ReEntrantTour(int x,int y)
		{
			// Filling up the chessboard matrix with -1's 
			int[] a= new int[N * N];
			for (int i = 0; i < N * N; ++i)
				a[i] = -1;

			// initial position 
			int sx = x;
			int sy = y;

			// Current points are same as initial points 
			Cell cell = new Cell(sx, sy);

			a[cell.y * N + cell.x] = 1; // Mark first move. 

			// Keep picking next points using 
			// Warnsdorff's heuristic 
			Cell ret = null;
			for (int i = 0; i < N * N - 1; ++i)
			{
				ret = Move(a, cell);
				if (ret == null)
					return false;
			}

			// Check if tour is closed (Can end 
			// at starting point) 
			if (!neighbour(ret.x, ret.y, sx, sy))
				return false;
           
			Draw(a);
			return true;
			
		}
		public bool NonReEntrantTour(int x,int y)
        {
			// Filling up the chessboard matrix with -1's 
			int[] a = new int[N * N];
			for (int i = 0; i < N * N; ++i)
				a[i] = -1;

			// initial position 
			int sx = x;
			int sy = y;

			// Current points are same as initial points 
			Cell cell = new Cell(sx, sy);

			a[cell.y * N + cell.x] = 1; // Mark first move. 

			// Keep picking next points using 
			// Warnsdorff's heuristic 
			Cell ret = null;
			for (int i = 0; i < N * N - 1; ++i)
			{
				ret = Move(a, cell);
				if (ret == null)
					return false;
			}

		

			Draw(a);
			return true;

		}

		static void Main(string[] args)
        {
			
			Console.WriteLine("Enter the intial value of the Knight's Position on 8*8 chess board:");

			int x = Convert.ToInt32(Console.ReadLine());
			int y = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine();
			for(int i=0;i<1;i++)
            {
				while (!new Program().ReEntrantTour(x, y))
				{
					;
				}
				Console.WriteLine();
				Console.WriteLine();
				while (!new Program().NonReEntrantTour(x,y))
				i++;
				
			}
			
		}
    }
}