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
using core.dbmappers;
using core.models;


namespace wpfapp
{
    /// <summary>
    /// Interaction logic for PageFCE1.xaml
    /// </summary>
    public partial class PageVehicles : Page
    {
        private readonly SQLiteConnection _sqlConn;
        private Vehicle _selectedVehicle;

        public PageVehicles(SQLiteConnection sqlConn)
        {
            _sqlConn = sqlConn;
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            var vehicles = VehicleDbMapper.SelectAll(_sqlConn);
            DgVehicles.ItemsSource = vehicles;
        }

        private void DgVehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedVehicle = DgVehicles.SelectedItem as Vehicle;
            BtnDetail.IsEnabled = true;
        }

        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {
            VehicleDetailWindow vdw = new VehicleDetailWindow(_sqlConn, _selectedVehicle, this);
            vdw.Show();
        }
    }
}
