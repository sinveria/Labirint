using System;
using System.Collections.Generic;


namespace Labirint
{
    class Program
    {
        const int n = 10;
        const int m = 10;
        
        static void Main(string[] args)
        {
            char[,] rez = Labirint();

            List<int[]> sp = Way(ref rez,5,5); //границы
            if(!(sp==null))
            {
                foreach (int[] a in sp)
                {
                    Console.WriteLine("{0} = {1}", a[0], a[1], a.ToString());
                }
            }

            Console.WriteLine("==========");
            for (int i = 0; i < m; i++)
            {
                string s = "";
                for (int ii = 0; ii < n; ii++)
                {
                    s += rez[ii, i];
                }
                Console.WriteLine(s);
            }
            //Console.WriteLine(Way(ref rez,2,9)); //границы
            Console.ReadLine();
        }
        public static List<int[]> Way(ref char[,] lab, int x, int y)
        {
            lab[x,0] = ' ';
            lab[y, m-1] = ' ';
            int[,] mat = new int[n, m];
            for (int i = 0; i < m; i++)
            {
                for (int ii = 0; ii < n; ii++)
                {
                    mat[ii, i] = 0;
                }
            }
            int sh = 1;
            mat[x, 0] = sh;

            while(mat[y,m-1] == 0 && rash(ref mat, ref lab, sh)) sh++;
            if (mat[y, m - 1] == 0)
            {
                Console.WriteLine("Тупик");
                return null;
            }
            Console.WriteLine("Цель достигнута");
            List<int[]> lis = new List<int[]>();
            int[] point = new int[2] { y, m - 1 };
            lis.Add(point);
            int xx = point[0];
            int yy = point[1];
            int dx = 0;
            int dy = 0;
            while (!(xx == x && yy == 0))
            {
                int zz = mat[xx,yy];
                if (yy < m - 1 && mat[xx, yy + 1] != 0 && mat[xx,yy + 1] < zz)
                {
                    dx = 0;
                    dy = 1;
                    zz = mat[xx + dx, yy + dy];
                }
                if (yy > 0  && mat[xx, yy - 1] != 0 && mat[xx, yy - 1] < zz)
                {
                    dx = 0;
                    dy = - 1;
                    zz = mat[xx + dx, yy + dy];
                }
                if (xx > 0 && mat[xx-1, yy] != 0 && mat[xx -1, yy] < zz)
                {
                    dx = -1;
                    dy = 0;
                    zz = mat[xx + dx, yy + dy];
                }
                if (xx < n-1 && mat[xx + 1, yy] != 0 && mat[xx + 1, yy] < zz)
                {
                    dx = 1;
                    dy = 0;
                    zz = mat[xx + dx, yy + dy];
                }
                xx += dx;
                yy += dy;
                lis.Add(new int[2] { xx, yy });
            }
            Console.WriteLine("Точка выхода x = {0} y={1}", y, m - 1);
            // return true;

            for (int i = 0; i < m; i++)
            {
                string s = "";
                for (int ii = 0; ii < n; ii++)
                {
                    s += (mat[ii, i].ToString() + "!");
                }
                Console.WriteLine(s);
            }
            return lis;
        }

        public static bool rash(ref int[,] mat, ref char[,] lab, int x)
        {
            bool f = false;
            for (int i = 0; i < m; i++)
            {
                for (int ii = 0; ii < n; ii++)
                {
                    if(mat[ii, i] == x)
                    {
                        f = true;
                        if(i < m-1 && mat[ii,i+1] == 0 && lab[ii, i + 1] == ' ')
                        {
                            mat[ii, i + 1] = x + 1;
                            lab[ii, i + 1] = '▼';
                        }
                        if (i > 0 && mat[ii, i - 1] == 0 && lab[ii, i - 1] == ' ')
                        {
                            mat[ii, i - 1] = x + 1;
                            lab[ii, i - 1] = '▼';
                        }
                        if (ii > 0 && mat[ii-1, i] == 0 && lab[ii-1, i] == ' ')
                        {
                            mat[ii-1,i] = x + 1;
                            lab[ii-1,i] = '▼';
                        }
                        if (ii < n-1 && mat[ii+1, i] == 0 && lab[ii+1, i] == ' ')
                        {
                            mat[ii+1, i] = x + 1;
                            lab[ii+1, i] = '▼';
                        }
                    }
                }
            }
            return f;
        }

        public static char[,] Labirint()
        {
            //n столбцы, m строки
            char[,] lab = new char[n, m];
            for (int i = 0; i < n; i++)
            {
                lab[i, 0] = '■';
            }
            //заполняются строки между первой и последней линией
            for (int i = 1; i < m-1; i++)
            {
                lab[0,i] = '■';
                for (int ii = 1; ii < n-1; ii++)
                {
                    lab[ii, i] = ' ';
                }
                lab[n-1, i] = '■';
            }
            for (int i = 0; i < n; i++)
            {
                lab[i, m-1] = '■';
            }

            Random ram = new Random();
            for (int i = 0; i < (n-2)*(m-2)*0.3; i++)
            {
                int st = ram.Next(1, n - 1);
                int sr = ram.Next(1, m - 1);
                lab[st, sr] = '■';
            }
            return lab;
        }
    }
}
