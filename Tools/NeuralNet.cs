using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public class NeuralNet
    {
        private readonly decimal _thresholdLevel;
        private readonly int _neuronCapacity;

        public List<Cluster> Clusters { get; private set; } = new();

        private readonly List<Neuron> _neurons;
        public IReadOnlyList<Neuron> Neurons => _neurons;

        public NeuralNet(decimal thresholdLevel, int neuronCapacity)
        {
            _neuronCapacity = neuronCapacity;
            _thresholdLevel = thresholdLevel;
            _neurons = new();
            _neurons.Add(UnallocatedNeuron());
        }

        public void Process(byte[] testSampleEncoding)
        {
            if (_neurons.Count == 1)
            {
                Remember(new Neuron(testSampleEncoding));
                return;
            }

            Neuron winner = _neurons.First();
            decimal maxSum = SummarizeNeuron(winner, testSampleEncoding);
            for (int i = 0; i < _neurons.Count; i++)
            {
                var neuronSum = SummarizeNeuron(_neurons[i], testSampleEncoding);
                if (neuronSum > maxSum)
                {
                    maxSum = neuronSum;
                    winner = _neurons[i];
                }
            }

            if (_neurons.IndexOf(winner) == 0)
            {
                Remember(new Neuron(testSampleEncoding));
                return;
            }

            var cWeights = new List<byte>(testSampleEncoding.ToList());
            for (int i = 0; i < cWeights.Count; i++)
            {
                cWeights[i] *= winner.TWeights[i];
            }

            if (MatchLevel(testSampleEncoding, cWeights) > _thresholdLevel)
            {
                Clusters.Find(cluster => cluster.Key.Id == winner.Id).Add(new Neuron(testSampleEncoding));
                winner.Retrain(cWeights);
            }
            else
            {
                Remember(new Neuron(testSampleEncoding));
            }
        }

        private static decimal MatchLevel(byte[] encoding, List<byte> cWeights)
        {
            var total = encoding.Length;
            int matchesCount = 0;

            for (int i = 0; i < encoding.Length; i++)
            {
                if (encoding[i] == cWeights[i])
                    matchesCount++;
            }

            return Math.Round((decimal)matchesCount / (decimal)total, 2);
        }

        private static decimal SummarizeNeuron(Neuron neuron, byte[] encoding)
        {
            decimal sum = 0.00M;
            for (int i = 0; i < neuron.TWeights.Count; i++)
            {
                sum += encoding[i] * neuron.BWeights[i];
            }

            return sum;
        }


        private void Remember(Neuron neuron)
        {
            _neurons.Add(neuron);
            Clusters.Add(new Cluster(neuron, new[] { neuron }));
        }

        private Neuron UnallocatedNeuron()
        {
            var bytes = new byte[_neuronCapacity];
            Array.Fill<byte>(bytes, 1);
            return new Neuron(bytes);
        }
    }
}
