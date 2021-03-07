namespace Models.NeuralNetModels.ActivationFunctions
{
    public class BinaryStep : IActivationFunction
    {

        private readonly float _threshold;

        public BinaryStep(float threshold)
        {
            _threshold = threshold;
        }

        public float Execute(float weightedSum)
        {
            if (weightedSum >= _threshold)
            {
                return 1;
            }
            return 0;
        }

        public float GetDerivative(float input)
        {
            return 0;
        }
    }
}
