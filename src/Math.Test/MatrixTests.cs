using Math.Exceptions;
using System;
using Xunit;

namespace Math.Test
{
    public class MatrixTests
    {
        [Theory, InlineData(0, 0, 1),
        InlineData(0, 1, 2),
        InlineData(1, 0, 3),
        InlineData(2, 1, 6)]
        public void GetItem_PassCorrectIndexes_GetItem(Int32 x, Int32 y, Single result)
        {
            // Arrange
            Single[,] matrixItems = new Single[,] { { 1, 2 }, { 3, 4 }, { 3, 6 } };
            Matrix matrix = new(matrixItems);

            // Act
            Single item = matrix.GetItem(x, y);

            // Assert
            Assert.Equal(item, result);
        }

        [Fact]
        public void GetItem_PassIncorrectIndexes_GetException()
        {
            // Arrange
            Single[,] matrixItems = new Single[,] { { 1, 2 }, { 3, 4 }, { 3, 6 } };
            Matrix matrix = new(matrixItems);

            // Act
            Assert.Throws<IndexOutOfRangeException>(() => matrix.GetItem(5, 3));

            // Assert
        }

        [Fact]
        public void Sum_PassMatrixWithSameDimensions_GetCorrectResult()
        {
            // Arrange
            Single[,] matrixItems1 = new Single[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Matrix matrix1 = new(matrixItems1);
            Single[,] matrixItems2 = new Single[,] { { 2, 3 }, { 4, 5 }, { 6, 7 } };
            Matrix matrix2 = new(matrixItems2);

            // Act
            Matrix result = matrix1.Sum(matrix2);

            // Assert
            Assert.Equal(3, result.GetItem(0,0));
            Assert.Equal(5, result.GetItem(0,1));
            Assert.Equal(7, result.GetItem(1,0));
            Assert.Equal(9, result.GetItem(1,1));
            Assert.Equal(11, result.GetItem(2,0));
            Assert.Equal(13, result.GetItem(2,1));
        }

        [Fact]
        public void Sum_PassMatrixWithDifferentDimensions_GetException()
        {
            // Arrange
            Single[,] matrixItems1 = new Single[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Matrix matrix1 = new(matrixItems1);
            Single[,] matrixItems2 = new Single[,] { { 2, 3, 4}, { 5, 6, 7 } };
            Matrix matrix2 = new(matrixItems2);

            Single[,] matrixItems3 = new Single[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Matrix matrix3 = new(matrixItems3);
            Single[,] matrixItems4 = new Single[,] { { 2, 3, 4 }, { 5, 6, 7 }, { 8, 9, 10 } };
            Matrix matrix4 = new(matrixItems4);

            // Act
            Assert.Throws<IncorrectDimensionException>(() => matrix1.Sum(matrix2));
            Assert.Throws<IncorrectDimensionException>(() => matrix3.Sum(matrix4));

            // Assert
        }

        [Fact]
        public void GetDimension0_GetCorrectDimension()
        {
            // Arrange
            Single[,] matrixItems = new Single[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Matrix matrix = new(matrixItems);

            // Act
            Int32 dimension = matrix.GetDimension0();

            // Assert
            Assert.Equal(3, dimension);
        }

        [Fact]
        public void GetDimension1_GetCorrectDimension()
        {
            // Arrange
            Single[,] matrixItems = new Single[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            Matrix matrix = new(matrixItems);

            // Act
            Int32 dimension = matrix.GetDimension1();

            // Assert
            Assert.Equal(2, dimension);
        }

        [Fact]
        public void Multiply_PassScalar_GetNewMatrix()
        {
            // Arrange
            Single[,] matrixItems = new Single[,] { { 1, 1 }, { 1, 1 }, { 1, 1 } };
            Matrix matrix = new(matrixItems);
            Single scalar = 2.5F;

            // Act
            Matrix result = matrix.Multiply(scalar);

            // Assert
            Assert.Equal(2.5F, result.GetItem(0, 0));
            Assert.Equal(2.5F, result.GetItem(0, 1));
            Assert.Equal(2.5F, result.GetItem(1, 0));
            Assert.Equal(2.5F, result.GetItem(1, 1));
            Assert.Equal(2.5F, result.GetItem(2, 0));
            Assert.Equal(2.5F, result.GetItem(2, 1));
        }

    }
}