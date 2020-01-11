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
using testapp;

namespace wpfapp
{
    /// <summary>
    /// Interaction logic for PageFCE1.xaml
    /// </summary>
    public partial class PageStatistics : Page
    {
        private readonly SQLiteConnection _sqlConn;
        private DataRowView _selectedDataRow;

        public PageStatistics(SQLiteConnection sqlConn)
        {
            _sqlConn = sqlConn;
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            /*DataSet dsStatistics = DBDBDB.GetStatistics(_sqlConn);
            DataTable dtCounts = dsStatistics.Tables["Counts"];
            DataRow drCounts = dtCounts.Rows[0];
            lblUsers.Content = $"{drCounts["active_users"]} aktivních / {drCounts["total_users"]} celkem";
            lblVehicles.Content = $"{drCounts["vehicles_count"]}";
            lblInspections.Content = $"{drCounts["inspections_count"]}";
            lblInsp1.Content = $"{drCounts["insp_0_30"]}";
            lblInsp2.Content = $"{drCounts["insp_0_7"]}";
            lblInsp3.Content = $"{drCounts["insp_expired"]}";
            lblAge1.Content = $"{drCounts["age_0_1"]}";
            lblAge2.Content = $"{drCounts["age_2_4"]}";
            lblAge3.Content = $"{drCounts["age_5_more"]}";

            DataTable dtBrands = dsStatistics.Tables["Brands"];
            lblBrand1.Content = $"{dtBrands.Rows[0][0]}";
            lblBrand2.Content = $"{dtBrands.Rows[1][0]}";
            lblBrand3.Content = $"{dtBrands.Rows[2][0]}";

            DataTable dtTypes = dsStatistics.Tables["Types"];
            lblType1.Content = $"{dtTypes.Rows[0][0]}";
            lblType2.Content = $"{dtTypes.Rows[1][0]}";
            lblType3.Content = $"{dtTypes.Rows[2][0]}";

            DataTable dtStations = dsStatistics.Tables["Stations"];
            lblStation1.Content = $"{dtStations.Rows[0][0]}";
            lblStation2.Content = $"{dtStations.Rows[1][0]}";
            lblStation3.Content = $"{dtStations.Rows[2][0]}";
            */



            // DgNotifications.ItemsSource = dtVehicles?.DefaultView;
        }
    }
}
