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

        [Fact]
        public void GetInput_PassInputArray_GetTheSameArray()
        {
            // Arrange
            InputLayer layer = new(5);

            // Act
            layer.SetInput(new int[] { 1, 0, 1, 1, 0 });
            int[] input = layer.GetInput();

            // Assert
            Assert.Equal(5, input.Length);
            Assert.Equal(1, input[0]);
            Assert.Equal(0, input[1]);
            Assert.Equal(1, input[2]);
            Assert.Equal(1, input[3]);
            Assert.Equal(0, input[4]);
        }

        [Fact]
        public void AddBias_GetTrueFromHasBiasMethod()
        {
            // Arrange
            InputLayer layer1 = new(5);
            InputLayer layer2 = new(5);

            // Act
            layer1.AddBias();
            layer2.AddBias();

            // Assert
            Assert.True(layer1.HasBias());
            Assert.True(layer2.HasBias());
        }

        [Fact]
        public void RemoveBias_GetFalseFromHasBiasMethod()
        {
            // Arrange
            InputLayer layer1 = new(5);
            InputLayer layer2 = new(5);

            // Act
            layer1.RemoveBias();
            layer2.AddBias();
            layer2.RemoveBias();

            // Assert
            Assert.False(layer1.HasBias());
            Assert.False(layer2.HasBias());
        }

        [Fact]
        public void HasBias_DefaultValue_GetFalse()
        {
            // Arrange
            InputLayer layer = new(5);

            // Act
            bool hasBias = layer.HasBias();

            // Arrange
            Assert.False(hasBias);

        }

        [Fact]
        public void HasBias_AddBias_GetTrue()
        {
            // Arrange
            InputLayer layer = new(5);

            // Act
            layer.AddBias();
            bool hasBias = layer.HasBias();

            // Arrange
            Assert.True(hasBias);
        }

        [Fact]
        public void HasBias_RemoveBias_GetFalse()
        {
            // Arrange
            InputLayer layer = new(5);

            // Act
            layer.AddBias();
            layer.RemoveBias();
            bool hasBias = layer.HasBias();

            // Arrange
            Assert.False(hasBias);
        }

    }
}
