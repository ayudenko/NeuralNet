using Models.NeuralNetModels.ActivationFunctions;
using Xunit;

namespace Models.Test.NeuralNetModels.ActivationFunctions
{
    public class RELUTests
    {

        [Theory]
        [InlineData(3.2f, 3.2f)]
        [InlineData(0f, 0f)]
        [InlineData(-1.1f, 0f)]
        public void Execute_ShouldReturnMaxValeFromZeroAndGivenValue(float weightedSum, float result)
        { 
            IActivationFunction function = new RELU();

            float output = function.Execute(weightedSum);

            Assert.Equal(result, output);
        }

        [Theory]
        [InlineData(3.2f, 1f)]
        [InlineData(0f, 0f)]
        [InlineData(-1.1f, 0f)]
        public void GetDerivative_ShouldRetrunZeroOrOne(float weightedSum, float result)
        {
            IActivationFunction function = new RELU();

            float output = function.GetDerivative(weightedSum);

            Assert.Equal(result, output);
        }

    }
}
