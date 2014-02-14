using CC.Utils.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.AppServices.RateFetcher.OpenExchangeRates
{
    public class OXRFetcher : BaseFetcher
    {
        private const string AppId = "a3f93a104c5a408381d2d48498ddf0e9";
        private const string RequestUrl = "https://openexchangerates.org/api/latest.json?app_id=a3f93a104c5a408381d2d48498ddf0e9";

        private string _from;
        private string _to;

        protected internal override string PrepareUrl(string from, string to)
        {
            _from = from;
            _to = to;
            return RequestUrl;
        }

        protected internal override FetchResult ParseRate(string data)
        {
            var result = JsonConvert.DeserializeObject<OXRResult>(data, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            if (result == null || result.rates == null) return null;

            var rateList = result.rates;
            var fromProperty = typeof(Rate).GetProperty(_from);
            var toProperty = typeof(Rate).GetProperty(_to);

            if (fromProperty == null || toProperty == null) return null;

            var fromRate = (double)fromProperty.GetValue(rateList);
            var toRate = (double)toProperty.GetValue(rateList);

            var rate = toRate / fromRate;

            return new FetchResult() { 
                Time = ConvertHelper.UnixTimeStampToDateTime(result.timestamp).ToLongTimeString(),
                Date = ConvertHelper.UnixTimeStampToDateTime(result.timestamp).ToShortDateString(),
                Rate = rate,
                Bid = -1,
                Ask = -1
            };
        }
    }
}
