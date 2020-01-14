using System;
using System.Collections.Generic;
using System.Xml;
using core.models;


namespace core.xmlmappers
{
    public class VehicleBrandXmlMapper
    {
        private static string filename = appconfig.xmlTypes;
        public static List<VehicleBrand> SelectAll()
        {
            List<VehicleBrand> vbmList = new List<VehicleBrand>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            XmlNodeList list = xmlDoc.SelectNodes("//VehicleBrands//VehicleBrand");

            foreach (XmlNode record in list)
            {
                VehicleBrand vtm = new VehicleBrand()
                {
                    Id = Convert.ToInt32(record.Attributes?["id"].Value),
                    BrandName = record.SelectSingleNode("BrandName")?.InnerText,
                    LogoUrl = record.SelectSingleNode("LogoUrl")?.InnerText
                };
                vbmList.Add(vtm);
            }

            return vbmList;
        }

        public static VehicleBrand SelectById(int id)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            XmlNode node = xmlDoc.SelectSingleNode($"//VehicleBrands//VehicleBrand[@id='{id}']");

            if (node == null) return null;

            VehicleBrand vtm = new VehicleBrand()
            {
                Id = id,
                BrandName = node.SelectSingleNode("BrandName")?.InnerText,
                LogoUrl = node.SelectSingleNode("LogoUrl")?.InnerText
            };
            return vtm;
        }

        public static int Insert(VehicleBrand vbm)
        {
            if (vbm == null) return -2;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            XmlNodeList list = xmlDoc.SelectNodes($"//VehicleBrands//@id");

            // získáme příští ID záznamu
            int maxId = 0;
            if (list != null)
            {
                foreach (XmlNode node in list)
                {
                    int nodeId = Convert.ToInt32(node.Value);
                    if (nodeId > maxId)
                    {
                        maxId = nodeId;
                    }
                }

                maxId++;
            }

            XmlNode root = xmlDoc.SelectSingleNode("VehicleBrands");
            if (root == null) return -1;

            XmlElement vehicleBrand = xmlDoc.CreateElement(string.Empty, "VehicleBrand", string.Empty);
            XmlAttribute attrId = xmlDoc.CreateAttribute("id");
            attrId.Value = maxId.ToString();
            vehicleBrand.Attributes.Append(attrId);

            XmlElement brandName = xmlDoc.CreateElement(string.Empty, "BrandName", string.Empty);
            XmlText text1 = xmlDoc.CreateTextNode(vbm.BrandName);
            brandName.AppendChild(text1);
            vehicleBrand.AppendChild(brandName);

            XmlElement logoUrl = xmlDoc.CreateElement(string.Empty, "LogoUrl", string.Empty);
            XmlText text2 = xmlDoc.CreateTextNode(vbm.LogoUrl);
            logoUrl.AppendChild(text2);
            vehicleBrand.AppendChild(logoUrl);

            root.AppendChild(vehicleBrand);

            xmlDoc.Save(filename);

            return maxId;
        }

        public static int Update(VehicleBrand vbm)
        {
            if (vbm == null) return -2;
            if (vbm.Id < 0) return -3;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            XmlNode node = xmlDoc.SelectSingleNode($"//VehicleBrands//VehicleBrand[@id='{vbm.Id}']");

            XmlNode brandName = node?.SelectSingleNode("BrandName");
            XmlNode logoUrl = node?.SelectSingleNode("LogoUrl");


            if (brandName == null || logoUrl == null) return -1;
            brandName.InnerText = vbm.BrandName;
            logoUrl.InnerText = vbm.LogoUrl;

            xmlDoc.Save(filename);

            return 0;
        }

        public static int Delete(VehicleBrand vbm)
        {
            if (vbm == null) return -2;
            if (vbm.Id < 0) return -3;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            XmlNode node = xmlDoc.SelectSingleNode($"//VehicleBrands//VehicleBrand[@id='{vbm.Id}']");

            if (node != null)
            {
                XmlNode root = xmlDoc.SelectSingleNode("VehicleBrands");
                root?.RemoveChild(node);
            }
            else return -1;

            xmlDoc.Save(filename);

            return 0;
        }
    }
}
