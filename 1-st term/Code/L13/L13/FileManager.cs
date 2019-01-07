using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace L13
{
    class FileManager
    {
        DirectoryInfo directory;

        public FileManager(string path)
        {
            directory = new DirectoryInfo(path);
            directory.Create();
        }

        public void PerformTask()
        {
            var subdir_str = new StringBuilder(null);
            foreach (var dir in directory.Parent.EnumerateDirectories())//
                subdir_str.Append(dir.Name);

            var files_str = new StringBuilder(null);
            foreach (var file in directory.Parent.EnumerateFiles())//
                files_str.Append(file.Name);

            var str = subdir_str.ToString() + files_str.ToString();

            try
            {
                //CreateSubdirectory(string path)
                var Inspect_directory = new DirectoryInfo(Path.Combine(directory.FullName, "Inspect"));
                if (Inspect_directory.Exists)
                    Inspect_directory.Delete(true);
                Inspect_directory.Create();
                Console.WriteLine($"Directory {Inspect_directory.Name} successfully created!");
                Console.WriteLine();

                var DirInfo_file = new FileInfo(Path.Combine(directory.FullName, "DirInfo.txt"));
                if (DirInfo_file.Exists)
                    DirInfo_file.Delete();
                File.AppendAllText(DirInfo_file.FullName, str);
                Console.WriteLine($"File {DirInfo_file.Name} successfully created and data recorded!");
                Console.WriteLine();

                var Copy_file = new FileInfo(Path.Combine(directory.FullName, "Copy.txt"));
                if (Copy_file.Exists)
                    Copy_file.Delete();
                DirInfo_file.CopyTo(Copy_file.FullName);
                Console.WriteLine($"File {DirInfo_file.Name} copied!");
                DirInfo_file.Delete();
                Console.WriteLine($"File {DirInfo_file.Name} deleted!");
                Console.WriteLine();

                var Files_directory = new DirectoryInfo(Path.Combine(directory.FullName, "Files"));
                Files_directory.Create();
                Console.WriteLine($"Directory {Files_directory.Name} successfully created!");
                Console.WriteLine();

                Console.Write("Enter extension: ");
                var extension = Console.ReadLine();
                foreach (var file in directory.Parent.GetFiles("*." + extension))
                {
                    file.CopyTo(Path.Combine(Files_directory.FullName, file.Name));
                    Console.WriteLine($"File {file} copied to {Files_directory}");
                }
                Console.WriteLine();

                Files_directory.MoveTo(Path.Combine(Inspect_directory.FullName, "Files"));
                Console.WriteLine($"Directory {Files_directory} moved to {Inspect_directory}");
                Console.WriteLine();


                var path = Path.Combine(directory.FullName, "Archive.zip");
                ZipFile.CreateFromDirectory(Files_directory.FullName, path);
                Console.WriteLine($"Archive successfully created in {directory.FullName}!");
                using (var archive = ZipFile.OpenRead(path))
                {
                    foreach (ZipArchiveEntry file in archive.Entries)
                    {
                        Console.WriteLine("File name: {0}", file.Name);
                        Console.WriteLine("File size: {0} bytes", file.Length);
                        Console.WriteLine("Compression ratio: {0}", ((double)file.CompressedLength / file.Length).ToString("0.0%"));
                    }
                }
                ZipFile.ExtractToDirectory(path, directory.FullName);
                Console.WriteLine();

                Console.WriteLine($"Archive unzipped to {directory.FullName}");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Log.RecordInfo(directory.FullName, "There have been changes in the directory!");
        }
    }
}
