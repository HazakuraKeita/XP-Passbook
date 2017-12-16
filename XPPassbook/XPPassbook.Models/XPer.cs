using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace XPPassbook.Models
{
    public class XPer
    {
        public int InvestmentAmount { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
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
            xml = new SettingsXml();

            if (xml.IsSuccess)
            {
                InvestmentAmount = xml.InvestmentAmount;
                Name = xml.Name;
                Address = xml.Address;
                ImageFullName = xml.ImageFullName;
            }
        }

        public void Update()
        {
            xml.InvestmentAmount = InvestmentAmount;
            xml.Name = Name;
            xml.Address = Address;
            xml.ImageFullName = ImageFullName;
            xml.Update();
        }
    }
}
