using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace num7
{
    public interface ICalculate<T>
    {
        T Sum(T A, T B);
        T Dif(T A, T B);
        T Mul(T A, T B);
        T Div(T A, T B);
        T Minus(T A);
        T SetNum(T A, int num);
    }

   public class IntCalculate : ICalculate<int>
    {
        public int Sum(int A, int B)
        {
            return A + B;
        }
        public int Dif(int A, int B)
        {
            return A - B;
        }
        public int Mul(int A, int B)
        {
            return A * B;
        }
        public int Div(int A, int B)
        {
            return A / B;
        }
        public int Minus(int A)
        {
            return -A;
        }
        public int SetNum(int A, int num)
        {
            return num;
        }
    }
   public class MatrixCalculate : ICalculate<Matrix>
    {
        public Matrix Sum(Matrix A, Matrix B)
        {
            return A + B;
        }
        public Matrix Dif(Matrix A, Matrix B)
        {
            return A - B;
        }
        public Matrix Mul(Matrix A, Matrix B)
        {
            return A * B;
        }

        public Matrix Div(Matrix A, Matrix B)
        {
            return A / B;
        }

        public Matrix Minus(Matrix A)
        {
            return A * (-1);
        }
        public Matrix SetNum(Matrix A, int num)
        {
            Matrix a = new Matrix(A.Size(), num);
            return a;
        }
    }

}
