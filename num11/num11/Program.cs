using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace num11
{
    public sealed class Program
    {
        private static readonly string _root = "C:\\Apps";

        [STAThread]
        static void Main(string[] args)
        {
            string command = "";

            while (command != "0")
            {
                Console.WriteLine("input command:" +
                                  "\n 1 to find file with part " +
                                  "\n 2 to find in all .txt files " +
                                  "\n 3 to find with pattern " +
                                  "\n 4 to open file with associated program " +
                                  "\n 5 to archive file with GZipStream" +
                                  "\n 0 to exit program");

                command = Console.ReadLine();

                switch (command)
                {
                    case "1":
                        string part = "";
                        Console.WriteLine("input part you wanna find");
                        part = Console.ReadLine();
                        Google.Search(_root, "findFile", part);
                        break;

                    case "2":

                        Console.WriteLine("input part you wanna find");
                        part = Console.ReadLine();
                        Google.Search(_root, "findInFile", part);
                        break;

                    case "3":
                        Console.WriteLine("input pattern");
                        part = Console.ReadLine();
                        Google.Search(_root, "findWithPattern", part);
                        break;

                    case "4":
                        Console.WriteLine("input file name you wanna open");
                        part = Console.ReadLine();
                        Google.Search(_root, "fileExecute", part);
                        break;

                    case "5":
                        Console.WriteLine("input file name you wanna archive");
                        part = Console.ReadLine();
                        Google.Search(_root, "fileArchive", part);
                        break;
                }
                Console.ReadKey();
            }
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
