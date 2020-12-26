using Math.Exceptions;
using System;

namespace Math
{
    public class Matrix
    {

        private readonly Single[,] _matrix;

        public Matrix(Single[,] matrix)
        {
            _matrix = matrix;
        }

        public Single GetItem(Int32 x, Int32 y)
        {
            return _matrix[x, y];
        }

        public Int32 GetDimension0()
        {
            return _matrix.GetLength(0);
        }

        public Int32 GetDimension1()
        {
            return _matrix.GetLength(1);
        }

        public Matrix Sum(Matrix matrix)
        {
            if ((matrix.GetDimension0() != GetDimension0()) || (matrix.GetDimension1() != GetDimension1()))
            {
                throw new IncorrectDimensionException();
            }
            Single[,] newMatrix = new Single[GetDimension0(), GetDimension1()];
            for (Int32 i = 0; i < GetDimension0(); i++)
            {
                for (Int32 k = 0; k < GetDimension1(); k++)
                {
                    newMatrix[i, k] = GetItem(i, k) + matrix.GetItem(i, k);
                }
            }
            Matrix result = new(newMatrix);
            return result;
        }

        public Matrix Multiply(Single scalar)
        {
            Single[,] newMatrix = new Single[GetDimension0(), GetDimension1()];
            for (Int32 i = 0; i < GetDimension0(); i++)
            {
                for (Int32 k = 0; k < GetDimension1(); k++)
                {
                    newMatrix[i, k] = GetItem(i, k) * scalar;
                }
            }
            Matrix result = new(newMatrix);
            return result;
        }

        public Matrix Multiply(Matrix matrix)
        {
            Single[,] newMatrix = new Single[GetDimension0(), matrix.GetDimension1()];
            for (int i = 0; i < GetDimension0(); i++)
            {
                for (int k = 0; k < matrix.GetDimension1(); k++)
                {
                    for (Int32 m = 0; m < GetDimension1(); m++)
                    {
                        newMatrix[i, k] += GetItem(i, m) * matrix.GetItem(m, k);
                    }
                }
            }
            Matrix result = new(newMatrix);
            return result;
        }

        public Matrix Transpose()
        {
            float[,] newMatrix = new float[GetDimension1(), GetDimension0()];
            for (int i = 0; i < GetDimension0(); i++)
            {
                for (int k = 0; k < GetDimension1(); k++)
                {
                    newMatrix[k, i] = _matrix[i, k];
                }
            }
            Matrix result = new(newMatrix);
            return result;
        }
    }
}
