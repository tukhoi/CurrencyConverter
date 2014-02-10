using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.AppServices
{
    public enum ErrorCode
    {
        None,
        CurrenciesToConvertIsNullOrEmpty,
        ErrorWhileFetchExchangeRate
    }
}
