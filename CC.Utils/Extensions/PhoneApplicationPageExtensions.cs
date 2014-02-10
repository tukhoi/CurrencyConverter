using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Utils.Extensions
{
    public static class PhoneApplicationPageExtensions
    {
        public static void SetProgressIndicator(this PhoneApplicationPage page, bool isVisible, string message = "")
        {
            if (SystemTray.ProgressIndicator == null)
                SystemTray.ProgressIndicator = new ProgressIndicator();

            SystemTray.ProgressIndicator.IsIndeterminate = isVisible;
            SystemTray.ProgressIndicator.IsVisible = isVisible;
            SystemTray.ProgressIndicator.Text = message;
        }

        public static void BackToPreviousPage(this PhoneApplicationPage page)
        {
            page.NavigationService.GoBack();
        }
    }
}
