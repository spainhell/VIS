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
using core;
using core.dbmappers;
using core.logic;
using core.models;
using core.xmlmappers;


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
            List<VehicleType> typesList = VehicleTypeXmlMapper.SelectAll();
            cbType.ItemsSource = typesList;
            cbType.DisplayMemberPath = "TypeName";
            cbType.SelectedValuePath = "Id";

            List<VehicleBrand> brandsList = VehicleBrandXmlMapper.SelectAll();
            cbBrand.ItemsSource = brandsList;
            cbBrand.DisplayMemberPath = "BrandName";
            cbBrand.SelectedValuePath = "Id";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var typeId = cbType.SelectedValue;
            var brandId = cbBrand.SelectedValue;

            Vehicle vm = new Vehicle()
            {
                Id = -1, LicensePlate = tbLicensePlate.Text, Price = Convert.ToDecimal(tbPrice.Text), PurchasedOn = Convert.ToDateTime(datePicker.Text),
                Title = tbTitle.Text, VehicleBrand = null, VehicleType = null, Vin = tbVin.Text, Vintage = Convert.ToInt16(tbVintage.Text)
            };

            VehicleLogic.Create(_sqlConn, vm, Convert.ToInt32(typeId), Convert.ToInt32(brandId), 4, 1);
        }
    }
}
