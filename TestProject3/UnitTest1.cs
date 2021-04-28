using Tools;
using Xunit;

namespace TestProject3
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var neuralNet = new NeuralNet(0.85M, 10);

            neuralNet.Process(new byte[10] { 1, 1, 0, 1, 1, 1, 0, 1, 0, 1 });
            neuralNet.Process(new byte[10] { 0, 0, 1, 0, 0, 1, 1, 1, 1, 1 });
            neuralNet.Process(new byte[10] { 1, 1, 1, 1, 1, 1, 0, 1, 0, 1 });
            neuralNet.Process(new byte[10] { 0, 0, 1, 0, 0, 0, 1, 1, 1, 1 });
            neuralNet.Process(new byte[10] { 1, 0, 1, 0, 0, 1, 1, 1, 1, 1 });
        }
    }
}
