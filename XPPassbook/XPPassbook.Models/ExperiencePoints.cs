using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XPPassbook.Models
{
    public class ExperiencePoints
    {
        public ExperiencePoints() { }

        public async Task Load()
        {
            try
            {
                using (var response = await (new HttpClient()).GetAsync("https://api.coinmarketcap.com/v1/ticker/xp/?convert=JPY"))
                {
                    var json = await response.Content.ReadAsStringAsync();
                }
            }
            catch { }
        }
    }
}
