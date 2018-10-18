using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace num8
{
    class Program
    {
        public static void ShowMessage(object sender, MyEventArgs m)
        {
            Console.WriteLine("\n{0}: {1}", sender, m.msg);
        }
        static void Main(string[] args)
        {
            Complex.DivByZero += ShowMessage;
            Complex A = new Complex(10, 0);

            Complex B = new Complex(1, 1);

            Complex C = null;

            var list1 = new List<Complex>() { A, C };
            var list2 = new List<Complex>() { A, B };
            var vec1 = new Vector<Complex>(new ComplexICalcRealisation(), list1);
            var vec2 = new Vector<Complex>(new ComplexICalcRealisation(), list2);

            var vec3 = new Vector<int>(new IntICalcRealisation());
            vec3.Add(1);
            vec3.Add(2);
            vec3.Add(2);
            vec3.Add(-1);
            Console.WriteLine(vec3.Module());

            var vec4 = new Vector<int>(new IntICalcRealisation());

            vec4.Add(1);
            vec4.Add(1);
            vec4.Add(-5);
            vec4.Add(3);

            Console.WriteLine(vec3.Scalar_vector(vec4));


            var vec5 = new Vector<int>(new IntICalcRealisation());

            vec5.Add(3);
            vec5.Add(2);
            vec5.Add(8);
            vec5.Add(-7);

            List<Vector<int>> test = new List<Vector<int>>() { vec3, vec4, vec5 };

            var result = (Vector<int>.Orthogonalize(test));

            Console.WriteLine(vec1 + vec2);

            Console.WriteLine(vec1.ToString());
            Console.ReadKey();

          }
    }
}
