using Math;
using Models.NeuralNetModels.ActivationFunctions;
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
            RandomizeWeights();
        }

        public void Process()
        {
            Matrix inputMatrix = new(_inputs);
            Matrix weightsMatrix = new(_weights);
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

        private void RandomizeWeights()
        {
            for (int i = 0; i < _weights.GetLength(0); i++)
            {
                for (int k = 0; k < _weights.GetLength(1); k++)
                {
                    var rand = new Random();
                    _weights[i, k] = (float)rand.NextDouble();
                }
            }
        }

        private bool IsValidInputs(float[] inputs) => inputs.Length == _inputs.Length;

        private static bool IsValidInputsNumber(int inputsNumber) => inputsNumber > 0;

        private static bool IsValidOutputsNumber(int outputsNumber) => outputsNumber > 0;

    }
}
