using Xunit;
using System.IO;
using Newtonsoft.Json.Linq;

namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            GDPaggregate G1 = new GDPaggregate();
            await GDPaggregate.solution();
            var actual = await fileProcessing.Reader(@"../../../../AggregateGDPPopulation/data/output.json");
            var expected = await fileProcessing.Reader(@"../../../expected-output.json");
            JObject actualJson = JObject.Parse(actual);
            JObject expectedJson = JObject.Parse(expected);
            Assert.Equal(actualJson, expectedJson);
        }

    }
}