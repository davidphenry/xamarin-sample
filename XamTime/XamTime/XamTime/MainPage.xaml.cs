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

namespace XamTime
{
    public partial class MainPage : ContentPage
    {
        HttpClient client;
        public MainPage()
        {
            InitializeComponent();
        }
        CookieContainer cookieJar;
        private void InitClient()
        {

            var handler = new HttpClientHandler();
            cookieJar = new CookieContainer();
            handler.CookieContainer = cookieJar;
            handler.UseCookies = true;
            client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            try
            {
                lblStatus.Text = await GetTimeData(txtUser.Text, txtPass.Text);
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.ToString();
            }
        }

        private async Task<string> GetTimeData(string userName, string password)
        {
            InitClient();
            var formContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("username",userName),
                new KeyValuePair<string, string>("password", password)
            });

            var response = await client.PostAsync("http://portal.dragonspears.com", formContent);
            Stream stringContent = await response.Content.ReadAsStreamAsync();

            var responseCookies = cookieJar.GetCookies(new Uri("http://portal.dragonspears.com"));
            var responseCookie = responseCookies["ASP.NET_SessionId"];
            //client.DefaultRequestHeaders.Add(responseCookie.Name, responseCookie.Value);

            var uri = "http://portal.dragonspears.com/WebServices/TimeEntry.asmx/GetUserTimekeeperModel";
            //var uri = "http://portal.dragonspears.com/WebServices/TimeEntry.asmx/GetTimeEntryForCurrentUser?startDate=03/01/2017&endDate=03/07/2017";
            //var uri = "http://portal.dragonspears.com/WebServices/TimeEntry.asmx/GetAccounts";//?showAllAccounts=true";

           //var uri = "http://portal.dragonspears.com/time.aspx";
            response = await client.PostAsync(uri, null);

            if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception("Login Failed");

            response.EnsureSuccessStatusCode();

            string json = await ReadJsonFromContentAsync(response.Content);
            return json;
        }
        protected async Task<string> ReadJsonFromContentAsync(HttpContent content)
        {
            var stream = await content.ReadAsStreamAsync();
            string json = null;
            using (var reader = new StreamReader(stream))
                json = await reader.ReadToEndAsync();

            return json;
        }
        protected async Task<T> ReadJson<T>(string json) where T : class
        {
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

    }
}
