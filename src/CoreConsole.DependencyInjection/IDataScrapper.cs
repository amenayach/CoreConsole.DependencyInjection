namespace CoreConsole.DependencyInjection
{
    using Newtonsoft.Json.Linq;

    public interface IDataScrapper
    {
        JToken GetData(string url);
    }
}
