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
using System.Windows.Shapes;
using testapp;
using testapp.dbmappers;
using testapp.models;

namespace wpfapp
{
    /// <summary>
    /// Interaction logic for VehicleDetailWindow.xaml
    /// </summary>
    public partial class VehicleDetailWindow : Window
    {
        private readonly SQLiteConnection _sqlConn;
        private readonly VehicleModel _vehicle;
        private readonly PageVehicles _parent;

        public VehicleDetailWindow(SQLiteConnection sqlConn, VehicleModel vehicle, PageVehicles parent)
        {
            _sqlConn = sqlConn;
            _vehicle = vehicle;
            _parent = parent;
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            tbTitle.Text = _vehicle.Title;
            tbType.Text = _vehicle.VehicleType.TypeName;
            tbVin.Text = _vehicle.Vin;
            tbLicensePlate.Text = _vehicle.LicensePlate;
            tbBrand.Text = _vehicle.VehicleBrand.BrandName;
            tbVintage.Text = _vehicle.Vintage.ToString();

            List<Inspection> inspections = InspectionDbMapper.SelectAllByVehicleId(_sqlConn, _vehicle.Id);

            List<Inspection> sortedInspections = inspections.OrderByDescending(c => c.InspectionDate).ToList();

            if (sortedInspections.Count == 0) return;

            tbValidDays.Text = (sortedInspections[0].ValidTo - sortedInspections[0].InspectionDate).TotalDays + " dní";
            tbValidTo.Text = sortedInspections[0].ValidTo.ToShortDateString();
            tbStation.Text = sortedInspections[0].InspectionStation.Company;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var mb = MessageBox.Show($"Přejete si opravdu smazat vozdilo?",
                "Smazání vozidla", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (mb == MessageBoxResult.No) return;

            // vyhledání tech. kontrol
            List<Inspection> inspections = InspectionDbMapper.SelectAllByVehicleId(_sqlConn, _vehicle.Id);

            // vyhlednání všech notifikací
            List<NotificationModel> notifications = new List<NotificationModel>(inspections.Count);
            foreach (Inspection inspection in inspections)
            {
                NotificationModel insNotify = NotificationDbMapper.SelectByInspectionId(_sqlConn, inspection.Id);
                if (insNotify != null) notifications.Add(insNotify);
            }

            // smazání všech notifikací
            foreach (NotificationModel notification in notifications)
            {
                NotificationDbMapper.Delete(_sqlConn, notification);
            }

            // smazání všech tech. kontrol
            foreach (Inspection inspection in inspections)
            {
                InspectionDbMapper.Delete(_sqlConn, inspection);
            }

            _parent.Refresh();
            Close();
        }
    }
}
