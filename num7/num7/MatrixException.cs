using System;

namespace num7
{
    public sealed class MatrixException : Exception
    {
        public MatrixException() { }
        public MatrixException(string message) : base(message) { }
    }
}