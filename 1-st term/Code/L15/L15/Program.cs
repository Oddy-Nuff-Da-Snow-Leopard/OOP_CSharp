using System;
using System.Diagnostics;
using System.Threading; 
using System.IO;
using System.Text;

namespace L15
{
    class Program
    {
        static object locker = new object();
        static void Main(string[] args)
        {
            // Task 1.
            try
            {
                using (var writer = new StreamWriter(@"D:\processes.txt", false, Encoding.Default))
                {
                    foreach (var process in Process.GetProcesses())
                    {
                        writer.WriteLine($"Name: {process.ProcessName}, ID: {process.Id}, base priority: {process.BasePriority}");
                        writer.WriteLine($"Start time: {process.StartTime}, total processor time: {process.TotalProcessorTime} ");
                        writer.WriteLine($"Amount of virtual memory: {process.VirtualMemorySize64} byte");
                        writer.WriteLine();
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
            Console.Clear();


            // Task 2.
            GetDomainInfo(AppDomain.CurrentDomain);

            var secondaryDomain = AppDomain.CreateDomain("Secondary domain");
            secondaryDomain.Load("mscorlib"); //?
            AppDomain.Unload(secondaryDomain);

            Console.ReadKey();
            Console.Clear();


            // Task 3.
            var currentThread = Thread.CurrentThread;
            currentThread.Name = "Primary";
            GetThreadInfo(currentThread);

            Console.Write("Enter n: ");
            var n = Convert.ToUInt32(Console.ReadLine());

            var myThread = new Thread(new ParameterizedThreadStart(FindPrimeNumbers))
            {
                Name = "Secondary"
            };

            GetThreadInfo(myThread);
            myThread.Start(n);
            GetThreadInfo(myThread);

            for (uint i = 0; i <= n; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Primary thread: {i}");
                Console.ResetColor();
                Thread.Sleep(50);

                if (i == n / 2)
                    myThread.Join();
            }

            GetThreadInfo(myThread);
            GetThreadInfo(currentThread);

            Console.ReadKey();
            Console.Clear();


            // Task 4.
            var oddNumbersThread1 = new Thread(new ParameterizedThreadStart(OddNumbers1));
            var evenNumbersThread1 = new Thread(new ParameterizedThreadStart(EvenNumbers1));

            oddNumbersThread1.Priority = ThreadPriority.Highest;
            oddNumbersThread1.Start(n);
            evenNumbersThread1.Start(n);

            Console.ReadKey();
            Console.WriteLine("\n");

            var path = @"D:\oddAndEvenNumbers.txt";
            if (File.Exists(path))
                File.Delete(path);

            var oddNumbersThread2 = new Thread(new ParameterizedThreadStart(OddNumbers2));
            var evenNumbersThread2 = new Thread(new ParameterizedThreadStart(EvenNumbers2));

            evenNumbersThread2.Start(n);
            oddNumbersThread2.Start(n);

            Console.ReadKey();
            Console.WriteLine("\n");

            for (uint i = 1; i <= n; i++) 
            {
                if (i % 2 == 0) 
                {
                    var evenNumbersThread3 = new Thread(new ThreadStart(EvenNumbers3));
                    evenNumbersThread3.Start();
                    Thread.Sleep(60);
                }

                else
                {
                    var oddNumbersThread3 = new Thread(new ThreadStart(OddNumbers3));
                    oddNumbersThread3.Start();
                    Thread.Sleep(60);
                }
            }

            Console.ReadKey();
            Console.Clear();


            // Task 5.
            var timerCallback = new TimerCallback(SayHello);
            var timer = new Timer(timerCallback, null, 2000, 500);

            Console.ReadKey();
        }


        // Task 2.
        private static void GetDomainInfo(AppDomain domain)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Information about application domain");
            Console.ResetColor();
            Console.WriteLine($"Name: {domain.FriendlyName}");
            Console.WriteLine($"Setup information: {domain.SetupInformation}");
            Console.WriteLine($"Base directory: {domain.BaseDirectory}");
            Console.WriteLine($"Id: {domain.Id}");

            foreach (var assembly in domain.GetAssemblies())
                Console.WriteLine(assembly.GetName().Name);
        }


        // Task 3.
        private static void GetThreadInfo(Thread thread)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Information about {thread.Name} thread:");
            Console.ResetColor();
            Console.WriteLine($"Is alive: {thread.IsAlive}");
            if (thread.IsAlive)
                Console.WriteLine($"Priority: {thread.Priority}");
            Console.WriteLine($"State: {thread.ThreadState}");
            Console.WriteLine($"Managed Id: {thread.ManagedThreadId}");
            Console.WriteLine();
        }

        private static void FindPrimeNumbers(object obj)
        {
            uint n = (uint)obj;
            var sieve = new uint[++n];
            for (uint i = 0; i < n; i++)
                sieve[i] = i;

            try
            {
                using (var writer = new StreamWriter(@"D:\Prime numbers.txt", false, Encoding.Default))
                {
                    for (uint p = 2; p < n; p++)
                    {
                        if (sieve[p] != 0)
                        {
                            writer.Write($"{sieve[p]} ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"Secondary thread: {sieve[p]}");
                            Console.ResetColor();
                            for (var j = p * p; j < n; j += p)
                                sieve[j] = 0;
                            Thread.Sleep(200);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        //Task 4.
        private static void OddNumbers1(object obj)
        {
            uint n = (uint)obj;

            for (uint i = 1; i <= n; i += 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{i} ");
                Console.ResetColor();
                Thread.Sleep(50);
            }
        }

        private static void EvenNumbers1(object obj)
        {
            uint n = (uint)obj;

            for (uint i = 2; i <= n; i += 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write($"{i} ");
                Console.ResetColor();
                Thread.Sleep(100);
            }
        }

        private static void OddNumbers2(object obj)
        {
            lock (locker)
            {
                uint n = (uint)obj;

                try
                {
                    using (var writer = new StreamWriter(@"D:\oddAndEvenNumbers.txt", true, Encoding.Default))
                    {
                        writer.WriteLine("Odd Numbers:");
                        for (uint i = 1; i <= n; i += 2)
                        {
                            writer.Write($"{i} ");
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write($"{i} ");
                            Console.ResetColor();
                            Thread.Sleep(50);
                        }
                        writer.WriteLine();
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void EvenNumbers2(object obj)
        {
            lock (locker)
            {
                uint n = (uint)obj;

                try
                {
                    using (var writer = new StreamWriter(@"D:\oddAndEvenNumbers.txt", true, Encoding.Default))
                    {
                        writer.WriteLine("Even Numbers:");
                        for (uint i = 2; i <= n; i += 2)
                        {
                            writer.Write($"{i} ");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write($"{i} ");
                            Console.ResetColor();
                            Thread.Sleep(100);
                        }
                        writer.WriteLine();
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static uint i1 = 1;
        private static void OddNumbers3()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"{i1} ");
            i1 += 2;
            Console.ResetColor();
            Thread.Sleep(50);
        }

        static uint i2 = 2;
        private static void EvenNumbers3()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{i2} ");
            i2 += 2;
            Console.ResetColor();
            Thread.Sleep(100);
        }


        // Task5.
        private static Random rnd = new Random();
        private static void SayHello(object obj)
        {
            Console.ForegroundColor = (ConsoleColor)rnd.Next(1, 16);
            Console.WriteLine("Hello from timer!");
            Console.ResetColor();
        }
    }
}