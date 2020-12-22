using Models.NeuralNetModels.Exceptions;

namespace Models.NeuralNetModels.Layers
{
    public class OutputLayer : ILayer
    {

        private float[] _items;

        public OutputLayer(int numberOfItems)
        {
            _items = new float[numberOfItems];
        }

        public void SetOutput(float[] outputs)
        {
            if (!IsValidOutput(outputs))
            {
                throw new OutputLayerException();
            }
            _items = outputs;
        }

        public float[] GetOutput()
        {
            return _items;
        }

        private bool IsValidOutput(float[] outputs)
        {
            return outputs.Length == _items.Length;
        }

    }
}
