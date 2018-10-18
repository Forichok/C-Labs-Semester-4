using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace num7
{

    public sealed class Matrix : ICloneable, IEnumerable
    {
        private decimal[,] _matrix;
        private readonly int _size;

        #region Constructors
        public Matrix(decimal[,] matrix, int n)
        {
            _matrix = matrix;
            _size = n;
        }

        public Matrix(Matrix matrix)
        {
            _matrix = matrix._matrix;
            _size = matrix._size;
        }

        public Matrix(int size)
        {
            _size = size;
            var random = new Random();
            _matrix = new decimal[_size, _size];
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _matrix[i, j] = random.Next() % 10 + 1;
                }
            }
        }

        public Matrix(int size, int value)
        {
            _size = size;
            _matrix = new decimal[_size, _size];
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _matrix[i, j] = value;
                }
            }
        }

        #endregion

        public int Size()
        {
            return _size;
        }
        private decimal[,] Minor(decimal[,] matrix, int N, int col, int row)
        {
            decimal[,] newMatrix = new decimal[N - 1, N - 1];
            int i = 0;
            for (int n = 0; n < N; n++)
            {
                var j = 0;
                if (n == row)
                    continue;
                for (int m = 0; m < N; m++)
                {
                    if (m == col)
                        continue;
                    newMatrix[i, j++] = matrix[n, m];
                }
                i++;
            }
            return newMatrix;
        }

        private decimal Determinant(decimal[,] matrix, int n)
        {
            int k = 1;
            decimal res = 0;
            if (matrix.Length == 1)
                return matrix[0, 0];
            if (matrix.Length == 4)
                return matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];

            for (int i = 0; i < n; i++)
            {
                res = res + k * matrix[0, i] * Determinant(Minor(matrix, n, i, 0), n - 1);
                k = -k;
            }
            return res;
        }
        #region operators




        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (A == null || B == null)
                return null;
            if (A.Size() != B._size) throw new MatrixException("Different size");
            var result = new Matrix(A._size, A._size);
            for (int i = 0; i < A._size; i++)
            {
                for (int j = 0; j < A._size; j++)
                {
                    result._matrix[i, j] = A._matrix[i, j] + B._matrix[i, j];
                }
            }
            return result;
        }
        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (A == null || B == null)
                return null;
            if (A.Size() != B._size) throw new MatrixException("Different size");
            Matrix result = new Matrix(A._size, A._size);
            for (int i = 0; i < A._size; i++)
            {
                for (int j = 0; j < A._size; j++)
                {
                    result._matrix[i, j] = A._matrix[i, j] - B._matrix[i, j];
                }
            }
            return result;
        }
        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A == null || B == null)
                return null;
            if (A.Size() != B._size) throw new MatrixException("Different size");
            var result = new Matrix(A._size, B._size);
            for (int i = 0; i < A._size; i++)
            {
                for (int j = 0; j < B._size; j++)
                {
                    result._matrix[i, j] = 0;
                    for (int l = 0; l < A._size; l++)
                    {
                        result._matrix[i, j] += A._matrix[i, l] * B._matrix[l, j];
                    }
                }
            }
            return result;
        }
        public static Matrix operator /(Matrix A, Matrix B)
        {
            if (A == null || B == null)
                return null;
            if (A.Size() != B._size) throw new MatrixException("Different size");
            var res = new Matrix(B.GetInverseMatrix());
            res = A * res;
            return res;
        }
        public static Matrix operator +(Matrix A, decimal number)
        {
            if (A == null )
                return null;
            var result = new Matrix(A._size, A._size);
            for (int i = 0; i < A._size; i++)
            {
                for (int j = 0; j < A._size; j++)
                {
                    result._matrix[i, j] += number;
                }
            }
            return result;
        }
        public static Matrix operator -(Matrix A, decimal number)
        {
            if (A == null )
                return null;
            var result = new Matrix(A._size, A._size);
            for (int i = 0; i < A._size; i++)
            {
                for (int j = 0; j < A._size; j++)
                {
                    result._matrix[i, j] -= number;
                }
            }
            return result;
        }
        public static Matrix operator *(Matrix A, decimal number)
        {
            if (A == null )
                return null;
            var result = new Matrix(A);
            for (int i = 0; i < A._size; i++)
            {
                for (int j = 0; j < A._size; j++)
                {
                    result._matrix[i, j] *= number;
                }
            }
            return result;
        }
        #endregion

        public IEnumerator GetEnumerator()
        {
            return _matrix.GetEnumerator();
        }

        public Matrix Transposed()
        {
            var newMatrix = new decimal[_size, _size];

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    newMatrix[i, j] = _matrix[j, i];
                }
            }
            _matrix = newMatrix;
            return (new Matrix(_matrix, _size));
        }

        public Matrix GetMinorMatrix()
        {
            Matrix minorMatrix = new Matrix(_size);

            for (int i = 0; i < _size; i++)
                for (int j = 0; j < _size; j++)
                {
                    minorMatrix._matrix[j, i] = Determinant(Minor(_matrix, _size, i, j), _size - 1);
                }
            return minorMatrix;
        }

        public Matrix GetCofactor()
        {
            int k = 1;
            Matrix result = new Matrix(GetMinorMatrix());
            for (int i = 0; i < _size; i++)
                for (int j = 0; j < _size; j++)
                {
                    result._matrix[i, j] *= k;
                    k *= -1;
                }
            return result;
        }

        public Matrix GetInverseMatrix()
        {
            Matrix result = new Matrix(GetCofactor());
            result.Transposed();
            decimal det = Determinant();
            if (det == 0)
                throw new MatrixException("det A = 0");
            det = 1 / det;
            return result * det;
        }

        public decimal Determinant()
        {
            return Determinant(_matrix, _size);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            string str = string.Empty;
            Console.WriteLine();
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    decimal roundMatrix = Math.Round(_matrix[i, j], 5);
                    if (roundMatrix.ToString().Length > 5)
                        str += (" | " + roundMatrix.ToString().Substring(0, 5));
                    else
                        str += (" | " + roundMatrix);
                }
                str += " |\n";
            }
            return str.Substring(0, str.Length - 1);
        }
    }
}
