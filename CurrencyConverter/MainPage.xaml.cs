using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CurrencyConverter.Resources;
using CC.AppServices;
using CC.AppServices.RateFetcher.Yahoo;
using CC.Utils.Extensions;
using CurrencyConverter.ViewModel;
using CC.AppServices.RateFetcher;
using CurrencyConverter.Helper;

namespace CurrencyConverter
{
    public partial class MainPage : PhoneApplicationPage
    {
        IExchangeRateFetcher _fetcher = new YahooFetcher();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Binding();
            SetResultVisible(System.Windows.Visibility.Collapsed);
            base.OnNavigatedTo(e);
        }

        private void Binding()
        {
            var codes = CurrencyCode.GetInstance();
            var currencies = new List<Currency>();

            codes.Keys.ForEach(k => {
                currencies.Add(new Currency { 
                    Code = k,
                    Name = codes[k]
                });
            });

            txtFromCurrency.DataContext = currencies;
            txtToCurrency.DataContext = currencies;
        }

        private async void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate()) return;

            this.SetProgressIndicator(true, AppResources.FetchingTitle);
            var result = await _fetcher.Fetch(txtFromCurrency.Text, txtToCurrency.Text);
            this.SetProgressIndicator(false);
            if (result.HasError)
                ToastMessage.Show(result.ErrorMessage());
            else
            {
                var rateResultMessage = AppResources.RateResult;
                rateResultMessage = string.Format(rateResultMessage, txtFromCurrency.Text, result.Target.Rate.ToString(), txtToCurrency.Text);
                txtRateResult.Text = rateResultMessage;

                var askResultMessage = AppResources.AskResult;
                askResultMessage = string.Format(askResultMessage, txtFromCurrency.Text, result.Target.Ask.ToString(), txtToCurrency.Text);
                txtAskResult.Text = askResultMessage;

                var bidResultMessage = AppResources.BidResult;
                bidResultMessage = string.Format(bidResultMessage, txtFromCurrency.Text, result.Target.Bid.ToString(), txtToCurrency.Text);
                txtBidResult.Text = bidResultMessage;

                var time = AppResources.UpdateTitle;
                time = string.Format(time, result.Target.Date + " " + result.Target.Time);
                txtTime.Text = time;

                SetResultVisible(System.Windows.Visibility.Visible);
            }
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(txtFromCurrency.Text) || string.IsNullOrEmpty(txtToCurrency.Text))
            {
                ToastMessage.Show(AppResources.ErrEmptyCurrency);
                return false;
            }

            var currencyCode = CurrencyCode.GetInstance();
            if (!currencyCode.ContainsKey(txtFromCurrency.Text))
            {
                ToastMessage.Show(AppResources.ErrFromCurrencyNotFound);
                return false;
            }
            if (!currencyCode.ContainsKey(txtToCurrency.Text))
            {
                ToastMessage.Show(AppResources.ErrToCurrencyNotFound);
                return false;
            }

            return true;
        }

        private void SetResultVisible(Visibility visibility)
        {
            lblRate.Visibility = visibility;
            lblAsk.Visibility = visibility;
            lblBid.Visibility = visibility;
            txtRateResult.Visibility = visibility;
            txtAskResult.Visibility = visibility;
            txtBidResult.Visibility = visibility;
            txtTime.Visibility = visibility;
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}