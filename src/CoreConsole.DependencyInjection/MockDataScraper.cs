namespace CoreConsole.DependencyInjection
{
    using Newtonsoft.Json.Linq;
    using System.Linq;

    public class MockDataScraper : IDataScraper
    {
        public JToken GetData(string url)
        {
            return JToken.FromObject(Enumerable.Range(1, 10).Select(m => new
            {
                Id = m,
                Name = $"Test name {m}"
            }));
        }
    }
}
