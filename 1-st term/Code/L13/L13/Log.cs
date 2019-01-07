using System;
using System.IO;
using System.Text;

namespace L13
{
    static class Log
    {
        static string path = @"D:\C#\Logfile.txt";

        public static void RecordInfo(string place, string info)
        {
            using (var writer = new StreamWriter(path, true, Encoding.Default))
            {
                writer.WriteLine(DateTime.Now + "     " + place + "     " + info);
            }
        }
        
        public static void ReadFile()
        {
            using (var reader = new StreamReader(path, Encoding.Default))
            {
                string info = reader.ReadToEnd();
                Console.WriteLine(info);
            }
        }

        public static void SearchInfo(string info)
        {
            using (var reader = new StreamReader(path, Encoding.Default))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(info))
                        Console.WriteLine(line);
                }
            }
        }

        public static void ShowAmountOfRecords()
        {
            using (var reader = new StreamReader(path, Encoding.Default))
            {
                string line;
                byte counter = 0;
                while ((line = reader.ReadLine()) != null)
                    counter++;

                Console.WriteLine($"Amount of records: {counter}");
            }
        }
    }
}
