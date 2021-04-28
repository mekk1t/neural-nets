using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public class Cluster : List<Neuron>, IGrouping<Neuron, Neuron>
    {
        public Cluster(Neuron key) : base() => Key = key;

        public Cluster(Neuron key, IEnumerable<Neuron> collection) : base(collection) => Key = key;

        public Neuron Key { get; }
    }
}
