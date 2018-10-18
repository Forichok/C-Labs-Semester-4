using System;

namespace num8
{
    public class MyEventArgs : EventArgs
    {
        public readonly string msg;
        public MyEventArgs(string message)
        {
            msg = message;
        }

    }

    public sealed class Complex
    {
        public static EventHandler<MyEventArgs> DivByZero;

        private double _real;
        private double _imaginary;

        public Complex()
        {
            _real = 0;
            _imaginary = 0;
        }

        public Complex(double real, double imaginary)
        {
            this._real = real;
            this._imaginary = imaginary;
        }
        public Complex(double _real)
        {
            this._real = _real;
        }
        
        #region Operators

        public static Complex operator +(Complex A, Complex B)
        {
            // сделать проверки более адекватными
            if (A == null||B==null)
                return null;

            var sumTwoReal = A._real + B._real;
            var sumTwoImaginary = A._imaginary + B._imaginary;
            return new Complex(sumTwoReal, sumTwoImaginary);
        }
        public static Complex operator -(Complex A, Complex B)
        {
            if (A == null || B == null)
                return null;
            double subTwoReal = A._real - B._real;
            double subTwoImaginary = A._imaginary - B._imaginary;
            return new Complex(subTwoReal, subTwoImaginary);
        }
        public static Complex operator *(Complex A, Complex B)
        {
            if (A == null || B == null)
                return null;
            double resultReal = A._real * B._real - A._imaginary * B._imaginary;
            double resultImaginary = A._real * B._imaginary + A._imaginary * B._real;
            return new Complex(resultReal, resultImaginary);
        }
        public static Complex operator *(Complex A, double k)
        {
            if (A == null)
                return null;
            return new Complex(A._real * k, A._imaginary * k);
        }
        public static Complex operator /(Complex A, Complex B)
        {
            if (A == null || B == null)
                return null;
            double divider = B._real * B._real + B._imaginary * B._imaginary;
            Complex result = new Complex();
            if (divider == 0)
            {
                if (DivByZero != null)
                {
                    DivByZero(B, new MyEventArgs("  Error! Division by zero!  "));
                    result = new Complex(0, 0);
                }
            }
            else
            {
                result = new Complex((A._real * B._real + A._imaginary * B._imaginary) / divider, (A._imaginary * B._real - A._real * B._imaginary) / divider);
            }
            return result;
        }

        #endregion

        public double Module()
        {
            double sumRealAndImaginarty = _real * _real + _imaginary * _imaginary;
            return Math.Sqrt(sumRealAndImaginarty);
        }

        public double Angle()
        {
            return Math.Atan2(_imaginary, _real);
        }

        public Complex Pow(double k)
        {
            double modulInDegree = Math.Pow(Module(), k);
            double angleWithCoef = k * Angle();
            double resultReal = modulInDegree * Math.Cos(angleWithCoef);
            double resultImaginary = modulInDegree * Math.Sin(angleWithCoef);
            Complex result = new Complex(resultReal, resultImaginary);
            return result;
        }

        public Complex[] Root(int n)
        {
            double modulInDegree = Math.Pow(Module(), 1 / (double)n);
            double angleWithCoef = Angle() / (double)n;
            double oneRadianInDegree = 57.2958;
            Complex[] result = new Complex[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = new Complex();
            }
            for (int i = 0; i < n; i++)
            {
                double degree = 360 * i / ((double)n * oneRadianInDegree);
                result[i]._real = modulInDegree * Math.Cos(angleWithCoef + degree);
                result[i]._imaginary = modulInDegree * Math.Sin(angleWithCoef + degree);
            }
            return result;
        }

        public override string ToString()
        {
            if (_imaginary == 0)
                return (_real.ToString());
            if (_imaginary > 0)
                return (_real + "+" + _imaginary + "i");

            return (_real.ToString() + _imaginary + "i");
        }

        
    }
}
