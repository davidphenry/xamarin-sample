using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamTime
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            webviewLogin.Source = new UrlWebViewSource() { Url = "http://portal.dragonspears.com" };
            webviewLogin.Navigating += WebviewLogin_Navigating;
        }

        private void WebviewLogin_Navigating(object sender, WebNavigatingEventArgs e)
        {
            //login success
            if (e.Url.Contains("time.aspx"))
            {
                App.Current.MainPage = new NavigationPage(new XamTime.MainPage());
            }
        }
    }
    
}
