using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using bl;
using core.dbmappers;
using core.models;

namespace wpfapp
{
    public partial class PageNotifications : Page
    {
        private readonly SQLiteConnection _sqlConn;
        private List<Notification> _notifications;

        public PageNotifications(SQLiteConnection sqlConn)
        {
            _sqlConn = sqlConn;
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            _notifications = NotificationLogic.GetNotifications(_sqlConn, 30);
            DgNotifications.ItemsSource = _notifications;
        }

        private void DgVehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //_selectedVehicle = DgNotifications.SelectedItem as Vehicle;
            //BtnDetail.IsEnabled = true;
        }

        private void BtnSendNotify_Click(object sender, RoutedEventArgs e)
        {
            bool r = NotificationLogic.SaveNotifications(_sqlConn, _notifications);

            if (r)
            {
                MessageBox.Show($"Notifikace byly uloženy do DB", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Notifikace se nepodařilo uložit do DB.", "Varování", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            Refresh();
        }
    }
}
