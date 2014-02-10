using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.AppServices.RateFetcher
{
    public class FetchResult
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public double Rate { get; set; }
        public double Ask { get; set; }
        public double Bid { get; set; }
    }
}
