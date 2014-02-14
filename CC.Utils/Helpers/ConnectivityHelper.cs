using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CC.Utils.Helpers
{
    public static class ConnectivityHelper
    {
        public static bool NetworkAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
