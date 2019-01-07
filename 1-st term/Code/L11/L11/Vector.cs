using System;
using System.Collections;

namespace L11
{
    class Vector : IEnumerable
    {
        public int[] vector { get; private set; }
        public ushort size { get; private set; }
        private bool isNull;

        private static Random rnd;

        static Vector()
        {
            rnd = new Random();
        }

        public Vector(ushort size)
        {
            this.size = size;
            vector = new int[size];
            isNull = true;
        }

        public Vector(int[] array)
        {
            vector = array;
            size = (ushort)vector.Length;
            isNull = false;
        }

        public Vector(ushort size, bool makeItRandom) : this(size)
        {
            if (makeItRandom)
            {
                for (var i = 0; i < vector.Length; i++)
                    vector[i] = rnd.Next(-20, 20);
                isNull = false;
            }
        }

        public void Print()
        {
            if (isNull)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"Vector: ");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write($"Vector: ");
                Console.ResetColor();
            }

            Console.Write("(");
            for (var i = 0; i < vector.Length; i++)
            {

                if (i != vector.Length - 1)
                    Console.Write($"{vector[i]}, ");

                else Console.Write(vector[i] + ")");
            }
            Console.WriteLine();
        }


        public IEnumerator GetEnumerator()
        {
            return vector.GetEnumerator();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Size: {size}");
            Console.WriteLine($"Is null: {isNull}");
            Console.WriteLine();
        }

        public bool IsWithZeroelement()
        {
            foreach (var element in vector)
                if (element == 0)
                    return true;

            return false;
        }

        public double CountModule()
        {
            if (!isNull)
            {
                double module = 0;
                foreach (var element in vector)
                    module += Math.Pow(element, 2);

                return module;
            }

            else
                return 0;
        }
    }
}
