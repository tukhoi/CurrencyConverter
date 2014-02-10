using CC.Utils.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.AppServices.RateFetcher
{
    public abstract class BaseFetcher : IExchangeRateFetcher
    {
        public string Name { get; set; }

        public async Task<AppResult<FetchResult>> Fetch(string from, string to)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                return Result(ErrorCode.CurrenciesToConvertIsNullOrEmpty);

            var url = PrepareUrl(from, to);
            var data = await GetRawResult(url);
            if (!string.IsNullOrEmpty(data.Trim()))
            {
                var value = ParseRate(data);
                return Result(value);
            }

            return Result(ErrorCode.ErrorWhileFetchExchangeRate);
        }

        protected virtual async Task<string> GetRawResult(string requestUri)
        {
            return await WebTasks.GetRawResult(requestUri);
        }

        protected internal abstract FetchResult ParseRate(string data);
        protected internal abstract string PrepareUrl(string from, string to);

        protected internal AppResult<FetchResult> Result(FetchResult result)
        {
            return new AppResult<FetchResult>(result);
        }

        protected internal AppResult<FetchResult> Result(ErrorCode code)
        {
            return new AppResult<FetchResult>(code);
        }
    }
}
