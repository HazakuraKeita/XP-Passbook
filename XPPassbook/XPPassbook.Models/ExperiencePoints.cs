using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace XPPassbook.Models
{
    public class ExperiencePoints
    {
        public double Usd { get; private set; }
        public double Btc { get; private set; }
        public double Jpy { get; private set; }

        public ExperiencePoints() { }

        public async Task Load()
        {
            try
            {
                using (var client = await (new HttpClient()).GetAsync("https://api.coinmarketcap.com/v1/ticker/xp/?convert=JPY"))
                {
                    // coinmarketcap json format is wrong. Root array has no name. too bad!
                    var jsonString = (await client.Content.ReadAsStringAsync()).Replace("[\n", string.Empty).Replace("\n]", string.Empty);
                    using (var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(Response));
                        var response = (Response)serializer.ReadObject(jsonStream);

                        Usd = response.Usd;
                        Btc = response.Btc;
                        Jpy = response.Jpy;
                    }
                }
            }
            catch { }
        }

        [DataContract]
        class Response
        {
            [DataMember(Name = "price_usd")]
            public double Usd { get; set; }
            [DataMember(Name = "price_btc")]
            public double Btc { get; set; }
            [DataMember(Name = "price_jpy")]
            public double Jpy { get; set; }
        }
    }
}
