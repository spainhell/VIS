using System;
using System.Data.SQLite;
using System.Windows;
using bl;
using core.models;


namespace wpfapp
{
    /// <summary>
    /// Interaction logic for VehicleDetailWindow.xaml
    /// </summary>
    public partial class VehicleDetailWindow : Window
    {
        private readonly SQLiteConnection _sqlConn;
        private readonly Vehicle _vehicle;
        private readonly PageVehicles _parent;

        public VehicleDetailWindow(SQLiteConnection sqlConn, Vehicle vehicle, PageVehicles parent)
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

            var lastInsp = InspectionLogic.GetLastInspection(_sqlConn, _vehicle.Id);
            if (lastInsp == null) return;

            tbValidDays.Text = (lastInsp.ValidTo - lastInsp.InspectionDate).TotalDays + " dní";
            tbValidTo.Text = lastInsp.ValidTo.ToShortDateString();
            tbStation.Text = lastInsp.InspectionStation.Company;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var mb = MessageBox.Show($"Přejete si opravdu smazat vozdilo?",
                "Smazání vozidla", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (mb == MessageBoxResult.No) return;

            VehicleLogic.Delete(_sqlConn, 1, _vehicle.Id);

            _parent.Refresh();
            Close();
        }
    }
}
