using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XPPassbook.Models
{
    public class SettingsXml
    {
        public bool IsSuccess { get; set; }
        public int InvestmentAmount { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ImageFullName { get; set; }

        string fileFullName;

        public SettingsXml()
        {
            var programDataFullName = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/XpJp/XP-Passbook/";
            if (!Directory.Exists(programDataFullName))
            {
                Directory.CreateDirectory(programDataFullName);
            }

            IsSuccess = true;
            fileFullName = programDataFullName + "Settings.xml";
            Open();
        }

        private void Open()
        {
            if (!File.Exists(fileFullName))
            {
                Create();
            }

            if (IsSuccess)
            {
                Load();
            }
        }

        private void Create()
        {
            IsSuccess = false;

            try
            {
                var xml = new XDocument(
                    new XElement("Settings",
                    new XElement("Name", new XAttribute("Value", "No name")),
                    new XElement("Address", new XAttribute("Value", "No address")),
                    new XElement("InvestmentAmount", new XAttribute("Value", 0)),
                    new XElement("ImageFullName", new XAttribute("Value", string.Empty))));

                xml.Save(fileFullName);
                IsSuccess = true;
            }
            catch
            {

            }
        }

        private void Load()
        {
            IsSuccess = false;

            try
            {
                var xml = XDocument.Load(fileFullName);

                InvestmentAmount = int.Parse(xml.Descendants("InvestmentAmount").Single().Attribute("Value").Value);
                Name = xml.Descendants("Name").Single().Attribute("Value").Value;
                Address = xml.Descendants("Address").Single().Attribute("Value").Value;
                ImageFullName = xml.Descendants("ImageFullName").Single().Attribute("Value").Value;

                IsSuccess = true;
            }
            catch
            {

            }
        }

        public void Update()
        {
            IsSuccess = false;

            try
            {
                var xml = XDocument.Load(fileFullName);

                xml.Descendants("InvestmentAmount").Single().Attribute("Value").Value = InvestmentAmount.ToString();
                xml.Descendants("Name").Single().Attribute("Value").Value = Name;
                xml.Descendants("Address").Single().Attribute("Value").Value = Address;
                xml.Descendants("ImageFullName").Single().Attribute("Value").Value = ImageFullName;

                xml.Save(fileFullName);
                IsSuccess = true;
            }
            catch
            {

            }
        }
    }
}
