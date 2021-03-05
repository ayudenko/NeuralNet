namespace Models.NeuralNetModels.ActivationFunctions
{
    public interface IActivationFunction
    {

        abstract float Execute(float weightedSum);

        abstract float GetDerivative(float input);

    }
}
