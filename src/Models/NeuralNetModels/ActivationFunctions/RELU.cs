namespace Models.NeuralNetModels.ActivationFunctions
{
    public class RELU : IActivationFunction
    {
        public float Execute(float weightedSum)
        {
            return System.Math.Max(0, weightedSum);
        }
    }
}
