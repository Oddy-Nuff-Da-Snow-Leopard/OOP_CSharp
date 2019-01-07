
namespace MySpace
{
    public static class StringExtension
    {
        public static ushort CountDuplicate(this string str, char symbol)
        {
            ushort counter = 0;
            foreach (ushort i in str)
            {
                if (i == symbol)
                    counter++;
            }
            return counter;
        }

        public static string RemoveDuplicate(this string str, char symbol)
        {
            for (ushort i = 0; i < str.Length; i++)
                if (str[i] == symbol)
                {
                    str = str.Remove(i, 1);
                    i--;
                }
            return str;
        }
    }

    public static class ODArrayExtension
    {
        public static double FindAvgSum(this ODArray a)
        {
            short sum = 0;
            foreach (short i in a.Array)
                sum += i;
            return sum / a.Length;
        }

        public static ODArray Abs(this ODArray a)
        {
            for(ushort i = 0; i < a.Length; i++)
            {
                if (a.Array[i] < 0)
                    a.Array[i] *= -1;
            }
            return a;
        }
    }
}