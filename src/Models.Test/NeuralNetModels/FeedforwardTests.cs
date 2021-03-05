using Models.NeuralNetModels;
using Models.NeuralNetModels.ActivationFunctions;
using Moq;
using System;
using System.Collections.Generic;
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

        [Theory]
        [InlineData(new float[] { 1.1f, 1.2f })]
        [InlineData(new float[] { 1.1f, 1.2f, 1.3f, 1.4f })]
        public void SetInputs_PassArrayOfDifferentDimension_GetException(float[] inputs)
        {
            Feedforward network = new(3, 1, false);

            Assert.Throws<ArgumentException>(() => network.SetInputs(inputs));
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

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(-2, -2)]
        public void InitializeWeightsWithSingle_ProcessShouldReturnZero_WhenZerosPassed(float initialWeight, float expectedWeight)
        {
            Feedforward network = new(2, 1, false);

            network.InitializeWeightsWithSingle(initialWeight);

            for (int i = 0; i < network.Weights.GetLength(0); i++)
            {
                for (int k = 0; k < network.Weights.GetLength(1); k++)
                {
                    Assert.Equal(expectedWeight, network.Weights[i, k]);
                }
            }
        }

        [Fact]
        public void Process_PassZeroesAsInputsWithoutBiasAndInitializeWeightsWithRandomizerAndEmptyActivationFunction_GetZero()
        {
            Feedforward network = new(2, 1, false);
            float[] inputs = { 0f, 0f };
            network.InitializeWeightsWithRandomizer();
            network.SetInputs(inputs);
            network.ActivationFunction = new EmptyActivationFunction();

            network.Process();

            Assert.Equal(0f, network.GetOutputs()[0]);

        }

        [Theory]
        [InlineData(new float[] { 0f, 0f }, -1f, 0f)]
        [InlineData(new float[] { 0f, 1f }, -1f, -1f)]
        [InlineData(new float[] { 1f, 0f }, -1f, -1f)]
        [InlineData(new float[] { 1f, 1f }, -1f, -2f)]
        [InlineData(new float[] { 0f, -1f }, -1f, 1f)]
        [InlineData(new float[] { -1f, 0f }, -1f, 1f)]
        [InlineData(new float[] { -1f, -1f }, -1f, 2f)]
        [InlineData(new float[] { 0f, 0f }, 0f, 0f)]
        [InlineData(new float[] { 0f, 1f }, 0f, 0f)]
        [InlineData(new float[] { 1f, 0f }, 0f, 0f)]
        [InlineData(new float[] { 1f, 1f }, 0f, 0f)]
        [InlineData(new float[] { 0f, -1f }, 0f, 0f)]
        [InlineData(new float[] { -1f, 0f }, 0f, 0f)]
        [InlineData(new float[] { -1f, -1f }, 0f, 0f)]
        [InlineData(new float[] { 0f, 0f }, 1f, 0f)]
        [InlineData(new float[] { 0f, 1f }, 1f, 1f)]
        [InlineData(new float[] { 1f, 0f }, 1f, 1f)]
        [InlineData(new float[] { 1f, 1f }, 1f, 2f)]
        [InlineData(new float[] { 0f, -1f }, 1f, -1f)]
        [InlineData(new float[] { -1f, 0f }, 1f, -1f)]
        [InlineData(new float[] { -1f, -1f }, 1f, -2f)]
        public void Process_PassInputsAndInitializeWeightsWithSingleAndEmptyActivationFunction_WithoutBias(float[] inputs, float weight, float expected)
        {
            Feedforward network = new(2, 1, false);
            network.InitializeWeightsWithSingle(weight);
            network.ActivationFunction = new EmptyActivationFunction();
            network.SetInputs(inputs);

            network.Process();

            Assert.Equal(expected, network.GetOutputs()[0]);
        }

        [Theory]
        [InlineData(new float[] { 0f, 0f }, -1f, -1f)]
        [InlineData(new float[] { 0f, 1f }, -1f, -2f)]
        [InlineData(new float[] { 1f, 0f }, -1f, -2f)]
        [InlineData(new float[] { 1f, 1f }, -1f, -3f)]
        [InlineData(new float[] { 0f, -1f }, -1f, 0f)]
        [InlineData(new float[] { -1f, 0f }, -1f, 0f)]
        [InlineData(new float[] { -1f, -1f }, -1f, 1f)]

        [InlineData(new float[] { 0f, 0f }, 0f, 0f)]
        [InlineData(new float[] { 0f, 1f }, 0f, 0f)]
        [InlineData(new float[] { 1f, 0f }, 0f, 0f)]
        [InlineData(new float[] { 1f, 1f }, 0f, 0f)]
        [InlineData(new float[] { 0f, -1f }, 0f, 0f)]
        [InlineData(new float[] { -1f, 0f }, 0f, 0f)]
        [InlineData(new float[] { -1f, -1f }, 0f, 0f)]

        [InlineData(new float[] { 0f, 0f }, 1f, 1f)]
        [InlineData(new float[] { 0f, 1f }, 1f, 2f)]
        [InlineData(new float[] { 1f, 0f }, 1f, 2f)]
        [InlineData(new float[] { 1f, 1f }, 1f, 3f)]
        [InlineData(new float[] { 0f, -1f }, 1f, 0f)]
        [InlineData(new float[] { -1f, 0f }, 1f, 0f)]
        [InlineData(new float[] { -1f, -1f }, 1f, -1f)]
        public void Process_PassInputsAndInitializeWeightsWithSingleAndEmptyActivationFunction_WithBias(float[] inputs, float weight, float expected)
        {
            Feedforward network = new(2, 1, true);
            network.InitializeWeightsWithSingle(weight);
            network.SetInputs(inputs);
            network.ActivationFunction = new EmptyActivationFunction();

            network.Process();

            Assert.Equal(expected, network.GetOutputs()[0]);
        }

        [Fact]
        public void Process_PassOnesAsInputsWithBinaryStepActivationFunction_ActivationFunctionShouldBeExecuted()
        {
            Feedforward network = new(2, 1, false);
            float[] inputs = new float[] { 1f, 1f };
            network.InitializeWeightsWithSingle(1f);
            Mock<IActivationFunction> activationFunctionMock = new Mock<IActivationFunction>();
            network.ActivationFunction = activationFunctionMock.Object;
            network.SetInputs(inputs);

            network.Process();
            float output = network.GetOutputs()[0];

            activationFunctionMock.Verify(v => v.Execute(It.IsAny<float>()));

        }

        [Theory]
        [MemberData(nameof(WeightsData.Data), MemberType = typeof(WeightsData))]
        public void AdjustWeightsWithError_SetWeightsWithSingleAndApplyErorAndEmptyActivatinFunction(float[] inputs, float[,] initialWeights, float error, bool hasBias, float[,] expectedWeights)
        {
            Feedforward network = new(2, 1, hasBias);
            network.Weights = initialWeights;
            network.ActivationFunction = new EmptyActivationFunction();
            network.SetInputs(inputs);

            network.Process();
            network.AdjustWeightsWithError(error);
            float[,] weights = network.Weights;

            for (int i = 0; i < weights.GetLength(0); i++)
            {
                for (int k = 0; k < weights.GetLength(1); k++)
                {
                    Assert.Equal(expectedWeights[i, k], weights[i, k]);
                }
            }
        }

        class EmptyActivationFunction : IActivationFunction
        {
            public float Execute(float weightedSum)
            {
                return weightedSum;
            }

            public float GetDerivative(float input)
            {
                return 0;
            }
        }

    }

    public class WeightsData
    {

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { new float[] { 0f, 0f }, new float[,] { { 1, 3 } }, 0.4f, false, new float[,] { { 0.1f, 0.3f } } },
            };

    }

}
