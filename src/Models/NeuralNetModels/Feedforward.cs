using Math;
using Models.NeuralNetModels.ActivationFunctions;
using Models.NeuralNetModels.Exceptions;
using System;

namespace Models.NeuralNetModels
{
    public class Feedforward
    {

        private float[] _inputs;
        private float[] _outputs;
        private float[,] _weights;

        public IActivationFunction ActivationFunction { get; set; }

        public Feedforward(int inputsNumber, int outputsNumber)
        {
            if (!IsValidInputsNumber(inputsNumber))
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(inputsNumber), message: "Incorrent number of input items.");
            }
            if (!IsValidOutputsNumber(outputsNumber))
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(outputsNumber), message: "Incorrent number of output items.");
            }
            _inputs = new float[inputsNumber];
            _outputs = new float[outputsNumber];
            _weights = new float[outputsNumber, inputsNumber];
        }

        public void InitializeWeightsWithRandomizer()
        {
            var rand = new Random();
            for (int i = 0; i < _weights.GetLength(0); i++)
            {
                for (int k = 0; k < _weights.GetLength(1); k++)
                {
                    _weights[i, k] = (float)rand.NextDouble();
                }
            }
        }

        public void InitializeWeightsWithSingle(float value)
        {
            for (int i = 0; i < _weights.GetLength(0); i++)
            {
                for (int k = 0; k < _weights.GetLength(1); k++)
                {
                    _weights[i, k] = value;
                }
            }
        }

        public void Process()
        {
            Matrix inputMatrix = new(_inputs);
            Matrix weightsMatrix = new(_weights);
            Matrix inputMatrixTransposed = inputMatrix.Transpose();
            Matrix outputsMatrix = weightsMatrix.Multiply(inputMatrixTransposed);
            float[,] outputsMatrixArray = outputsMatrix.ToArray();
            _outputs = ConvertTwoDimensionalArrayToSingleDimensionalArray(outputsMatrixArray);
        }

        public void SetInputs(float[] inputs)
        {
            if (!IsValidInputs(inputs))
            {
                throw new ArgumentException(paramName: nameof(inputs), message: "Wrong number of input values.");
            }
            _inputs = inputs;
        }

        public float[] GetOutputs() => _outputs;

        private float[] ConvertTwoDimensionalArrayToSingleDimensionalArray(float[,] mArray)
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

        private bool IsValidInputs(float[] inputs) => inputs.Length == _inputs.Length;

        private static bool IsValidInputsNumber(int inputsNumber) => inputsNumber > 0;

        private static bool IsValidOutputsNumber(int outputsNumber) => outputsNumber > 0;

    }
}
