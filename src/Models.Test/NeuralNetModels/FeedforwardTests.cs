using Models.NeuralNetModels;
using Models.NeuralNetModels.ActivationFunctions;
using System;
using Xunit;

namespace Models.Test.NeuralNetModels
{
    public class FeedforwardTests
    {

        [Theory]
        [InlineData(0, 3)]
        [InlineData(-1, 3)]
        public void Constructor_PassNumberOfInputsLessThan1_GetException(int inputsNumber, int outputsNumber)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Feedforward(inputsNumber, outputsNumber, false));
        }

        [Theory]
        [InlineData(2, 0)]
        [InlineData(2, -1)]
        public void Constructor_PassNumberOfOutputsLessThan1_GetException(int inputsNumber, int outputsNumber)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Feedforward(inputsNumber, outputsNumber, false));
        }

        [Fact]
        public void SetInputs_PassArrayOfDifferentDimension_GetException()
        {
            Feedforward network = new(3, 1, false);

            float[] inputs1 = { 1.1f, 1.2f };
            float[] inputs2 = { 1.1f, 1.2f, 1.3f, 1.4f };

            Assert.Throws<ArgumentException>(() => network.SetInputs(inputs1));
            Assert.Throws<ArgumentException>(() => network.SetInputs(inputs2));
        }

        [Fact]
        public void InitializeWeightsWithRandomizer_ProcessShouldReturnValuesBetweenMinusOneAndOne()
        {
            Feedforward network = new(2, 1, false);

            network.InitializeWeightsWithRandomizer();

            for (int i = 0; i < network.Weights.GetLength(0); i++)
            {
                for (int k = 0; k < network.Weights.GetLength(1); k++)
                {
                    Assert.InRange<float>(network.Weights[i, k], -1, 1);
                }
            }
        }

        [Fact]
        public void InitializeWeightsWithSingle_ProcessShouldReturnZero_WhenZerosPassed()
        {
            Feedforward network = new(2, 1, false);

            network.InitializeWeightsWithSingle(0f);

            Assert.Equal(0f, network.GetOutputs()[0]);
        }

        [Fact]
        public void Process_PassZeroesAsInputsWithoutBias_GetZero()
        {
            Feedforward network = new(2, 1, false);
            float[] inputs = { 0f, 0f };
            network.InitializeWeightsWithRandomizer();
            network.SetInputs(inputs);

            network.Process();

            Assert.Equal(0f, network.GetOutputs()[0]);

        }

        [Fact]
        public void Process_PassOnesAsInputs_WithoutBias_GetTwo()
        {
            Feedforward network = new(2, 1, false);
            float[] inputs = { 1f, 1f };
            network.InitializeWeightsWithSingle(1f);
            network.SetInputs(inputs);

            network.Process();

            Assert.Equal(2f, network.GetOutputs()[0]);
        }

        [Fact]
        public void Process_PassOnesAsInputs_WithBias_GetThree()
        {
            Feedforward network = new(2, 1, true);
            float[] inputs = { 1f, 1f };
            network.InitializeWeightsWithSingle(1f);
            network.SetInputs(inputs);

            network.Process();

            Assert.Equal(3f, network.GetOutputs()[0]);
        }

        class EmptyActivationFunction : IActivationFunction
        {
            public float Execute(float weightedSum)
            {
                return weightedSum;
            }
        }

    }
}
