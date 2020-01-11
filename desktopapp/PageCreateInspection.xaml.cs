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
    public partial class PageCreateInspection : Page
    {
        private readonly SQLiteConnection _sqlConn;
        private readonly int _userId;

        public PageCreateInspection(SQLiteConnection sqlConn, int userId)
        {
            _userId = userId;
            _sqlConn = sqlConn;
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            List <VehicleModel> vehicles = VehicleDbMapper.SelectAll(_sqlConn);
            cbVehicle.ItemsSource = vehicles;
            cbVehicle.DisplayMemberPath = "Title";
            cbVehicle.SelectedValuePath = "Id";

            List<InspectionStationModel> stations = InspectionStationDbMapper.SelectAll(_sqlConn);
            cbStation.ItemsSource = stations;
            cbStation.DisplayMemberPath = "Company";
            cbStation.SelectedValuePath = "Id";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InspectionModel im = new InspectionModel()
                {
                    Id = -1,
                    Vehicle = VehicleDbMapper.SelectById(_sqlConn, Convert.ToInt32(cbVehicle.SelectedValue)),
                    InspectionDate = inspectionDatePicker.SelectedDate.Value,
                    ValidTo = validToPicker.SelectedDate.Value,
                    InspectionStation = InspectionStationDbMapper.SelectById(_sqlConn, Convert.ToInt32(cbStation.SelectedValue)),
                    ProtocolNumber = tbProtocolNr.Text,
                    Tachometer = Convert.ToInt32(tbTachometer.Text),
                    Price = Convert.ToInt32(tbPrice.Text),
                    Defects = tbDefects.Text
                };

                if (im.InspectionDate >= im.ValidTo)
                {
                    MessageBox.Show($"Chybně vyplněné datum platnosti STK.", "Stop", MessageBoxButton.OK, MessageBoxImage.Stop);
                }

                if (im.Price < 0)
                {
                    MessageBox.Show($"Chybně vyplněná cena STK.", "Stop", MessageBoxButton.OK, MessageBoxImage.Stop);
                }

                // kontrola tachometru

                int result = InspectionDbMapper.Insert(_sqlConn, im);
                if (result != 0)
                {
                    MessageBox.Show($"Prohlídku se nepodařilo uložit do DB.", "Stop", MessageBoxButton.OK, MessageBoxImage.Stop);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Vyplněné údaje jsou chybné:\n{ex.Message}",
                    "Stop", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }
    }
}
