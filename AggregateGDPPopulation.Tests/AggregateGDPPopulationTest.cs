using Xunit;
using System.IO;
using Newtonsoft.Json.Linq;
using AggregateGDPPopulation;

namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Program p = new Program();
            p.solution();
            var actual = File.ReadAllText(@"../../../../AggregateGDPPopulation/data/output.json");
            var expected = File.ReadAllText(@"../../../expected-output.json");
            JObject actualJson = JObject.Parse(actual);
            JObject expectedJson = JObject.Parse(expected);
            Assert.Equal(actualJson, expectedJson);

        }
    }
}
