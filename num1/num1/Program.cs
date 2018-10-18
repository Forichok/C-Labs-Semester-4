using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace num1
{
    class Program
    {
        static void Main(string[] args)
        {
            Complex[] karanoTest = Solver.Kordan(1, 2, -3, 5);
            Complex[] vietaTest = Solver.Vieta(1, 2, -3, 5);
            
            Console.ReadKey();
        }
    }
}
