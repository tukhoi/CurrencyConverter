using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CC.AppServices.RateFetcher.Yahoo
{
    [DataContract]
    internal class YahooResult
    {
        [DataMember]
        public Query query { get; set; }
    }

    [DataContract]
    internal class Query
    {
        [DataMember]
        public int count { get; set; }
        [DataMember]
        public string created { get; set; }
        [DataMember]
        public string lang { get; set; }
        [DataMember]
        public Result results { get; set; }
    }

    [DataContract]
    internal class Result
    {
        [DataMember]
        public RateElement rate { get; set; }
    }

    [DataContract]
    internal class RateElement
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Rate { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string Time { get; set; }
        [DataMember]
        public string Ask { get; set; }
        [DataMember]
        public string Bid { get; set; }
    }
}
