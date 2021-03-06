﻿using Models.NeuralNetModels.ActivationFunctions;
using Xunit;

namespace Models.Test.NeuralNetModels.ActivationFunctions
{
    public class BinaryStepTests
    {

        [Theory]
        [InlineData(3.2f, 0f, 1f)]
        [InlineData(0f, 0f, 1f)]
        [InlineData(-1.1f, 0f, 0f)]
        [InlineData(3.2f, 1f, 1f)]
        [InlineData(0f, 1f, 0f)]
        [InlineData(-1.1f, 1f, 0f)]
        [InlineData(1f, 1f, 1f)]
        public void Execute_ShouldReturnOne_WhenGivenValueGreaterOrEqualThanThreshold(float weightedSum, float threshold, float result)
        {
            IActivationFunction function = new BinaryStep(threshold);

            float output = function.Execute(weightedSum);

            Assert.Equal(result, output);
        }

        [Fact]
        public void GetDerivative_ShouldReturnZero_WhenGivenAnyValue()
        {
            IActivationFunction function = new BinaryStep(0);

            float output = function.GetDerivative(0f);

            Assert.Equal(0f, output);
        }

    }
}
