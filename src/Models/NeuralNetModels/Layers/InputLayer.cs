using Models.NeuralNetModels.Exceptions;

namespace Models.NeuralNetModels.Layers
{
    public class InputLayer : ILayer
    {

        private int[] _items;
        private bool _hasBias = false;

        public InputLayer(int itemsNumber)
        {
            if (itemsNumber < 1)
            {
                throw new InputLayerException();
            }
            _items = new int[itemsNumber];
        }

        public void SetInput(int[] inputs)
        {
            if (!IsValidInput(inputs))
            {
                throw new InputLayerException();
            }
            _items = inputs;
        }

        public int[] GetInput()
        {
            return _items;
        }

        public void AddBias()
        {
            _hasBias = true;
        }

        public void RemoveBias()
        {
            _hasBias = false;
        }

        public bool HasBias()
        {
            return _hasBias;
        }

        private bool IsValidInput(int[] inputs)
        {
            if (inputs.Length != _items.Length)
            {
                return false;
            }
            foreach (int input in inputs)
            {
                if ((input < 0) || (input > 1))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
