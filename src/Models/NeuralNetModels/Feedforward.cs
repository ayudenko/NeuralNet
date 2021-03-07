using Models.Math;
using Models.NeuralNetModels.ActivationFunctions;
using Models.NeuralNetModels.Exceptions;
using System;
using System.Linq;

namespace Models.NeuralNetModels
{
    public class Feedforward
    {

        private float[] _outputs;

        private readonly float[][,] _weights;

        private readonly float[][] _biases;

        private readonly float[][] _layers;

        private readonly IActivationFunction _activationFunction;

        public Feedforward(int[] layers, IActivationFunction activationFunction)
        {
            _activationFunction = activationFunction;
            for (int i = 0; i < layers.Length; i++)
            {
                if (!IsValidNodesNumber(layers[i]))
                {
                    throw new ArgumentOutOfRangeException(paramName: i.ToString(), message: "Incorrect number of layer items.");
                }
            }
            _weights = new float[layers.Length - 1][,];
            _biases = layers.Length > 2 ? new float[layers.Length - 2][] : new float[][] { };
            _layers = new float[layers.Length][];
            _outputs = new float[layers.Last()];
            for (int i = 0; i < layers.Length; i++)
            {
                _layers[i] = new float[layers[i]];
            }
            for (int i = 0; i < _weights.Length; i++)
            {
                _weights[i] = new float[layers[i + 1], layers[i]];
            }
            for (int i = 0; i < _biases.Length; i++)
            {
                _biases[i] = new float[layers[i + 2]];
            }
        }

        public void InitializeWeightsWithRandomizer()
        {
            var rand = new Random();
            foreach (var weights in _weights)
            {
                for (int i = 0; i < weights.GetLength(0); i++)
                {
                    for (int k = 0; k < weights.GetLength(1); k++)
                    {
                        weights[i, k] = (float)rand.NextDouble() * SignMultiplier();
                    }
                }
            }
            foreach (var bias in _biases)
            {
                for (int i = 0; i < bias.Length; i++)
                {
                    bias[i] = (float)rand.NextDouble() * SignMultiplier();
                }
            }

        }

        public void InitializeWeightsWithSingle(float value)
        {
            foreach (var weights in _weights)
            {
                for (int i = 0; i < weights.GetLength(0); i++)
                {
                    for (int k = 0; k < weights.GetLength(1); k++)
                    {
                        weights[i, k] = value;
                    }
                }
            }
            foreach (var bias in _biases)
            {
                for (int i = 0; i < bias.Length; i++)
                {
                    bias[i] = value;
                }
            }
        }

        public void Process()
        {
            Matrix[] weightsMatrixes = new Matrix[_weights.Length];
            for (int i = 0; i < _weights.Length; i++)
            {
                weightsMatrixes[i] = new Matrix(_weights[i]);
            }
            Matrix[] biasesMatrixes = new Matrix[_biases.Length];
            for (int i = 0; i < _biases.Length; i++)
            {
                biasesMatrixes[i] = new Matrix(_biases[i]);
            }
            Matrix[] layersMatrixes = new Matrix[_layers.Length];
            for (int i = 0; i < _layers.Length; i++)
            {
                layersMatrixes[i] = new Matrix(_layers[i]);
            }

            for (int i = 0; i < layersMatrixes.Length - 1; i++)
            {
                Matrix inputMatrix = layersMatrixes[i].Transpose();
                Matrix outputMatrix = weightsMatrixes[i].Multiply(inputMatrix);
                if (i > 0)
                {
                    Matrix biasMatrix = biasesMatrixes[i-1].Transpose();
                    outputMatrix = outputMatrix.Sum(biasMatrix);
                }
                float[,] outputMatrixArray = outputMatrix.ToArray();
                float[] convertedOutputsMatrixArray = ConvertTwoDimensionalArrayToSingleDimensionalArray(outputMatrixArray);
                float[] outputs = ApplyActivationFunction(convertedOutputsMatrixArray);
                _layers[i + 1] = outputs;
                layersMatrixes[i + 1] = new Matrix(outputs);
            }
            _outputs = _layers[^1];
        }

        public void SetInputs(float[] inputs)
        {
            if (!IsValidInputs(inputs.Length))
            {
                throw new ArgumentException(paramName: nameof(inputs), message: "Incorrect number of input values.");
            }
            _layers[0] = inputs;
        }

        public float[] GetOutputs() => _outputs;

        public static int SignMultiplier()
        {
            int multiplier = 1;
            var rand = new Random();
            int value = rand.Next(0, 2);
            if (value == 0)
            {
                multiplier = -1;
            }
            return multiplier;
        }

        private float[] ApplyActivationFunction(float[] weightedSums)
            => weightedSums.Select(x => _activationFunction.Execute(x)).ToArray();

        private static float[] ConvertTwoDimensionalArrayToSingleDimensionalArray(float[,] mArray)
        {
            float[] sArray;
            if (mArray.GetLength(0) == 1)
            {
                sArray = new float[mArray.GetLength(1)];
                for (int i = 0; i < mArray.GetLength(1); i++)
                {
                    sArray[i] = mArray[0, i];
                }
            }
            else if (mArray.GetLength(1) == 1)
            {
                sArray = new float[mArray.GetLength(0)];
                for (int i = 0; i < mArray.GetLength(0); i++)
                {
                    sArray[i] = mArray[i, 0];
                }
            }
            else
            {
                throw new IncorrectArrayDimensionsException();
            }
            return sArray;
        }

        private bool IsValidInputs(int inputsLength) => inputsLength == _layers[0].Length;

        private static bool IsValidNodesNumber(int nodesNumber) => nodesNumber > 0;

    }
}
