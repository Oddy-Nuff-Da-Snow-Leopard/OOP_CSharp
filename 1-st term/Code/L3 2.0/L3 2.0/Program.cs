using System;

namespace L3
{
    partial class Vectors
    {
        Vector[] vector;
        public Vectors(int size)
        {
            vector = new Vector[size];
        }
    }

    class Vector
    {
        #region Fields
        private static readonly int maxSize;
        private int size;

        private readonly int[] vector;
        private const double e = Math.E;

        private static int amount;
        private readonly int serialNumber;
        #endregion

        #region Properties
        public int Size
        {
            private set
            {
                if (value <= maxSize && value >= 0)
                    size = value;
                else Console.WriteLine("Error");
            }
            get { return size; }
        }

        public bool IsEmpty { get; } 
        #endregion

        #region Constructors
        static Vector()
        {
            maxSize = 20;
            amount = 0;
        }

        public Vector()
        {
            size = 0;
            vector = null;
            IsEmpty = true;
            serialNumber = ++amount;
        }

        public Vector(int[] vector)
        {
            this.vector = vector;
            Size = vector.Length;
            IsEmpty = false;
            serialNumber = ++amount;
        }
        #endregion

        public static void Info(Vector v)
        {
            if (v.size > 0)
            {
                Console.WriteLine($"Vector {v.serialNumber}:");
                foreach (var i in v.vector)
                    Console.Write(i + " ");
            }
            Console.WriteLine($"Is empty? - {v.IsEmpty}");
            Console.WriteLine($"Size of vector is {v.size}\n");
        }
    }

    partial class Vectors
    {
        public Vector this[int index]
        {
            get { return vector[index]; }
            set { vector[index] = value; }
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter how many vectors you need to create: ");
            int numberOfVectors = Convert.ToInt32(Console.ReadLine());
            Vectors vectors = new Vectors(numberOfVectors);
            vectors[0] = new Vector();
            var A = new int[] { 1, 2, 3, 4, 5 };
            vectors[1] = new Vector(A);

            Vector.Info(vectors[0]);
            Vector.Info(vectors[1]);

            Console.ReadKey();
        }
    }
}
