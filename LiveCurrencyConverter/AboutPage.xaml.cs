﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

namespace LiveCurrencyConverter
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void btnRating_Click(object sender, RoutedEventArgs e)
        {
            MarketplaceReviewTask oRateTask = new MarketplaceReviewTask();
            oRateTask.Show();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            GoogleAnalytics.EasyTracker.GetTracker().SendView(this.ToString());
        }
    }
}