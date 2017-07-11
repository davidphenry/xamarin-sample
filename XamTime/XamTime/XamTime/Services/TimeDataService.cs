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
using System.Collections.ObjectModel;
using XamTime.Models;

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

            string json = await response.Content.ReadAsStringAsync();
            return json;
        }

        internal static async Task<IEnumerable<ClientAccount>> GetAccountData()
        {
            //var list = new List<ClientAccount>();
            //for (int i = 1; i <= 10; i++)
            //    list.Add(new ClientAccount() { AccountId = i.ToString(), AccountName = $"Account{i}" });
            //return list;

            InitClient();
            string baseUri = "http://portal.dragonspears.com/WebServices/TimeEntry.asmx/";

            var uri = $"{baseUri}GetAccounts?showAllAccounts=true";

            var response = await client.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception("Login Failed");

            response.EnsureSuccessStatusCode();

            var list = await ReadXml<ClientAccounts>(response);
            return list.Accounts;
        }

        //private static async Task<string> ReadJsonFromContentAsync(HttpContent content)
        //{
        //    var stream = await content.ReadAsStreamAsync();
        //    string json = null;
        //    using (var reader = new StreamReader(stream))
        //        json = await reader.ReadToEndAsync();

        //    return json;
        //}
        private static async Task<T> ReadJson<T>(HttpResponseMessage response) where T : class
        {
            string json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
        private static async Task<T> ReadXml<T>(HttpResponseMessage response) where T : class
        {
            string xml = await response.Content.ReadAsStringAsync();
            var result = xml.Deserialize<T>();
            return result;
        }
    }
}
