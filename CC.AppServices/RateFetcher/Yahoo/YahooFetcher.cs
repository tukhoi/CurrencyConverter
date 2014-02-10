using CC.AppServices.RateFetcher;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.AppServices.RateFetcher.Yahoo
{
    public class YahooFetcher : BaseFetcher
    {
        private string yql = "select * from yahoo.finance.xchange where pair in ({0})";
        private string apiUrl = "http://query.yahooapis.com/v1/public/yql?q={0}&format=json&env=store://datatables.org/alltableswithkeys";

        protected internal override string PrepareUrl(string from, string to)
        {
            var currenciesPair = string.Format(@"""{0}{1}""", from, to);
            var yql = string.Format(this.yql, currenciesPair);
            var url = string.Format(this.apiUrl, yql);

            return url;
        }

        protected internal override FetchResult ParseRate(string data)
        {
            var result = JsonConvert.DeserializeObject<YahooResult>(data, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            if (result == null ||
                result.query == null ||
                result.query.results == null ||
                result.query.results.rate == null)
                return null;

            var fetchResult = new FetchResult();
            fetchResult.id = result.query.results.rate.id;
            fetchResult.Name = result.query.results.rate.Name;
            fetchResult.Date = result.query.results.rate.Date;
            fetchResult.Time = result.query.results.rate.Time;

            double rate, ask, bid;

            if (double.TryParse(result.query.results.rate.Rate, out rate))
                fetchResult.Rate = rate;
            if (double.TryParse(result.query.results.rate.Ask, out ask))
                fetchResult.Ask = ask;
            if (double.TryParse(result.query.results.rate.Bid, out bid))
                fetchResult.Bid = bid;

            return fetchResult;
        }
    }
}
