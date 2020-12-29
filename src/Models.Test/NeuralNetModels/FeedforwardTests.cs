using Models.NeuralNetModels;
using Models.NeuralNetModels.ActivationFunctions;
using Moq;
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
            Assert.Throws<ArgumentOutOfRangeException>(() => new Feedforward(inputsNumber, outputsNumber));
        }

        [Theory]
        [InlineData(2, 0)]
        [InlineData(2, -1)]
        public void Constructor_PassNumberOfOutputsLessThan1_GetException(int inputsNumber, int outputsNumber)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Feedforward(inputsNumber, outputsNumber));
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

        [Fact]
        public void InitializeWeightsWithSingle_ProcessShouldReturnZero_WhenZerosPassed()
        {
            Feedforward network = new(2, 1);
            network.ActivationFunction = new EmptyActivationFunction();
            network.SetInputs(new float[] { 1f, 1f });
            
            network.InitializeWeightsWithSingle(0f);

            Assert.Equal(0f, network.GetOutputs()[0]);
        }


        /*[Fact]
        public void Process_PassInputsAndActivationFunction()
        {
            Feedforward network = new(2, 1);
            float[] inputs = { 0f, 0f };
            network.SetInputs(inputs);
            network.ActivationFunction = new BinaryStep();

            network.Process();



        }*/

        class EmptyActivationFunction : IActivationFunction
        {
            public float Execute(float weightedSum)
            {
                return weightedSum;
            }
        }

    }
}
