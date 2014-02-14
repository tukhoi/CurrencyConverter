using CC.AppServices;
using CC.AppServices.RateFetcher;
using LiveCurrencyConverter.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCurrencyConverter.Helper
{
    public static class FetchResultExtensions
    {
        public static string ErrorMessage<T>(this AppResult<T> result)
        {
            var message = string.Empty;
            switch (result.Error)
            {
                case ErrorCode.None:
                    message = string.Empty;
                    break;
                default:
                    message = AppResources.ErrGenericError;
                    break;
            }

            return message;
        }
    }
}
