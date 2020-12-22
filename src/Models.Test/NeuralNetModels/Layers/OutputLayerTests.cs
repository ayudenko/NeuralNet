using Models.NeuralNetModels.Exceptions;
using Models.NeuralNetModels.Layers;
using Xunit;

namespace Models.Test.NeuralNetModels.Layers
{
    public class OutputLayerTests
    {

        [Fact]
        public void SetOutput_PassItemsWithCorrectDimensions_GetTheSameItems()
        {
            // Arrange
            OutputLayer layer = new(5);

            // Act
            var output = new float[5] { 1.1f, 1.2f, 2.3f, 2.4f, 3.5f };
            layer.SetOutput(output);
            float[] result = layer.GetOutput();

            // Assert
            Assert.Equal(5, result.Length);
            Assert.Equal(1.1f, result[0]);
            Assert.Equal(1.2f, result[1]);
            Assert.Equal(2.3f, result[2]);
            Assert.Equal(2.4f, result[3]);
            Assert.Equal(3.5f, result[4]);
        }

        [Fact]
        public void SetOutput_PassItemsWithIncorrectDimensions_GetException()
        {
            // Arrange
            OutputLayer layer = new(5);

            // Act
            var output1 = new float[4] { 1.1f, 1.2f, 2.3f, 2.4f };
            var output2 = new float[6] { 1.1f, 1.2f, 2.3f, 2.4f, 3.5f, 3.6f };
            var output3 = new float[0];

            // Assert
            Assert.Throws<OutputLayerException>(() => layer.SetOutput(output1));
            Assert.Throws<OutputLayerException>(() => layer.SetOutput(output2));
            Assert.Throws<OutputLayerException>(() => layer.SetOutput(output3));
        }

        [Fact]
        public void GetOutpt_PassItems_GetTheSameItems()
        {
            // Arrange
            OutputLayer layer = new(5);

            // Act
            var output = new float[5] { 1.1f, 1.2f, 2.3f, 2.4f, 3.5f };
            layer.SetOutput(output);
            float[] result = layer.GetOutput();

            // Assert
            Assert.Equal(5, result.Length);
            Assert.Equal(1.1f, result[0]);
            Assert.Equal(1.2f, result[1]);
            Assert.Equal(2.3f, result[2]);
            Assert.Equal(2.4f, result[3]);
            Assert.Equal(3.5f, result[4]);
        }

    }
}
