using System;

namespace L3
{
    class Vector 
    {
        protected int size;
        public int[] vector;

        public int Size
        {
            set
            {
                if (value < 0)
                    Console.WriteLine("Error, entered a negative number!");
                
                else
                    size = value;
            }
            get { return size; }
        }

        private void CreateVector(int size)
        {
            vector = new int[size];
            Console.WriteLine("Enter vector elements:");
            for (var i = 0; i < size; i++)
                vector[i] = Convert.ToInt32(Console.ReadLine());
        }

        public void PrintVector()
        {
            Console.Write("{ ");
            for (var i = 0; i < size; i++)
            {
                if (i != size - 1)
                    Console.Write($"{vector[i]}, ");

                else Console.WriteLine(vector[i] + " }");
            }
        }

        public double module;
        public bool isWithZero;

        private void FindOutModule()
        {
            double sumOfSquares = 0;
            for (var i = 0; i < size; i++)
                sumOfSquares += Math.Pow(vector[i], 2);
            module = Math.Sqrt(sumOfSquares);
        }

        private void CheckForZero()
        {
            for (var i = 0; i < size; i++)
            {
                if (vector[i] == 0) 
                    isWithZero = true;
            }
        }

        public void newObj()
        {
            CreateVector(size);
            Console.Write("Entered vector: ");
            PrintVector();
            CheckForZero();
            FindOutModule();
        }

        public int[] MultiplicationByNumber(int number)
        {
            var newV = new int[vector.Length];
            for (var i = 0; i < vector.Length; i++)
                newV[i] = vector[i] * number;
            return newV;
        }

        public int[] AdditionOfTwoVectors(int[] secondVector)
        {
            var newV = new int[secondVector.Length];
            for (var i = 0; i < secondVector.Length; i++)
                newV[i] = vector[i] + secondVector[i];
            return newV;
        }

        public int[] MultiplicationOfTwoVectors(int[] secondVector)
        {
            var newV = new int[secondVector.Length];
            for (var i = 0; i < secondVector.Length; i++)
                newV[i] = vector[i] * secondVector[i];
            return newV;
        }
    }
    

    class Program
    {
        private static void Print(int[] newV)
        {
            Console.Write("{ ");
            for (var i = 0; i < newV.Length; i++)
            {
                if (i != newV.Length - 1)
                    Console.Write($"{newV[i]}, ");

                else Console.WriteLine(newV[i] + " }\n");
            }
        }

        private static bool Checkup(int[] V1, int[] V2)
        {
            if (V1.Length == V2.Length)
                return true;
            else return false;
        }
        

        static void Main(string[] args)
        {
            // Declaration of an array of objects and filling it with the keyboard.
            Console.Write("Enter how many vectors you need to create: ");
            int numberOfVectors = Convert.ToInt32(Console.ReadLine());
            var V = new Vector[numberOfVectors];
            for (var i = 0; i < numberOfVectors; i++)
            {
                V[i] = new Vector();
                Console.Write($"\nEnter now many elements will be in the vector {i + 1}: ");
                V[i].Size = Convert.ToInt32(Console.ReadLine());
                if (V[i].Size > 0)
                    V[i].newObj();
                else i--;
            }

            // Check for at least one ZERO element.
            bool flag = false;
            for (var i = 0; i < numberOfVectors; i++)
            {
                if (V[i].isWithZero)
                {
                    flag = true;
                    break;
                }
            }

            // If there is at least one vector with ZERO element, then output it (them).
            if (flag)
            {
                Console.WriteLine("\nList of vectors containing element 0:");

                for (var i = 0; i < numberOfVectors; i++)
                {
                    if (V[i].isWithZero)
                    {
                        Console.Write($"Vector {i + 1}: ");
                        V[i].PrintVector();
                    }
                }
            }
            else Console.WriteLine("\nNone of the vectors contains element 0!");


            // Search vector with the smallest module.
            var min = V[0].module;
            int k = 0;
            for (var i = 1; i < numberOfVectors; i++)
            {
                if (min > V[i].module)
                {
                    min = V[i].module;
                    k = i;
                }
            }
            Console.WriteLine($"\nThe vector with the smallest module is vector {k + 1}, is's value is {min}");


            Console.ReadKey();
            Console.Clear();

            char choice;
            do
            {
                Console.WriteLine("Enter a to multiply the vector by the number;");
                Console.WriteLine("Enter b to add two vectors;");
                Console.WriteLine("Enter c to multiply two vectors;");
                Console.WriteLine("Enter q to exit.");
                Console.Write("Your choice: ");
                choice = Convert.ToChar(Console.ReadLine());
                if (choice == 'a' || choice == 'b' || choice == 'c')
                {
                    Console.WriteLine("\nAll vectors:");
                    for (var i = 0; i < numberOfVectors; i++)
                    {
                        Console.Write($"Vector {i + 1}: ");
                        V[i].PrintVector();
                    }
                }
                else break;

                switch (choice)
                {
                    case 'a':
                        {
                            Console.Write("Select vector: ");
                            int n = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Select number: ");
                            int number = Convert.ToInt32(Console.ReadLine());

                            var newV = V[--n].MultiplicationByNumber(number);
                            Console.Write($"Multiply the vector {++n} by the number {number}: ");
                            Print(newV);
                            break;
                        }

                    case 'b':
                    case 'c':
                        {
                            Console.Write("Select first vector: ");
                            int n = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Select second vector: ");
                            int m = Convert.ToInt32(Console.ReadLine());

                            if (Checkup(V[--n].vector, V[--m].vector))
                            {
                                if (choice == 'b')
                                {
                                    var newV = V[n].AdditionOfTwoVectors(V[m].vector);
                                    Console.Write($"Addittion of vector {++n} and {++m}: ");
                                    Print(newV);
                                }

                                if (choice == 'c')
                                {
                                    var newV = V[n].MultiplicationOfTwoVectors(V[m].vector);
                                    Console.Write($"Multiplication of vector {++n} and {++m}: ");
                                    Print(newV);
                                }
                            }
                            else
                                Console.WriteLine("Vectors lengths don't match");
                            break;
                        }
                }
                Console.ReadKey();
                Console.Clear();
            } while (choice != 'q');
        }
    }
}
