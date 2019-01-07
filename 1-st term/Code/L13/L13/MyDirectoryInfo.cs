using System;
using System.Collections.Generic;
using System.IO;

namespace L13
{
    class MyDirectoryInfo
    {
        DirectoryInfo directory;
        private readonly string path;

        public MyDirectoryInfo(string path)
        {
            directory = new DirectoryInfo(path);
            this.path = path;
        }

        public void ShowName()
        {
            if (Directory.Exists(path))
                Console.WriteLine(directory.Name);
        }

        public void ShowSubdirectories()
        {
            if (Directory.Exists(path))
            {
                var subdirectories = Directory.GetDirectories(path);
                Console.WriteLine("Amount of subdirectories: {0}", subdirectories.Length);
                foreach (var dir in subdirectories)
                    Console.WriteLine(dir);
                Console.WriteLine();
            }
        }

        public void ShowFiles()
        {
            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);
                Console.WriteLine("Amount of files: {0}", files.Length);
                foreach (var file in files)
                    Console.WriteLine(file);
                Console.WriteLine();
            }
        }

        public void ShowTimeInfo()
        {
            if (Directory.Exists(path))
            {
                Console.WriteLine("Creation time: {0}", directory.CreationTime);
                Console.WriteLine("Last access time: {0}", Directory.GetLastAccessTime(path));
                Console.WriteLine("Last write time: {0}", directory.LastWriteTime);
            }
        }

        public void ShowParentList()
        {
            if (Directory.Exists(path))
            {
                var parentList = new List<DirectoryInfo>();
                var parent = directory.Parent;
                while (parent != null)
                {
                    parentList.Add(parent);
                    parent = parent.Parent;
                }

                Console.WriteLine("List of parent directories:");
                foreach (var element in parentList)
                {
                    Console.Write($"<--{element} ");
                }
                Console.WriteLine(directory.Root);
            }
        }

        public void ShowFullInformation()
        {
            if (Directory.Exists(path))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Full information of {directory.Name} directory");
                Console.ResetColor();

                ShowSubdirectories();
                ShowFiles();
                ShowTimeInfo();
                ShowParentList();
                Log.RecordInfo(directory.FullName, "Full directory information was obtained!");
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Directory {directory.Name} doesn't exist!");
                Console.ResetColor();
                Log.RecordInfo(directory.FullName, "Directory information wasn't obtained!");
            }
            Console.WriteLine();

            
        }
    }
}
