using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public class Neuron
    {
        private const int L = 2;

        public List<byte> TWeights => _tWeights.ToList();
        private readonly byte[] _tWeights;
        private readonly int _tWeightsSum;

        public List<decimal> BWeights => _tWeights.Select(CalculateBWeight).ToList();

        public Neuron(byte[] encoding)
        {
            _tWeights = encoding;
            _tWeightsSum = SumOf(_tWeights);
        }

        public void Retrain(List<byte> newTWeights)
        {
            for (int i = 0; i < newTWeights.Count; i++)
            {
                _tWeights[i] = newTWeights[i];
            }
        }

        private decimal CalculateBWeight(byte tWeight) => Math.Round((decimal)L * (decimal)tWeight / (decimal)(L - 1 + _tWeightsSum), 2);

        private static int SumOf(byte[] array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
                sum += array[i];

            return sum;
        }
    }
}
