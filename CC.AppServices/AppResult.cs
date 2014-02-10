using CC.Utils.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.AppServices
{
    public class AppResult<T> : BaseResult<T, ErrorCode>
    {
        public AppResult(T target, ErrorCode error)
            : base(target, error)
        {
        }

        public AppResult(T target)
            : base(target, ErrorCode.None)
        {
        }

        public AppResult(ErrorCode error)
            : base(error)
        {
        }

        public bool HasError
        {
            get { return Error != ErrorCode.None; }
        }
    }
}
