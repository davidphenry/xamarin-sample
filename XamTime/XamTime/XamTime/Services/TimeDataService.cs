using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamTime.Services
{
    public static class TimeDataService
    {
        static CookieContainer cookieJar;
        static HttpClient client;
        private static void InitClient()
        {
            var cookieManager = DependencyService.Get<ICookieManager>();

            var handler = new HttpClientHandler();
            cookieJar = cookieManager.InitCookieContainer();

            handler.CookieContainer = cookieJar;
            handler.UseCookies = true;
            client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public static async Task<string> GetTimeData(string endPoint)
        {
            InitClient();
            string baseUri = "http://portal.dragonspears.com/WebServices/TimeEntry.asmx/";

            var uri = $"{baseUri}{endPoint}";
            //var uri = http://portal.dragonspears.com/WebServices/TimeEntry.asmx/GetUserTimekeeperModel";
            //var uri = "http://portal.dragonspears.com/WebServices/TimeEntry.asmx/GetTimeEntryForCurrentUser?startDate=03/01/2017&endDate=03/07/2017";
            //var uri = "http://portal.dragonspears.com/WebServices/TimeEntry.asmx/GetAccounts";//?showAllAccounts=true";

            //var uri = "http://portal.dragonspears.com/time.aspx";

            var response = await client.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception("Login Failed");

            response.EnsureSuccessStatusCode();

            string json = await ReadJsonFromContentAsync(response.Content);
            return json;
        }
        private static async Task<string> ReadJsonFromContentAsync(HttpContent content)
        {
            var stream = await content.ReadAsStreamAsync();
            string json = null;
            using (var reader = new StreamReader(stream))
                json = await reader.ReadToEndAsync();

            return json;
        }
        private static async Task<T> ReadJson<T>(string json) where T : class
        {
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
    }
}
