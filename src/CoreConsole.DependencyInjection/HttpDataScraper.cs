namespace CoreConsole.DependencyInjection
{
    using Newtonsoft.Json.Linq;
    using System.Net.Http;

    public class HttpDataScraper : IDataScraper
    {
        public JToken GetData(string url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36");

                var response = httpClient.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    return JToken.FromObject(response.Content.ReadAsStringAsync().Result);
                }
            }

            return null;
        }
    }
}
