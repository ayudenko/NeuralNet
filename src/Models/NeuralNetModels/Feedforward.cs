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

        private readonly Random _rand = new Random();

        public Feedforward(int[] layers, IActivationFunction activationFunction)
        {
            _activationFunction = activationFunction;
            ThrowExceptionForInvalidLayerDimension(layers);
            _weights = new float[layers.Length - 1][,];
            _biases = layers.Length > 2 ? new float[layers.Length - 2][] : Array.Empty<float[]>();
            _layers = new float[layers.Length][];
            _outputs = new float[layers.Last()];
            InitializeNetworkData(layers);
        }

        public void InitializeWeightsWithRandomizer()
        {
            InitializeWeights(GetRanomizedValue);
            InitializeBiases(GetRanomizedValue);
            var a = _biases;
        }

        public void InitializeWeightsWithSingle(float value)
        {
            InitializeWeights(() => value);
            InitializeBiases(() => value);
        }

        private float GetRanomizedValue()
            => (float)_rand.NextDouble() * SignMultiplier();

        private void InitializeWeights(Func<float> initializerValue)
        {
            foreach (var weights in _weights)
            {
                for (int i = 0; i < weights.GetLength(0); i++)
                {
                    for (int k = 0; k < weights.GetLength(1); k++)
                    {
                        weights[i, k] = initializerValue.Invoke();
                    }
                }
            }
        }

        private void InitializeBiases(Func<float> initializerValue)
        {
            foreach (var biases in _biases)
            {
                for (int i = 0; i < biases.Length; i++)
                {
                    biases[i] = initializerValue.Invoke();
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
                    Matrix biasMatrix = biasesMatrixes[i - 1].Transpose();
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

        public int SignMultiplier()
        {
            int multiplier = 1;
            int value = _rand.Next(0, 2);
            if (value == 0)
            {
                multiplier = -1;
            }
            return multiplier;
        }

        private void InitializeNetworkData(int[] layers)
        {
            InitializeLayers(layers);
            InitializeWeights(layers);
            InitializeBiases(layers);
        }

        private void InitializeLayers(int[] layers)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                _layers[i] = new float[layers[i]];
            }
        }

        private void InitializeWeights(int[] layers)
        {
            for (int i = 0; i < _weights.Length; i++)
            {
                _weights[i] = new float[layers[i + 1], layers[i]];
            }
        }

        private void InitializeBiases(int[] layers)
        {
            for (int i = 0; i < _biases.Length; i++)
            {
                _biases[i] = new float[layers[i + 2]];
            }
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

        private void ThrowExceptionForInvalidLayerDimension(int[] layers)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                if (!IsValidNodesNumber(layers[i]))
                {
                    throw new ArgumentOutOfRangeException(paramName: i.ToString(), message: "Incorrect number of layer items.");
                }
            }
        }

        private bool IsValidInputs(int inputsLength) => inputsLength == _layers[0].Length;

        private static bool IsValidNodesNumber(int nodesNumber) => nodesNumber > 0;

    }
}
