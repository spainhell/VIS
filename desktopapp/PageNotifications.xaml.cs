using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using testapp.dbmappers;
using testapp.models;

namespace wpfapp
{
    public partial class PageNotifications : Page
    {
        private readonly SQLiteConnection _sqlConn;
        private VehicleModel _selectedVehicle;

        public PageNotifications(SQLiteConnection sqlConn)
        {
            _sqlConn = sqlConn;
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            List<NotificationModel> notifications = NotificationDbMapper.GenerateNotifications(_sqlConn, 30);
            DgNotifications.ItemsSource = notifications;
        }

        private void DgVehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //_selectedVehicle = DgNotifications.SelectedItem as VehicleModel;
            //BtnDetail.IsEnabled = true;
        }

        private void BtnDetail_Click(object sender, RoutedEventArgs e)
        {
            //VehicleDetailWindow vdw = new VehicleDetailWindow(_sqlConn, _selectedVehicle);
            //vdw.Show();
        }
    }
}
