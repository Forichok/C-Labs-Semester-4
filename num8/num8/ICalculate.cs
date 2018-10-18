using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace num8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public interface ICalculate<T> 
    {
        T Sum(T a, T b);
        T Subtraction(T a, T b);
        T Multiplication(T a, T b);
        T MulOnNumber(T a, double b);
        T Division(T a, T b);
        T Pow(T a, int b);
        T Sqrt(T a);

    }

    class IntICalcRealisation : ICalculate<int>
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }

        public int Subtraction(int a, int b)
        {
            return a - b;
        }

        public int Multiplication(int a, int b)
        {
            return a * b;
        }

        public int MulOnNumber(int a, double b)
        {
            int _b = Convert.ToInt32(b);
            return a * _b;
        }

        public int Division(int a, int b)
        {
            return a / b;
        }

        public int Pow(int a, int b)
        {
            return (int) Math.Pow(a, b);
        }

        public int Sqrt(int a)
        {
            return (int) Math.Sqrt(a);
        }

    }

    class DoubleICalcRealisation : ICalculate<double>
    {
        public double Sum(double a, double b)
        {
            return a + b;
        }

        public double Subtraction(double a, double b)
        {
            return a - b;
        }

        public double Multiplication(double a, double b)
        {
            return a * b;
        }

        public double MulOnNumber(double a, double b)
        {
            return a * b;
        }

        public double Division(double a, double b)
        {
            return a / b;
        }

        public double Pow(double a, int b)
        {
            return (double) Math.Pow(a, b);
        }

        public double Sqrt(double a)
        {
            return (double) Math.Sqrt(a);
        }

    }

    class ComplexICalcRealisation : ICalculate<Complex>
    {
        public Complex Sum(Complex a, Complex b)
        {
            return a + b;
        }

        public Complex Subtraction(Complex a, Complex b)
        {
            return a - b;
        }

        public Complex Multiplication(Complex a, Complex b)
        {
            return a * b;
        }

        public Complex MulOnNumber(Complex a, double b)
        {
            return a * b;
        }

        public Complex Division(Complex a, Complex b)
        {
            return a / b;
        }

        public Complex Pow(Complex a, int b)
        {
            return a.Pow(b);
        }

        public Complex Sqrt(Complex a)
        {
            return Sqrt(a);
        }

    }
}


