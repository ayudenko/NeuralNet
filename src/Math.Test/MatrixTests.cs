using Math.Exceptions;
using System;
using Xunit;

namespace Math.Test
{
    public class MatrixTests
    {

        [Fact]
        public void Constructor_PassArray()
        {
            float[] matrixItems = new float[] { 1, 2, 3 };
            
            Matrix matrix = new(matrixItems);

            Assert.Equal(1, matrix.GetDimension0());
            Assert.Equal(3, matrix.GetDimension1());
            Assert.Equal(1, matrix.GetItem(0, 0));
            Assert.Equal(2, matrix.GetItem(0, 1));
            Assert.Equal(3, matrix.GetItem(0, 2));
        }

        [Theory, InlineData(0, 0, 1),
        InlineData(0, 1, 2),
        InlineData(1, 0, 3),
        InlineData(2, 1, 6)]
        public void GetItem_PassCorrectIndexes_GetItem(int row, int column, float result)
        {
            float[,] matrixItems = new float[,] { { 1, 2 }, { 3, 4 }, { 3, 6 } };
            Matrix matrix = new(matrixItems);

            float item = matrix.GetItem(row, column);

            Assert.Equal(item, result);
        }

        [Fact]
        public void GetItem_PassIncorrectIndexes_GetException()
        {
            float[,] matrixItems = new float[,] { { 1, 2 }, { 3, 4 }, { 3, 6 } };
            Matrix matrix = new(matrixItems);

            Assert.Throws<IndexOutOfRangeException>(() => matrix.GetItem(5, 3));
        }

