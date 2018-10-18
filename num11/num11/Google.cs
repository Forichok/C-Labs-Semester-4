using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace num11
{
    class Google
    {
        private static readonly List<FileInfo> Found = new List<FileInfo>();

        public static void Search(string root, string command = "", string findPart = "")
        {
            Stack<string> dirs = new Stack<string>(20);

            if (!Directory.Exists(root))
            {
                return;
            }

            dirs.Push(root);

            while (dirs.Count > 0)
            {
                var currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = Directory.GetDirectories(currentDir);
                }

                catch (Exception e)
                {
                    //   Console.WriteLine(e.Message);
                    continue;
                }

                string[] files;

                try
                {
                    files = Directory.GetFiles(currentDir);
                }

                catch (Exception e)
                {
                    // Console.WriteLine(e.Message);
                    continue;
                }

                foreach (string file in files) //все файлики из текущей папки 
                {
                    try
                    {
                        var fi = new FileInfo(file);

                        if (command == "findInFile" && fi.Name.EndsWith(".txt"))
                        {
                            var sr = new StreamReader(fi.FullName);
                            var lineNumber = 0;
                            while (!sr.EndOfStream)
                            {
                                lineNumber++;
                                string line = sr.ReadLine();
                                if (line.Contains(findPart))
                                {
                                    Console.WriteLine("Found \"{0}\" ({1} line) in file:\n{2}", findPart, lineNumber, fi.FullName);
                                    sr.Close();
                                    break;
                                }
                            }
                        }

                        if (command == "findFile" && fi.Name.Contains(findPart))
                            Console.WriteLine("{0}: {1}, {2}", fi.FullName, fi.Length, fi.CreationTime);

                        if (command == "fileExecute" && fi.Name == findPart)
                        {
                            Found.Add(fi);
                        }

                        if (command == "fileArchive" && fi.Name == findPart)
                        {
                            Found.Add(fi);
                        }

                        if (command == "findWithPattern")
                        {
                            findPart = findPart.Replace(".", "\\W");
                            findPart = findPart.Replace("*", "[^ ]+");
                            findPart = findPart.Replace("?", "\\w");

                            var reg = new Regex(findPart, RegexOptions.IgnoreCase);

                            bool isFound = reg.Match(fi.Name).ToString() == fi.Name;
                            if (isFound)
                            {
                                Console.WriteLine("{0}: {1}, {2}", fi.Name, fi.Length, fi.CreationTime);
                                Found.Add(fi);
                            }
                        }
                    }
                    catch (IOException e)
                    {
                        //Console.WriteLine(e.Message);
                    }
                }
                foreach (string str in subDirs)
                    dirs.Push(str);
            }

            switch (command)
            {
                case "findWithPattern":
                    FindWithPattern();
                    break;
                case "fileExecute":
                    ExecuteFile();
                    break;
                case "fileArchive":
                    ArchiveFile();
                    break;
            }
            Found.Clear();

        }

        private static void FindWithPattern()
        {
            try
            {
                for (int i = 0; i < Found.Count; i++)
                {
                    Console.WriteLine("{0}: {1}", i + 1, Found[i].FullName);
                }
                Console.WriteLine("Input file's number you wanna open/archive");
                var fileNumber = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.WriteLine("Choose action:" +
                                  "\n 0 to archive" +
                                  "\n 1 to open");

                var action = Convert.ToInt32(Console.ReadLine());

                bool isNumCorrect = fileNumber >= 0 && fileNumber < Found.Count;

                if (isNumCorrect)
                {
                    switch (action)
                    {
                        case 0:
                            ArchiveFile(Found[fileNumber]);
                            break;

                        case 1:
                            OpenFile(Found[fileNumber]);
                            break;
                    }
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private static void ExecuteFile()
        {
            for (int i = 0; i < Found.Count; i++)
            {
                Console.WriteLine("{0}: {1}", i + 1, Found[i].FullName);
            }
            Console.WriteLine("Input file's number you wanna open");
            try
            {
                var fileNumber = Convert.ToInt32(Console.ReadLine()) - 1;
                bool isNumCorrect = fileNumber >= 0 && fileNumber < Found.Count;
                if (isNumCorrect)
                {
                    OpenFile(Found[fileNumber]);
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void OpenFile(FileInfo file)
        {
            var pi = new ProcessStartInfo(file.FullName)
            {
                Arguments = Path.GetFileName(file.FullName),
                UseShellExecute = true,
                WorkingDirectory = Path.GetDirectoryName(file.FullName),
                Verb = "OPEN"
            };
            Process.Start(pi);
        }

        private static void ArchiveFile()
        {

            for (int i = 0; i < Found.Count; i++)
            {
                Console.WriteLine("{0}: {1}", i + 1, Found[i].FullName);
            }
            Console.WriteLine("Input file's number you wanna archive");
            try
            {
                var fileNumber = Convert.ToInt32(Console.ReadLine()) - 1;
                bool isNumCorrect = fileNumber >= 0 && fileNumber < Found.Count;
                if (isNumCorrect)
                {
                    ArchiveFile(Found[fileNumber]);
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        private static void ArchiveFile(FileInfo file)
        {
            try
            {
                var bytes = File.ReadAllBytes(file.FullName);
                var dialog = new FolderBrowserDialog();

                dialog.ShowDialog();

                var archivePath = dialog.SelectedPath + "\\" + file.Name + ".gz";
                using (FileStream fs = new FileStream(archivePath, FileMode.CreateNew))
                using (GZipStream zipStream = new GZipStream(fs, CompressionMode.Compress, false))
                {
                    zipStream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
