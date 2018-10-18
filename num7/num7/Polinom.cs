using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace num7
{
    /// <summary>
    /// нечитабельно, разбей пожалуйста по блокам
    /// </summary>
    
   public sealed class Polynom<T> :  IEnumerable
    {
        private readonly List<Monom<T>> polynom;
        private static ICalculate<T> _calculator;

        #region Constructors

        public Polynom(ICalculate<T> calculator)
        {
            polynom = new List<Monom<T>>();
            _calculator = calculator;
        }
        public Polynom(Polynom<T> A, ICalculate<T> calculator)
        {
            polynom = new List<Monom<T>>();
            _calculator = calculator;
            for (int i = 0; i < A.polynom.Count; i++)
                Add(A.polynom[i]);
        }
        #endregion

        #region operators
        public static Polynom<T> operator +(Polynom<T> A, Polynom<T> B)
        {
            if (A == null || B == null)
                return null;
            Polynom<T> res = new Polynom<T>(_calculator);
            foreach (var i in A.polynom)
                res.Add(i);
            foreach (var i in B.polynom)
                res.Add(i);
            return res;
        }
        public static Polynom<T> operator -(Polynom<T> A, Polynom<T> B)
        {
            if (A == null || B == null)
                return null;
            var res = new Polynom<T>(_calculator);
            foreach (Monom<T> i in A)
                res.Add(i);
            foreach (Monom<T> i in B)
            {
                T minus = _calculator.Minus(i.coef);
                Monom<T> monom;
                monom.coef = minus;
                monom.pow = i.pow;
                res.Add(monom);
            }
            return res;
        }
        public static Polynom<T> operator *(Polynom<T> A, Polynom<T> B)
        {
            if (A == null || B == null)
                return null;
            var res = new Polynom<T>(_calculator);

            foreach (var i in A.polynom)
                foreach (var j in B.polynom)
                {
                    Monom<T> monom;
                    monom.coef = _calculator.Mul(i.coef, j.coef);
                    monom.pow = i.pow + j.pow;
                    res.Add(monom);
                }
            return res;
        }
        public static Polynom<T> operator *(Polynom<T> A, Monom<T> B)
        {
            if (A == null )
                return null;
            var res = new Polynom<T>(A, _calculator);

            for (int i = 0; i < res.polynom.Count; i++)
            {
                Monom<T> monom;
                monom.coef = _calculator.Mul(res.polynom[i].coef, B.coef);
                monom.pow = res.polynom[i].pow + B.pow;
                res.polynom[i] = monom;
            }
            return res;
        }
        public static Polynom<T> operator /(Polynom<T> A, Polynom<T> B)
        {
            if (A == null || B == null)
                return null;
            var res = new Polynom<T>(_calculator);
            var num = new Polynom<T>(A, _calculator);
            Polynom<T> tmp;
            while (num >= B)
            {
                Monom<T> newMonom;
                newMonom.pow = num.polynom[0].pow - B.polynom[0].pow;
                newMonom.coef = _calculator.Div(num.polynom[0].coef, B.polynom[0].coef);
                res.Add(newMonom);
                tmp = B * newMonom;
                num = num - tmp;
                num.polynom.RemoveAt(0);
            }
            return res;
        }
        public static Polynom<T> Composition(Polynom<T> A, Polynom<T> B)
        {
            if (A == null || B == null)
                return null;
            var res = new Polynom<T>(A, _calculator);
            for (int i = 0; i < A.polynom.Count; i++)
            {
                var newB = new Polynom<T>(B, _calculator);
                for (int j = 0; j < A.polynom[i].pow; j++)
                    newB = newB * B;
                res = res + newB;
            }
            return res;
        }
        public static bool operator ==(Polynom<T> A, Polynom<T> B)
        {
            if (A == null || B == null)
                return false;
            if (A.polynom.Count != B.polynom.Count)
                return false;
            for (int i = 0; i < A.polynom.Count; i++)
            {
                if (A.polynom[i].pow != B.polynom[i].pow)
                    return false;
            }
            return true;
        }
        public static bool operator >(Polynom<T> A, Polynom<T> B)
        {
            if (A == null || B == null)
                return false;
            if (A.polynom.Count <= B.polynom.Count)
                return false;
            for (int i = 0; i < A.polynom.Count; i++)
            {
                if (A.polynom[i].pow > B.polynom[i].pow)
                    return true;
                if (A.polynom[i].pow < B.polynom[i].pow)
                    return false;
            }
            return true;
        }
        public static bool operator !=(Polynom<T> A, Polynom<T> B)
        {
            return !(A == B);
        }
        public static bool operator >=(Polynom<T> A, Polynom<T> B)
        {
            return (A > B || A == B);
        }
        public static bool operator <(Polynom<T> A, Polynom<T> B)
        {
            return !(A >= B);
        }
        public static bool operator <=(Polynom<T> A, Polynom<T> B)
        {
            return !(A < B);
        }
        #endregion


        public void Add(Monom<T> monom)
        {
            if (polynom.Count == 0)
            {
                polynom.Add(monom);
                return;
            }
            for (int i = 0; i < polynom.Count; i++)
            {
                if (polynom[i].pow == monom.pow)
                {
                    T sum = _calculator.Sum(polynom[i].coef, monom.coef);
                    polynom.Remove(polynom[i]);
                    monom.coef = sum;
                    polynom.Insert(i, monom);
                    return;
                }
            }
            for (int i = 0; i < polynom.Count; i++)
            {
                if ((polynom[i].pow < monom.pow))
                {
                    polynom.Insert(i, monom);
                    return;
                }
            }
            polynom.Add(monom);
        }
        public IEnumerator GetEnumerator()
        {
            return polynom.GetEnumerator();
        }
        public override string ToString()
        {
            string s = string.Empty;

            if (polynom.Count == 0)
                return "0";

            for (var i = 0; i < polynom.Count; i++)
            {
                s += polynom[i];
            }
            return s.TrimEnd('+', '\n', ' ');
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    public struct Monom<T>
    {
        /// <summary>
        /// private филды + паблик get свойства
        /// </summary>
        public T coef;
        public int pow;

        public Monom<decimal> GetRandomMonom()
        {
            var monom = new Monom<decimal>();
            var rnd = new Random();
            monom.coef = rnd.Next() % 10;
            monom.pow = rnd.Next() % 10;
            return monom;
        }

        public Monom(T n, int p)
        {
            coef = n;
            pow = p;
        }

        public Monom(T n)
        {
            var rnd = new Random();
            coef = n;
            pow = rnd.Next() % 10;
        }
        public override string ToString()
        {
            if (typeof(Monom<T>) == typeof(Monom<Matrix>)) return coef + " * x^{" + pow + "} +\n\n";
            return coef + "x^{" + pow + "}+";
        }
    }
}