        [Fact]
        public void Sum_PassMatrixWithSameDimensions_GetCorrectResult()
        {
            float[,] matrixItems1 = new float[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Matrix matrix1 = new(matrixItems1);
            float[,] matrixItems2 = new float[,] { { 2, 3 }, { 4, 5 }, { 6, 7 } };
            Matrix matrix2 = new(matrixItems2);

            Matrix result = matrix1.Sum(matrix2);

            Assert.Equal(3, result.GetItem(0, 0));
            Assert.Equal(5, result.GetItem(0, 1));
            Assert.Equal(7, result.GetItem(1, 0));
            Assert.Equal(9, result.GetItem(1, 1));
            Assert.Equal(11, result.GetItem(2, 0));
            Assert.Equal(13, result.GetItem(2, 1));
        }

        [Fact]
        public void Sum_PassMatrixWithDifferentDimensions_GetException()
        {
            float[,] matrixItems1 = new float[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Matrix matrix1 = new(matrixItems1);
            float[,] matrixItems2 = new float[,] { { 2, 3, 4 }, { 5, 6, 7 } };
            Matrix matrix2 = new(matrixItems2);

            float[,] matrixItems3 = new float[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Matrix matrix3 = new(matrixItems3);
            float[,] matrixItems4 = new float[,] { { 2, 3, 4 }, { 5, 6, 7 }, { 8, 9, 10 } };
            Matrix matrix4 = new(matrixItems4);

            Assert.Throws<IncorrectDimensionException>(() => matrix1.Sum(matrix2));
            Assert.Throws<IncorrectDimensionException>(() => matrix3.Sum(matrix4));
        }

        [Fact]
        public void GetDimension0_GetCorrectDimension()
        {
            float[,] matrixItems = new float[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Matrix matrix = new(matrixItems);

            int dimension = matrix.GetDimension0();

            Assert.Equal(3, dimension);
        }

        [Fact]
        public void GetDimension1_GetCorrectDimension()
        {
            float[,] matrixItems = new float[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Matrix matrix = new(matrixItems);

            int dimension = matrix.GetDimension1();

            Assert.Equal(2, dimension);
        }

        [Fact]
        public void Multiply_PassScalar_GetNewMatrix()
        {
            float[,] matrixItems = new float[,] { { 1, 1 }, { 1, 1 }, { 1, 1 } };
            Matrix matrix = new(matrixItems);
            float scalar = 2.5f;

            Matrix result = matrix.Multiply(scalar);

            Assert.Equal(2.5f, result.GetItem(0, 0));
            Assert.Equal(2.5f, result.GetItem(0, 1));
            Assert.Equal(2.5f, result.GetItem(1, 0));
            Assert.Equal(2.5f, result.GetItem(1, 1));
            Assert.Equal(2.5f, result.GetItem(2, 0));
            Assert.Equal(2.5f, result.GetItem(2, 1));
        }

        [Fact]
        public void Multiply_PassMatrix_GetNewMatrix()
        {
            float[,] matrixItems1 = new float[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Matrix matrix1 = new(matrixItems1);
            float[,] matrixItems2 = new float[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            Matrix matrix2 = new(matrixItems2);

            Matrix result1 = matrix1.Multiply(matrix2);

            Assert.Equal(3, result1.GetDimension0());
            Assert.Equal(3, result1.GetDimension1());
            Assert.Equal(9f, result1.GetItem(0, 0));
            Assert.Equal(12f, result1.GetItem(0, 1));
            Assert.Equal(15f, result1.GetItem(0, 2));
            Assert.Equal(19f, result1.GetItem(1, 0));
            Assert.Equal(26f, result1.GetItem(1, 1));
            Assert.Equal(33f, result1.GetItem(1, 2));
            Assert.Equal(29f, result1.GetItem(2, 0));
            Assert.Equal(40f, result1.GetItem(2, 1));
            Assert.Equal(51f, result1.GetItem(2, 2));


            float[,] matrixItems3 = new float[,] { { 3, 2, 1 }, { 0, 1, 2 } };
            Matrix matrix3 = new(matrixItems3);
            float[,] matrixItems4 = new float[,] { { 1 }, { 2 }, { 3 } };
            Matrix matrix4 = new(matrixItems4);

            Matrix result2 = matrix3.Multiply(matrix4);

            Assert.Equal(2, result2.GetDimension0());
            Assert.Equal(1, result2.GetDimension1());
            Assert.Equal(10f, result2.GetItem(0, 0));
            Assert.Equal(8f, result2.GetItem(1, 0));
        }

        [Fact]
        public void Transpose_GetTransposedMatrix_WhenMatrixIsPassed()
        {
            float[,] matrixItems1 = new float[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            float[,] matrixItems2 = new float[,] { { 1, 2 }, { 3, 4 } };
            float[,] matrixItems3 = new float[,] { { 1, 2, 3 } };
            float[,] matrixItems4 = new float[,] { { 1 }, { 2 }, { 3 } };

            Matrix matrix1 = new(matrixItems1);
            Matrix matrix2 = new(matrixItems2);
            Matrix matrix3 = new(matrixItems3);
            Matrix matrix4 = new(matrixItems4);

            Matrix transposedMatrix1 = matrix1.Transpose();
            Matrix transposedMatrix2 = matrix2.Transpose();
            Matrix transposedMatrix3 = matrix3.Transpose();
            Matrix transposedMatrix4 = matrix4.Transpose();

            Assert.Equal(3, transposedMatrix1.GetDimension0());
            Assert.Equal(2, transposedMatrix1.GetDimension1());
            Assert.Equal(1, transposedMatrix1.GetItem(0, 0));
            Assert.Equal(4, transposedMatrix1.GetItem(0, 1));
            Assert.Equal(2, transposedMatrix1.GetItem(1, 0));
            Assert.Equal(5, transposedMatrix1.GetItem(1, 1));
            Assert.Equal(3, transposedMatrix1.GetItem(2, 0));
            Assert.Equal(6, transposedMatrix1.GetItem(2, 1));

            Assert.Equal(2, transposedMatrix2.GetDimension0());
            Assert.Equal(2, transposedMatrix2.GetDimension1());
            Assert.Equal(1, transposedMatrix2.GetItem(0, 0));
            Assert.Equal(3, transposedMatrix2.GetItem(0, 1));
            Assert.Equal(2, transposedMatrix2.GetItem(1, 0));
            Assert.Equal(4, transposedMatrix2.GetItem(1, 1));

            Assert.Equal(3, transposedMatrix3.GetDimension0());
            Assert.Equal(1, transposedMatrix3.GetDimension1());
            Assert.Equal(1, transposedMatrix3.GetItem(0, 0));
            Assert.Equal(2, transposedMatrix3.GetItem(1, 0));
            Assert.Equal(3, transposedMatrix3.GetItem(2, 0));

            Assert.Equal(1, transposedMatrix4.GetDimension0());
            Assert.Equal(3, transposedMatrix4.GetDimension1());
            Assert.Equal(1, transposedMatrix4.GetItem(0, 0));
            Assert.Equal(2, transposedMatrix4.GetItem(0, 1));
            Assert.Equal(3, transposedMatrix4.GetItem(0, 2));
        }
    }
}
