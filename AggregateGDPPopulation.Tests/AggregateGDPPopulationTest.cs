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
            Program p1 = new Program();
            await p1.solution();
            var actual = await p1.Reader(@"D:\workspace\C# Assignments\aggregate-gdp-population-csharp-problem-Hrishi246\AggregateGDPPopulation\data\output.json");
            var expected = await p1.Reader(@"D:\workspace\C# Assignments\aggregate-gdp-population-csharp-problem-Hrishi246\AggregateGDPPopulation.Tests\expected-output.json");
            JObject actualJson = JObject.Parse(actual);
            JObject expectedJson = JObject.Parse(expected);
            Assert.Equal(actualJson, expectedJson);
        }

    }
}