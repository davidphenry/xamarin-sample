using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using XamTime.Services;

namespace XamTime
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }    
        

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                string endpoint = txtEndpoint.Text;
                if (string.IsNullOrEmpty(endpoint))
                    endpoint = txtEndpoint.Placeholder;

                lblStatus.Text = await TimeDataService.GetTimeData(endpoint);
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.ToString();
            }
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            var cookieManager = DependencyService.Get<ICookieManager>();
            cookieManager.Clear();
            App.Current.MainPage = new LoginPage();
        }
    }
}
