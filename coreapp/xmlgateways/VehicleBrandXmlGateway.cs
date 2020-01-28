using System;
using System.Xml;

namespace core.xmlgateways
{
    public class VehicleBrandXmlGateway
    {
        private static VehicleBrandXmlGateway instance = new VehicleBrandXmlGateway();
        private static XmlDocument brandNodesDocument;

        private VehicleBrandXmlGateway()
        {
            Load(appconfig.xmlBrands);
        }
        private static void Load(string filename)
        {
            brandNodesDocument = new XmlDocument();
            brandNodesDocument.Load(filename);
        }

        private static void Save(string filename)
        {
            brandNodesDocument.Save(filename);
        }

        public static XmlNodeList FindAll()
        {
            return brandNodesDocument.SelectNodes("//VehicleBrands//VehicleBrand");
        }
        public static XmlNode FindById(int id)
        {
            return brandNodesDocument.SelectSingleNode($"//VehicleBrands//VehicleBrand[@id='{id}']"); ;
        }

        public static void Insert(string brand, string url)
        {
            XmlNodeList list = brandNodesDocument.SelectNodes($"//VehicleBrands//@id");

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

            XmlNode root = brandNodesDocument.SelectSingleNode("VehicleBrands");
            XmlElement vehicleBrand = brandNodesDocument.CreateElement(string.Empty, "VehicleBrand", string.Empty);
            XmlAttribute attrId = brandNodesDocument.CreateAttribute("id");
            attrId.Value = maxId.ToString();
            vehicleBrand.Attributes.Append(attrId);
            XmlElement brandName = brandNodesDocument.CreateElement(string.Empty, "BrandName", string.Empty);
            XmlText text1 = brandNodesDocument.CreateTextNode(brand);
            brandName.AppendChild(text1);
            vehicleBrand.AppendChild(brandName);
            XmlElement logoUrl = brandNodesDocument.CreateElement(string.Empty, "LogoUrl", string.Empty);
            XmlText text2 = brandNodesDocument.CreateTextNode(url);
            logoUrl.AppendChild(text2);
            vehicleBrand.AppendChild(logoUrl);
            root.AppendChild(vehicleBrand);

            Save(appconfig.xmlBrands);
        }

        public static void Update(int id, string brand, string url)
        {
            XmlNode node = brandNodesDocument.SelectSingleNode($"//VehicleBrands//VehicleBrand[@id='{id}']");
            node.SelectSingleNode("BrandName").InnerText = brand;
            node.SelectSingleNode("LogoUrl").InnerText = url;
            Save(appconfig.xmlBrands);
        }

        public static void Delete(int id)
        {
            XmlNode node = brandNodesDocument.SelectSingleNode($"//VehicleBrands//VehicleBrand[@id='{id}']");
            XmlNode root = brandNodesDocument.SelectSingleNode("VehicleBrands");
            root?.RemoveChild(node);
            Save(appconfig.xmlBrands);
        }
    }
}
