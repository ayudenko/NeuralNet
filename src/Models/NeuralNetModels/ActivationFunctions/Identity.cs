namespace Models.NeuralNetModels.ActivationFunctions
{
    public class Identity : IActivationFunction
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
