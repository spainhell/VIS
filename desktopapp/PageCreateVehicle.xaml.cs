using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using coreapp;
using testapp;
using testapp.dbmappers;
using testapp.models;
using testapp.xmlmappers;

namespace wpfapp
{
    /// <summary>
    /// Interaction logic for PageFCE1.xaml
    /// </summary>
    public partial class PageCreateVehicle : Page
    {
        private readonly SQLiteConnection _sqlConn;
        private readonly int _userId;

        public PageCreateVehicle(SQLiteConnection sqlConn, int userId)
        {
            _userId = userId;
            _sqlConn = sqlConn;
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            List<VehicleTypeModel> typesList = VehicleTypeXmlMapper.SelectAll(appconfig.xmlTypes);
            cbType.ItemsSource = typesList;
            cbType.DisplayMemberPath = "TypeName";
            cbType.SelectedValuePath = "Id";

            List<VehicleBrandModel> brandsList = VehicleBrandXmlMapper.SelectAll(appconfig.xmlBrands);
            cbBrand.ItemsSource = brandsList;
            cbBrand.DisplayMemberPath = "BrandName";
            cbBrand.SelectedValuePath = "Id";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var typeId = cbType.SelectedValue;
            var brandId = cbBrand.SelectedValue;

            VehicleTypeModel selectedType = null;
            VehicleBrandModel selectedBrand = null;

            if (typeId != null) selectedType = VehicleTypeXmlMapper.SelectById(appconfig.xmlTypes, Convert.ToInt32(typeId));
            if (typeId != null) selectedBrand = VehicleBrandXmlMapper.SelectById(appconfig.xmlBrands, Convert.ToInt32(brandId));

            VehicleModel vm = new VehicleModel()
            {
                Id = -1, LicensePlate = tbLicensePlate.Text, Price = Convert.ToDecimal(tbPrice.Text), PurchasedOn = Convert.ToDateTime(datePicker.Text),
                Title = tbTitle.Text, VehicleBrand = selectedBrand, VehicleType = selectedType, Vin = tbVin.Text, Vintage = Convert.ToInt16(tbVintage.Text)
            };

            VehicleDbMapper.Insert(_sqlConn, vm);
        }
    }
}
