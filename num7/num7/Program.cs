using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace num7
{
    public sealed class Program
    {
        static void Main()
        {
            var A = new Matrix(3);
            var B = new Matrix(3);
            Matrix C = null;
            foreach (var i in A)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine(A);
            Console.WriteLine(A.Transposed());

            Console.WriteLine(A + C);

            Monom<Matrix> monom1 = new Monom<Matrix>(B, 4);
            Monom<Matrix> monom2 = new Monom<Matrix>(A, 5);
            Monom<Matrix> monom3 = new Monom<Matrix>(B);
            Monom<Matrix> monom4 = new Monom<Matrix>(A);

            Polynom<Matrix> polynom1 = new Polynom<Matrix>(new MatrixCalculate()) { monom1, monom2 };
            Polynom<Matrix> polynom2 = new Polynom<Matrix>(new MatrixCalculate()) { monom1, monom3, monom4 };

            foreach (var i in polynom1)
            {
                Console.WriteLine(i);
            }

            //Console.WriteLine(polynom1);
            //Console.WriteLine(polynom2);

            //Console.WriteLine(polynom1 / polynom2);

            //Console.WriteLine(polynom1 + polynom2);

            Console.ReadKey();
        }
    }
}
