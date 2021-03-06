﻿using Models.NeuralNetModels.ActivationFunctions;
using Xunit;

namespace Models.Test.NeuralNetModels.ActivationFunctions
{
    public class IdentityTests
    {

        private readonly IActivationFunction _sut;

        public IdentityTests()
        {
            _sut = new Identity();
        }

        [Theory]
        [InlineData(3.2f, 3.2f)]
        [InlineData(0f, 0f)]
        [InlineData(-1.1f, -1.1f)]
        public void Execute_ShouldReturnMaxValeFromZeroAndGivenValue(float weightedSum, float result)
        {
            float output = _sut.Execute(weightedSum);

            Assert.Equal(result, output);
        }

        [Theory]
        [InlineData(3.2f, 0f)]
        [InlineData(0f, 0f)]
        [InlineData(-1.1f, 0f)]
        public void GetDerivative_ShouldRetrunZeroOrOne(float weightedSum, float result)
        {
            float output = _sut.GetDerivative(weightedSum);

            Assert.Equal(result, output);
        }

    }
}
