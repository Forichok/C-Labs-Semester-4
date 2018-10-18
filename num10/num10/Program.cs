using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace num10
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 2)
            {
                Rhyme.FillBaseFromFile(args[0]);
                Rhyme.ChangePoemFromFile(args[1], args[2]);
            }
            else
            {
                Rhyme.FillBaseFromFile();
                Rhyme.ChangePoemFromFile();
            }
            //Console.ReadKey();
        }
    }
}
