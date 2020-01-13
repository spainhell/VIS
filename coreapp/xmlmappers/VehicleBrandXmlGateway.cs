using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using coreapp;
using testapp.models;

namespace coreapp.xmlmappers
{
    public class VehicleBrandXmlGateway
    {
        private static VehicleBrandXmlGateway instance = new VehicleBrandXmlGateway();
        private static List<VehicleBrandModel> brands;

        private VehicleBrandXmlGateway()
        {
            brands = Load(appconfig.xmlBrands);
        }
        private static List<VehicleBrandModel> Load(string filename)
        {
            List<VehicleBrandModel> vbmList = new List<VehicleBrandModel>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);

            XmlNodeList list = xmlDoc.SelectNodes("//VehicleBrands//VehicleBrand");

            foreach (XmlNode record in list)
            {
                VehicleBrandModel vtm = new VehicleBrandModel()
                {
                    Id = Convert.ToInt32(record.Attributes?["id"].Value),
                    BrandName = record.SelectSingleNode("BrandName")?.InnerText,
                    LogoUrl = record.SelectSingleNode("LogoUrl")?.InnerText
                };
                vbmList.Add(vtm);
            }

            return vbmList;
        }

        private static void Save(string filename)
        {

        }

        public static VehicleBrandModel SelectById(int id)
        {
            return brands.Single(c => c.Id == id);
        }

        public static void Insert(VehicleBrandModel vbm)
        {
            brands.Add(vbm);
            Save(appconfig.xmlBrands);
        }

        public static void Update(VehicleBrandModel vbm)
        {
            Save(appconfig.xmlBrands);
        }

        public static void Delete(VehicleBrandModel vbm)
        {
            var itemToDelete = brands.Single(c => c.Id == vbm.Id);
            brands.Remove(itemToDelete);
            Save(appconfig.xmlBrands);
        }
    }
}
