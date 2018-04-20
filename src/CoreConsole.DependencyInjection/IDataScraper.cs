namespace CoreConsole.DependencyInjection
{
    using Newtonsoft.Json.Linq;

    public interface IDataScraper
    {
        JToken GetData(string url);
    }
}
