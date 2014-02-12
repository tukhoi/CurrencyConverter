﻿using System;
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
using Microsoft.Phone.Tasks;
using System.Globalization;

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
            BuildLocalizedApplicationBar();
            //SetResultVisible(System.Windows.Visibility.Collapsed);

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
                var fetchResult = result.Target;
                double amount = 0;

                if (string.IsNullOrEmpty(txtAmount.Text))
                    amount = 1;
                else
                    amount = double.Parse(txtAmount.Text);

                string resultString = "{0} {1} = {2} {3}";
                
                string rateResult = string.Format(resultString, amount.ToString("n"), txtFromCurrency.Text, (amount * fetchResult.Rate).ToString("n"), txtToCurrency.Text);
                string bidResult = string.Format(resultString, amount.ToString("n"), txtFromCurrency.Text, (amount * fetchResult.Bid).ToString("n"), txtToCurrency.Text);
                string askResult = string.Format(resultString, amount.ToString("n"), txtFromCurrency.Text, (amount * fetchResult.Ask).ToString("n"), txtToCurrency.Text);

                txtRateResult.Text = rateResult;
                txtBidResult.Text = AppResources.BidTitle + " " + bidResult;
                txtAskResult.Text = AppResources.AskTitle + " " + askResult;

                var time = AppResources.UpdateTitle;
                time = string.Format(time, result.Target.Date + " " + result.Target.Time);
                txtTime.Text = time;

                SetResultVisible(System.Windows.Visibility.Visible);
            }
        }

        private bool Validate()
        {
            txtFromCurrency.Text = txtFromCurrency.Text.ToUpper();
            txtToCurrency.Text = txtToCurrency.Text.ToUpper();

            double result = 0;
            if (!string.IsNullOrEmpty(txtAmount.Text) && !double.TryParse(txtAmount.Text, out result))
            {
                ToastMessage.Show(AppResources.ErrAmount);
                txtAmount.Text = "";
                return false;
            }
            else
                txtAmount.Text = result.ToString("n");
                

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
            txtRateResult.Visibility = visibility;
            txtAskResult.Visibility = visibility;
            txtBidResult.Visibility = visibility;
            txtTime.Visibility = visibility;
        }

        private void BuildLocalizedApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Default;

            var aboutButton = new ApplicationBarIconButton();
            aboutButton.Text = AppResources.AppBarAboutTitle;
            aboutButton.IconUri = new Uri("/Assets/AppBar/like.png", UriKind.Relative);
            aboutButton.Click += new EventHandler((sender, e) => {
                NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
            });

            ApplicationBar.Buttons.Add(aboutButton);
        }
    }
}