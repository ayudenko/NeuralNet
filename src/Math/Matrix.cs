using Math.Exceptions;

namespace Math
{
    public class Matrix
    {

        private readonly float[,] _matrix;

        public Matrix(float[,] matrix) => _matrix = matrix;

        public Matrix(float[] matrix)
        {
            _matrix = new float[1, matrix.Length];
            for (int i = 0; i < matrix.Length; i++)
            {
                _matrix[0, i] = matrix[i];
            }
        }

        public float GetItem(int row, int column) => _matrix[row, column];

        public int GetDimension0() => _matrix.GetLength(0);

        public int GetDimension1() => _matrix.GetLength(1);

        public Matrix Sum(Matrix matrix)
        {
            if ((matrix.GetDimension0() != GetDimension0()) || (matrix.GetDimension1() != GetDimension1()))
            {
                throw new IncorrectDimensionException();
            }
            float[,] newMatrix = new float[GetDimension0(), GetDimension1()];
            for (int i = 0; i < GetDimension0(); i++)
            {
                for (int k = 0; k < GetDimension1(); k++)
                {
                    newMatrix[i, k] = GetItem(i, k) + matrix.GetItem(i, k);
                }
            }
            Matrix result = new(newMatrix);
            return result;
        }

        public Matrix Multiply(float scalar)
        {
            float[,] newMatrix = new float[GetDimension0(), GetDimension1()];
            for (int i = 0; i < GetDimension0(); i++)
            {
                for (int k = 0; k < GetDimension1(); k++)
                {
                    newMatrix[i, k] = GetItem(i, k) * scalar;
                }
            }
            Matrix result = new(newMatrix);
            return result;
        }

        public Matrix Multiply(Matrix matrix)
        {
            float[,] newMatrix = new float[GetDimension0(), matrix.GetDimension1()];
            for (int i = 0; i < GetDimension0(); i++)
            {
                for (int k = 0; k < matrix.GetDimension1(); k++)
                {
                    for (int m = 0; m < GetDimension1(); m++)
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

        public float[,] ToArray()
        {
            return _matrix;
        }

    }
}
