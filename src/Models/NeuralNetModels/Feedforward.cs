using System;

namespace Models.NeuralNetModels
{
    public class Feedforward
    {

        private float[] _inputs;
        private float[] _outputs;

        public Feedforward(int inputsNumber, int outputsNumber)
        {
            if (!IsValidInputsNumber(inputsNumber) || !IsValidOutputsNumber(outputsNumber))
            {
                throw new ArgumentOutOfRangeException();
            }
            _inputs = new float[inputsNumber];
            _outputs = new float[outputsNumber];
        }

        public void SetInputs(float[] inputs)
        {
            _inputs = inputs;
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
