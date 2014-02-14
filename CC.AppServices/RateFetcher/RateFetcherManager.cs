using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.AppServices.RateFetcher
{
    public class RateFetcherManager
    {
        private static IDictionary<Provider, IExchangeRateFetcher> _store;

        static RateFetcherManager()
        {
            _store = new Dictionary<Provider, IExchangeRateFetcher>();
        }

        public static IExchangeRateFetcher GetFetcher(Provider provider)
        {
            if (!_store.ContainsKey(provider))
                _store.Add(provider, CreateFetcher(provider));

            return _store[provider];
        }

        static IExchangeRateFetcher CreateFetcher(Provider provider)
        {
            IExchangeRateFetcher fetcher = null;

            switch (provider)
            { 
                case Provider.YahooFinance:
                    fetcher = new Yahoo.YahooFetcher();
                    break;
                case Provider.OpenExchangeRates:
                    fetcher = new OpenExchangeRates.OXRFetcher();
                    break;
                default:
                    fetcher = new Yahoo.YahooFetcher();
                    break;
            }

            return fetcher;
        }
    }

    public enum Provider
    { 
        YahooFinance,
        OpenExchangeRates
    }
}
