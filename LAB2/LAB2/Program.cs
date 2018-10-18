using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LAB2;

namespace LAB2
{
    class Program
    {
        static void Main(string[] args)
        {

            GetTypeInfo test = new GetTypeInfo("System.Text.StringBuilder");
            
            var a = test.Create();
            Console.WriteLine(a.GetType());

            Console.WriteLine(test.GetInfo());

            Console.WriteLine( test.InvokeMethod("Insert",new[]{"0","d"}));

            Console.ReadKey();

        }

    }
}
