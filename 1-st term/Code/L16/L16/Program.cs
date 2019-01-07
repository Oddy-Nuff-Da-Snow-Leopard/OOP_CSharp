using System;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace L16
{
    class Program
    {
        private static Random rnd = new Random();

        private static int[,] CreateMatrix(ushort n, ushort m)
        {
            var matrix = new int[n, m];
            for (ushort i = 0; i < n; i++)
                for (ushort j = 0; j < m; j++)
                    matrix[i, j] = (short)rnd.Next(short.MinValue, short.MaxValue);

            return matrix;
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (ushort i = 0; i < matrix.GetLength(0); i++)
            {
                for (ushort j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]}\t");
                }
                Console.WriteLine("\n");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            // Task 1.
            Console.Write("Enter the number of rows in the first matrix: ");
            var n1 = Convert.ToUInt16(Console.ReadLine());
            Console.Write("Enter the number of columns in the first matrix: ");
            var m1 = Convert.ToUInt16(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Enter the number of rows in the second matrix: ");
            var n2 = Convert.ToUInt16(Console.ReadLine());
            Console.Write("Enter the number of columns in the second matrix: ");
            var m2 = Convert.ToUInt16(Console.ReadLine());
            Console.WriteLine();


            if (m1 == n2)
            {
                var matrixA = CreateMatrix(n1, m1);
                var matrixB = CreateMatrix(n2, m2);

                var stopWatcher = new Stopwatch();
                var task1 = new Task(() =>
                {
                    var matrixC = new int[matrixA.GetLength(0), matrixB.GetLength(1)];
                    for (ushort i = 0; i < matrixA.GetLength(0); i++)
                    {
                        for (ushort j = 0; j < matrixB.GetLength(1); j++)
                        {
                            for (ushort k = 0; k < matrixB.GetLength(0); k++)
                                matrixC[i, j] += matrixA[i, k] * matrixB[k, j];
                        }
                    }
                });

                stopWatcher.Start();
                task1.Start();
                task1.Wait();
                stopWatcher.Stop();

                Console.WriteLine($"Task ID: {task1.Id}");
                Console.WriteLine($"Is completed: {task1.IsCompleted}");
                Console.WriteLine($"Status: {task1.Status}");
                Console.WriteLine($"Lead time: {(double)stopWatcher.ElapsedMilliseconds / 1000.0} sec.");

                Console.ReadKey();
                Console.Clear();


                // Task 2.
                var cancelTokenSource = new CancellationTokenSource();
                var token = cancelTokenSource.Token;

                var task2 = new Task(() =>
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Task canceled with token!");
                        return;
                    }
                    var matrixC = new int[matrixA.GetLength(0), matrixB.GetLength(1)];
                    for (ushort i = 0; i < matrixA.GetLength(0); i++)
                    {
                        for (ushort j = 0; j < matrixB.GetLength(1); j++)
                        {
                            for (ushort k = 0; k < matrixB.GetLength(0); k++)
                                matrixC[i, j] += matrixA[i, k] * matrixB[k, j];
                        }
                    }
                });

                
                task2.Start();
                cancelTokenSource.Cancel();
                Console.Read();
            }

            else
                Console.WriteLine("Matrices can't be multiplied!");


            // Task 3.

            Console.ReadLine();
            Console.WriteLine("\t Задача о нахождении площади круга. \n Введите радиус:");
            int r = Convert.ToInt32(Console.ReadLine());
            Task<double> task3_1 = new Task<double>(() => Square(r));
            Task<double> task3_2 = new Task<double>(() => GetPI());
            Task<ushort> task3_3 = new Task<ushort>(() => Amount());
            task3_1.Start();
            task3_2.Start();
            task3_3.Start();
            Task.WaitAll();
            double result_3 = 0;
            Task<double> task3_4 = new Task<double>(() => (result_3 = 2 * task3_1.Result * task3_2.Result * task3_3.Result));
            task3_4.Start();
            Task.WaitAll();
            Thread.Sleep(1000);
            Console.WriteLine("Площадь равна: " + result_3);

            // Task 4.
            Console.ReadKey();
        }

        public static double Square(double r)
        {
            return Math.Pow(r, 2);
        }
        public static double GetPI()
        {
            return Math.PI;
        }
        public static ushort Amount()
        {
            Console.Write("Enter amount of numbers: ");
            return Convert.ToUInt16(Console.ReadLine());
        }

    }
}