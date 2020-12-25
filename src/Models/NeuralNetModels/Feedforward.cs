using System;

namespace Models.NeuralNetModels
{
    public class Feedforward
    {

        private float[] _inputs;
        private float[] _outputs;

        public Feedforward(int inputsNumber, int outputsNumber)
        {
            if (!IsValidInputsNumber(inputsNumber))
            {
                throw new ArgumentOutOfRangeException("Incorrent number of input items.");
            }
            if (!IsValidOutputsNumber(outputsNumber))
            {
                throw new ArgumentOutOfRangeException("Incorrent number of output items.");
            }
            _inputs = new float[inputsNumber];
            _outputs = new float[outputsNumber];
        }

        public void SetInputs(float[] inputs)
        {
            if (!IsValidInputs(inputs))
            {
                throw new ArgumentException("Wrong number of input values.");
            }
            _inputs = inputs;
        }

        public float[] GetOutputs()
        {
            return _outputs;
        }

        private bool IsValidInputs(float[] inputs)
        {
            return inputs.Length == _inputs.Length;
        }

        private static bool IsValidInputsNumber(int inputsNumber)
        {
            return inputsNumber > 0;
        }

        private static bool IsValidOutputsNumber(int outputsNumber)
        {
            return outputsNumber > 0;
        }

    }
}
