using Models.NeuralNetModels;
using System;
using Xunit;

namespace Models.Test.NeuralNetModels
{
    public class FeedforwardTests
    {

        [Fact]
        public void Constructor_PassNumberOfInputsLessThan1_GetException()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Feedforward(0, 3));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Feedforward(-1, 3));
        }

        [Fact]
        public void Constructor_PassNumberOfOutputsLessThan1_GetException()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Feedforward(2, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Feedforward(2, -1));
        }

    }
}
