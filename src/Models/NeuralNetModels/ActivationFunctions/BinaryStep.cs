namespace Models.NeuralNetModels.ActivationFunctions
{
    public class BinaryStep : IActivationFunction
    {

        public float Execute(float weightedSum)
        {
            if (weightedSum >= 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
