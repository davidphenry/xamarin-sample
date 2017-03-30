using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamTime.Models;
using XamTime.Services;
using XamTime.ViewModels;

namespace XamTime
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            vm = new MainPageViewModel();
            BindingContext = vm;
        }
        MainPageViewModel vm;

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await vm.GetTimeData();
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            var cookieManager = DependencyService.Get<ICookieManager>();
            cookieManager.Clear();
            App.Current.MainPage = new LoginPage();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }

    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            Endpoint = "GetUserTimekeeperModel";
        }

        string _endpoint;
        public string Endpoint            
        {
            get { return _endpoint; }
            set { SetPropertyByRef(ref _endpoint, value); }
        }

        string _status;
        public string Status
        {
            get { return _status; }
            set { SetPropertyByRef(ref _status, value); }
        }

        ObservableCollection<ClientAccount> _accountList;
        public ObservableCollection<ClientAccount> AccountList
        {
            get { return _accountList; }
            set { SetPropertyByRef(ref _accountList, value); }
        }

        internal async Task GetTimeData()
        {
            try
            {
                //               _status = await TimeDataService.GetTimeData(Endpoint);

                AccountList = new ObservableCollection<ClientAccount>(await TimeDataService.GetAccountData());
            }
            catch (Exception ex)
            {
               _status= ex.ToString();
            }

            OnPropertyChanged(nameof(Status));            
        }
    }
}
