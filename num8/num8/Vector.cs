using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace num8
{
    public class Vector<T> : ICollection<T>  
        where T: new()
    {
        private readonly List<T> _vector;
        private readonly ICalculate<T> _calculator;

        private Vector() { }

        public Vector(ICalculate<T> iCalc) : this(iCalc, new List<T>()) { }

        public Vector(ICalculate<T> iCalc, IEnumerable<T> collection)
        {
            this._calculator = iCalc;
            _vector = new List<T>(collection);
        }

        public Vector(ICalculate<T> iCalc, int n)
        {
            this._calculator = iCalc;
            _vector = new List<T>(n);
        }

        public static Vector<T> operator +(Vector<T> left, Vector<T> right)
        {
            if (left == null || right == null)
                return null;
            var result = new Vector<T>(left._calculator);
            for (int i = 0; i < left.Count; i++)
            {
                T sum = left._calculator.Sum(left[i], right[i]);
                result.Add(sum);
            }

            return result;
        }

        public static Vector<T> operator -(Vector<T> left, Vector<T> right)
        {
            if (left == null || right == null)
                return null;
            var result = new Vector<T>(left._calculator);

            for (int i = 0; i < left.Count; i++)
            {
                T sub = left._calculator.Subtraction(left[i], right[i]);
                result.Add(sub);
            }
            return result;
        }


        public static Vector<T> operator *(Vector<T> left, T k)
        {
            if (left == null)
                return null;
            var result = new Vector<T>(left._calculator);

            for (int i = 0; i < left.Count; i++)
            {
                T multi = left._calculator.Multiplication(left[i], k);
                result.Add(multi);
            }
            return result;
        }


        public T Module()
        {
            T sum=default(T);
            for (int i = 0; i < _vector.Count; i++)
            {
                T numberInSqrt = _calculator.Pow(_vector[i], 2);
                sum = _calculator.Sum(sum, numberInSqrt);
            }
            T modul = _calculator.Sqrt(sum);
            return modul;
        }

        public T Scalar_vector(Vector<T> b)
        {
            if (_vector.Count != b.Count)
                throw new ArithmeticException();

            T skal = default(T);
            for (int i = 0; i < _vector.Count; i++)
            {
                T multi = _calculator.Multiplication(_vector[i], b[i]);
                skal = _calculator.Sum(skal, multi);
            }
            return skal;
        }

        public T this[int index]
        {
            get => _vector[index];
            set => _vector[index] = value;
        }

        public override string ToString()
        {
            string str = "{";
            for (int i = 0; i < _vector.Count; i++)
            {
                str += _vector[i] + ", ";
            }
            return str.TrimEnd(',', ' ') + "}";
        }

        public static List<Vector<T>> Orthogonalize(List<Vector<T>> vect)
        {
            var n = vect.Count;
            var copyVect = new List<Vector<T>>(vect);
            var simillarSizeVect = new List<Vector<T>>(n);

            for (int i = 0; i < vect.Count; i++)
            {
                var result = new Vector<T>(copyVect[i]._calculator, copyVect[i].Count);
                if (i == 0)
                {
                    simillarSizeVect.Insert(i, copyVect[i]);
                }
                else if (i != 0)
                {
                    int i2 = i;
                    while (i2 != 0)
                    {
                        try
                        {
                            if (result.Count == 0)
                                result = Project(copyVect[i], simillarSizeVect[i2 - 1]);
                            else if (result.Count != 0)
                                result = result + (Project(copyVect[i], simillarSizeVect[i2 - 1]));
                            i2--;
                        }
                        catch (ArithmeticException ex)
                        {
                            Console.WriteLine(ex.Message);
                            break;
                        }
                    }
                    simillarSizeVect.Insert(i, (copyVect[i] - result));
                }
            }
            return simillarSizeVect;
        }

        public static Vector<T> Project(Vector<T> first, Vector<T> second)
        {
            T scalar1_2 = first.Scalar_vector(second);
            T scalar2_1 = second.Scalar_vector(second);
            T project = first._calculator.Division(scalar1_2, scalar2_1);
            var result = second * project;
            return result;
        }

        public T[] To_Array()
        {
            T[] array = _vector.ToArray();
            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _vector.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _vector.Add(item);
        }

        public void Clear()
        {
            _vector.Clear();
        }

        public bool Contains(T item)
        {
            return _vector.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _vector.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _vector.Remove(item);
        }
        public int Count => _vector.Count;
        public bool IsReadOnly => false;

    }
}
