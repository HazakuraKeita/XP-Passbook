using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace XPPassbook.Models
{
    public class XPer
    {
        public int InvestmentAmount { get; set; }
        public double CurrentAmount { get { return Balance * XP.Jpy; } }
        public double Balance { get; set; }
        public double Plofit { get { return CurrentAmount - InvestmentAmount; } }
        public double PlofitPercentage { get { return (Plofit / InvestmentAmount) * 100; } }
        public string MaskedAddress
        {
            get
            {
                if (string.IsNullOrEmpty(Address) || Address.Length < 8)
                {
                    return string.Empty;
                }

                var maskedAddress = Address.Substring(0, 7);
                for (var i = 0; i < Address.Length; i++)
                {
                    maskedAddress += "*";
                }

                return maskedAddress;
            }
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public ExperiencePoints XP { get; private set; }
        public BitmapImage IconImage { get; set; }
        public string ImageFullName
        {
            get { return imageFullName; }
            set
            {
                imageFullName = value;

                if (!string.IsNullOrEmpty(imageFullName))
                {
                    var image = new BitmapImage();
                    image.CreateOptions = BitmapCreateOptions.None;
                    image.BeginInit();
                    image.UriSource = new Uri(imageFullName, UriKind.Absolute);
                    image.EndInit();
                    IconImage = image;
                }
            }
        }

        string imageFullName;
        SettingsXml xml;

        public XPer()
        {
            XP = new ExperiencePoints();
            xml = new SettingsXml();

            if (xml.IsSuccess)
            {
                InvestmentAmount = xml.InvestmentAmount;
                Name = xml.Name;
                Address = xml.Address;
                ImageFullName = xml.ImageFullName;
            }
        }

        public async Task Load()
        {
            await LoadBalance();
            await XP.Load();
        }

        public void Save()
        {
            xml.InvestmentAmount = InvestmentAmount;
            xml.Name = Name;
            xml.Address = Address;
            xml.ImageFullName = ImageFullName;
            xml.Update();
        }

        private async Task LoadBalance()
        {
            try
            {
                using (var response = await (new HttpClient()).GetAsync("https://chainz.cryptoid.info/xp/api.dws?q=getbalance&a=" + Address))
                {
                    var data = await response.Content.ReadAsStringAsync();

                    Balance = double.Parse(data);
                }
            }
            catch { }
        }
    }
}
