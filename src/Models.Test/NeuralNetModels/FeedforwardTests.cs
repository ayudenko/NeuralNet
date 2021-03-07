using Models.NeuralNetModels;
using Models.NeuralNetModels.ActivationFunctions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Models.Test.NeuralNetModels
{
    public class FeedforwardTests
    {

        public FeedforwardTests()
        {

        }

        [Theory]
        [InlineData(new int[] { 0, 3, 2 })]
        [InlineData(new int[] { 1, -3, 2 })]
        [InlineData(new int[] { 1, 3, -2 })]
        public void Constructor_PassNumberOfNodesLessThan1_GetException(int[] layers)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Feedforward(layers, new Identity()));
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3 }, 3)]
        [InlineData(new int[] { 2, 3, 4 }, 4)]
        [InlineData(new int[] { 3, 2, 1 }, 1)]
        [InlineData(new int[] { 2, 2, 2 }, 2)]
        public void Constructor_PassNumberOfOutputs_GetTheSameNumber(int[] layers, int expectedOutputsNumber)
        {
            Feedforward network = new(layers, new Identity());

            var outputs = network.GetOutputs();

            Assert.Equal(expectedOutputsNumber, outputs.Length);
        }

        [Theory]
        [InlineData(new float[] { 1.1f, 1.2f })]
        [InlineData(new float[] { 1.1f, 1.2f, 1.3f })]
        [InlineData(new float[] { 1.1f, 1.2f, 1.3f, 1.4f, 1.5f })]
        public void SetInputs_PassArrayOfDifferentDimension_GetException(float[] inputs)
        {
            Feedforward network = new(new int[] { 4, 3, 2 }, new Identity());

            Assert.Throws<ArgumentException>(() => network.SetInputs(inputs));
        }

        [Theory]
        [InlineData(1f, new float[] { 1f, 2f, 3f, 4f }, new float[] { 31f, 31f })]
        [InlineData(1f, new float[] { 1f, -2f, 3f, -4f }, new float[] { -5f, -5f })]
        public void Process_InitialWeightsWithZeroesAndPassInpt_GetCorrectOutput(float initializer, float[] input, float[] output)
        {
            Feedforward network = new(new int[] { 4, 3, 2 }, new Identity());

            network.InitializeWeightsWithSingle(initializer);
            network.SetInputs(input);

            network.Process();

            Assert.Equal(output, network.GetOutputs());
        }

        [Fact]
        public void Process_PassOneLayer_GetInputsAsOutput()
        {
            Feedforward network = new(new int[] { 2 }, new Identity());
            network.InitializeWeightsWithSingle(0f);
            network.SetInputs(new float[] { 1f, 2f });

            network.Process();

            Assert.Equal(new float[] { 1f, 2f }, network.GetOutputs());
        }

        [Theory]
        [InlineData(new int[] { 3, 2 }, 1f, new float[] { 1f, 2f, 3f }, new float[] { 6f, 6f })]
        [InlineData(new int[] { 3, 2 }, 0f, new float[] { 1f, 2f, 3f }, new float[] { 0f, 0f })]
        public void Process_PassTwoLayers_GetCorrectOutput(int[] layers, float initializer, float[] inputs, float[] expected)
        {
            Feedforward network = new(layers, new Identity());
            network.InitializeWeightsWithSingle(initializer);
            network.SetInputs(inputs);

            network.Process();

            Assert.Equal(expected, network.GetOutputs());
        }

    }

    public class WeightsData
    {

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { new float[] { 0f, 0f }, new float[,] { { 1, 3 } }, 0.4f, new float[,] { { 0.1f, 0.3f } } },
            };

    }

}
