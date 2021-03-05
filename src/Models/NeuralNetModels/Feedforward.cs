﻿using Models.Math;
using Models.NeuralNetModels.ActivationFunctions;
using Models.NeuralNetModels.Exceptions;
using System;
using System.Linq;

namespace Models.NeuralNetModels
{
    public class Feedforward
    {

        private float[] _inputs;
        private float[] _outputs;

        public float[,] Weights { get; set; }

        public IActivationFunction ActivationFunction { get; set; }

        public Feedforward(int inputsNumber, int outputsNumber)
        {
            if (!IsValidInputsNumber(inputsNumber))
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(inputsNumber), message: "Incorrect number of input items.");
            }
            if (!IsValidOutputsNumber(outputsNumber))
            {
                throw new ArgumentOutOfRangeException(paramName: nameof(outputsNumber), message: "Incorrect number of output items.");
            }
            inputsNumber++;
            _inputs = new float[inputsNumber];
            _outputs = new float[outputsNumber];
            Weights = new float[_outputs.Length, inputsNumber];
        }

        public void InitializeWeightsWithRandomizer()
        {
            var rand = new Random();
            for (int i = 0; i < Weights.GetLength(0); i++)
            {
                for (int k = 0; k < Weights.GetLength(1); k++)
                {
                    Weights[i, k] = (float)rand.NextDouble() * SignMultiplier();
                }
            }
        }

        public void InitializeWeightsWithSingle(float value)
        {
            for (int i = 0; i < Weights.GetLength(0); i++)
            {
                for (int k = 0; k < Weights.GetLength(1); k++)
                {
                    Weights[i, k] = value;
                }
            }
        }

        public void Process()
        {
            Matrix inputMatrix = new(_inputs);
            Matrix weightsMatrix = new(Weights);
            Matrix inputMatrixTransposed = inputMatrix.Transpose();
            Matrix outputsMatrix = weightsMatrix.Multiply(inputMatrixTransposed);
            float[,] outputsMatrixArray = outputsMatrix.ToArray();
            float[] convertedOutputsMatrixArray = ConvertTwoDimensionalArrayToSingleDimensionalArray(outputsMatrixArray);
            _outputs = ApplyActivationFunction(convertedOutputsMatrixArray);
        }

        public void SetInputs(float[] inputs)
        {
            int inputsLength = inputs.Length;
            inputsLength++;
            if (!IsValidInputs(inputsLength))
            {
                throw new ArgumentException(paramName: nameof(inputs), message: "Incorrect number of input values.");
            }
            Array.Copy(inputs, _inputs, inputs.Length);
            _inputs[^1] = 1f;
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

        public void AdjustWeightsWithError(float error)
        {
            float[] weightSumsByOutputs = GetWeightSumsByOutputs();
            AdjustWeightsProportionally(weightSumsByOutputs, error);
        }

        private void AdjustWeightsProportionally(float[] weightSumsByOutputs, float error)
        {
            for (int i = 0; i < Weights.GetLength(0); i++)
            {
                for (int k = 0; k < Weights.GetLength(1); k++)
                {
                    Weights[i, k] = Weights[i, k] / weightSumsByOutputs[i] * error;
                }
            }
        }

        private float[] GetWeightSumsByOutputs()
        {
            float[] weightSumsByOutputs = new float[Weights.GetLength(0)];
            for (int i = 0; i < Weights.GetLength(0); i++)
            {
                for (int k = 0; k < Weights.GetLength(1); k++)
                {
                    weightSumsByOutputs[i] += Weights[i, k];
                }
            }
            return weightSumsByOutputs;
        }

        private float[] ApplyActivationFunction(float[] weightedSums)
            => weightedSums.Select(x => ActivationFunction.Execute(x)).ToArray();

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

        private bool IsValidInputs(int inputsLength) => inputsLength == _inputs.Length;

        private static bool IsValidInputsNumber(int inputsNumber) => inputsNumber > 0;

        private static bool IsValidOutputsNumber(int outputsNumber) => outputsNumber > 0;

    }
}
