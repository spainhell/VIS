using System;
using System.Collections.Generic;
using System.Xml;
using core.models;


namespace core.xmlmappers
{
    public class VehicleTypeXmlMapper
    {
        private static string filename = appconfig.xmlTypes;
        public static List<VehicleType> SelectAll()
        {
            List<VehicleType> vtmList = new List<VehicleType>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            XmlNodeList list = xmlDoc.SelectNodes("//VehicleTypes//VehicleType");

            foreach (XmlNode record in list)
            {
                VehicleType vtm = new VehicleType()
                {
                    Id = Convert.ToInt32(record.Attributes?["id"].Value),
                    TypeName = record.SelectSingleNode("TypeName")?.InnerText
                };
                vtmList.Add(vtm);
                //XmlNode myParent = record.SelectSingleNode("../..");
            }

            return vtmList;
        }

        public static VehicleType SelectById(int id)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            XmlNode node = xmlDoc.SelectSingleNode($"//VehicleTypes//VehicleType[@id='{id}']");

            if (node == null) return null;

            VehicleType vtm = new VehicleType()
            {
                Id = id,
                TypeName = node.SelectSingleNode("TypeName")?.InnerText
            };
            return vtm;
        }

        

        public static int Insert(VehicleType vtm)
        {
            if (vtm == null) return -2;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            XmlNodeList list = xmlDoc.SelectNodes($"//VehicleTypes//@id");

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

            XmlNode root = xmlDoc.SelectSingleNode("VehicleTypes");
            if (root == null) return -1;

            XmlElement vehicleType = xmlDoc.CreateElement(string.Empty, "VehicleType", string.Empty);
            XmlAttribute attrId = xmlDoc.CreateAttribute("id");
            attrId.Value = maxId.ToString();
            vehicleType.Attributes.Append(attrId);

            XmlElement typeName = xmlDoc.CreateElement(string.Empty, "TypeName", string.Empty);
            XmlText text1 = xmlDoc.CreateTextNode(vtm.TypeName);
            typeName.AppendChild(text1);
            vehicleType.AppendChild(typeName);

            root.AppendChild(vehicleType);

            xmlDoc.Save(filename);

            return maxId;
        }

        public static int Update(VehicleType vtm)
        {
            if (vtm == null) return -2;
            if (vtm.Id < 0) return -3;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            XmlNode node = xmlDoc.SelectSingleNode($"//VehicleTypes//VehicleType[@id='{vtm.Id}']");

            XmlNode typeName = node?.SelectSingleNode("TypeName");

            if (typeName == null) return -1;
            typeName.InnerText = vtm.TypeName;

            xmlDoc.Save(filename);

            return 0;
        }

        public static int Delete(VehicleType vtm)
        {
            if (vtm == null) return -2;
            if (vtm.Id < 0) return -3;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            XmlNode node = xmlDoc.SelectSingleNode($"//VehicleTypes//VehicleType[@id='{vtm.Id}']");

            if (node != null)
            {
                XmlNode root = xmlDoc.SelectSingleNode("VehicleTypes");
                root?.RemoveChild(node);
            }
            else return -1;

            xmlDoc.Save(filename);

            return 0;
        }
    }
}
