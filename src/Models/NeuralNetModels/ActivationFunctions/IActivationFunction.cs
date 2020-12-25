namespace Models.NeuralNetModels.ActivationFunctions
{
    public interface IActivationFunction
    {

        abstract float Execute(float weightedSum);

    }
}
