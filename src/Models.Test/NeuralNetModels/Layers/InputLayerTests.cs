using Models.NeuralNetModels.Exceptions;
using Models.NeuralNetModels.Layers;
using Xunit;

namespace Models.Test
{
    public class InputLayerTests
    {
        [Fact]
        public void Constructor_PassNumberOfItems_GetTheSameNumberOfItems()
        {
            // Arrange
            InputLayer layer = new(5);

            // Act
            int numberOfItems = layer.GetInput().Length;

            // Assert
            Assert.Equal(5, numberOfItems);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Constructor_PassNumberLessThanOne_GetException(int number)
        {
            // Arrange

            // Act

            // Assert
            Assert.Throws<InputLayerException>(() => new InputLayer(number));
        }

        [Theory]
        [InlineData(new int[] { 1, 0, 1, 1, 0 }, 5)]
        [InlineData(new int[] { 1, 1, 1, 1, 1, 1 }, 6)]
        [InlineData(new int[] { 0, 0, 0, 0, 0, 0, 0 }, 7)]
        public void SetInput_PassZerosAndOnesWithTheSameArrayDimension_SuccessAssignment(int[] input, int length)
        {
            // Arrange
            InputLayer layer = new(length);

            // Act
            layer.SetInput(input);
            int[] result = layer.GetInput();

            // Assert
            Assert.Equal(length, result.Length);
            for (int i = 0; i < length; i++)
            {
                Assert.Equal(input[i], result[i]);
            }
        }

        [Theory]
        [InlineData(new int[] { 0, 1, -1 })]
        [InlineData(new int[] { 0, 2, 1 })]
        public void SetInput_PassAnyNumbersWithTheSameArrayDimension_GetException(int[] input)
        {
            // Arrange
            InputLayer layer = new(3);

            // Act


            // Assert
            Assert.Throws<InputLayerException>(() => layer.SetInput(input));
        }

        [Fact]
        public void SetInput_PassArrayWithWrongDimension_GetException()
        {
            // Arrange
            InputLayer layer = new(5);

            // Act


            // Assert
            Assert.Throws<InputLayerException>(() => layer.SetInput(new int[] { 1, 1, 1, 1, }));
            Assert.Throws<InputLayerException>(() => layer.SetInput(new int[] { 1, 1, 1, 1, 1, 1}));
            Assert.Throws<InputLayerException>(() => layer.SetInput(new int[] { }));
        }

    }
}
