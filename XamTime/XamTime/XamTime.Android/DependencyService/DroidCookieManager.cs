using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamTime.Services;
using Xamarin.Forms;
using XamTime.Droid.DependencyService;
using System.Net;
using Android.Webkit;

[assembly: Dependency(typeof(DroidCookieManager))]
namespace XamTime.Droid.DependencyService
{
    public class DroidCookieManager : ICookieManager
    {
        public void Clear()
        {
            var cookieManager = CookieManager.Instance;
            cookieManager.RemoveAllCookies(null);
        }

        public CookieContainer InitCookieContainer()
        {
            var cookieContainer = new CookieContainer();
            var cookieManager = CookieManager.Instance;

            string cookieUrl = "http://portal.dragonspears.com";
            var cookie = cookieManager.GetCookie(cookieUrl);

            if (!string.IsNullOrEmpty(cookie))
                cookieContainer.SetCookies(new Uri(cookieUrl), cookie);

            return cookieContainer;            
        }
    }
}