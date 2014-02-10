using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.AppServices.RateFetcher
{
    public interface IExchangeRateFetcher
    {
        Task<AppResult<FetchResult>> Fetch(string from, string to);
    }
}
