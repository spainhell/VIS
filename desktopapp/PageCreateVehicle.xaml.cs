using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using bl;
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

            tbLicensePlate.Text = "";
            tbPrice.Text = "";
            tbTitle.Text = "";
            tbVin.Text = "";
            tbVintage.Text = "";
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

            var result = VehicleLogic.Create(_sqlConn, vm, Convert.ToInt32(typeId), Convert.ToInt32(brandId), 4, 1);
            if (result == -1)
                MessageBox.Show($"VIN již v DB existuje.", "Stop", MessageBoxButton.OK, MessageBoxImage.Stop);

            else if (result == 0)
            {
                MessageBox.Show($"Vozidlo bylo uloženo.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                Refresh();
            }
        }
    }
}
