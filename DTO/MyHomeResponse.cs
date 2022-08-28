using Newtonsoft.Json;
using ApartmentCrawler.Entities;
using System.Collections.Generic;

namespace ApartmentCrawler.DTO
{
    public class MyHomeResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public AdvListData Data { get; set; }
        public class AdvListData
        {
            public List<object> Maklers { get; set; }
            [JsonProperty("Prs")]
            public List<Product> Products { get; set; }
            public Dictionary<string, List<object>> Floors { get; set; }

            [JsonProperty("Users")]
            public UsersWrap UsersWrappedData { get; set; }

            public bool ASBannerLocsCheck { get; set; }
            public bool A2RegBannerLocsCheck { get; set; }
            public bool ASBannerShow { get; set; }
            public int TypeformQs { get; set; }
            public class UsersWrap
            {
                public int StatusCode { get; set; }
                public string StatusMessage { get; set; }
                [JsonProperty("Data")]
                public Dictionary<string, User> UsersDictionary { get; set; }                

            }
                   
        }
        
    }
    
}
