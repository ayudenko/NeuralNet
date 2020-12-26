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
            Assert.Throws<ArgumentOutOfRangeException>(() => new Feedforward(0, 3));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Feedforward(-1, 3));
        }

        [Fact]
        public void Constructor_PassNumberOfOutputsLessThan1_GetException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Feedforward(2, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Feedforward(2, -1));
        }

        [Fact]
        public void SetInputs_PassArrayOfDifferentDimension_GetException()
        {
            Feedforward network = new(3, 1);

            float[] inputs1 = { 1.1f, 1.2f };
            float[] inputs2 = { 1.1f, 1.2f, 1.3f, 1.4f };
            
            Assert.Throws<ArgumentException>(() => network.SetInputs(inputs1));
            Assert.Throws<ArgumentException>(() => network.SetInputs(inputs2));
        }

    }
}
