using System;
using System.Collections.Generic;
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
using coreapp;

namespace wpfapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SQLiteConnection _sqlConn;

        private PageStatistics _pageStatistics;
        private PageVehicles _pageVehicles;
        private PageCreateVehicle _pageCreateVehicle;
        private PageCreateInspection _pageCreateInspection;
        private PageNotifications _pageNotifications;

        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            try
            {
                _sqlConn = new SQLiteConnection(appconfig.sqlite);
                _sqlConn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Nepodařilo se připojit k DB", "Chyba", MessageBoxButton.OK,
                    MessageBoxImage.Stop);
                System.Environment.Exit(-1);
            }

            _pageStatistics = new PageStatistics(_sqlConn);
            FrmModule.Content = _pageStatistics;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _sqlConn?.Close();
            
        }

        private void BtnVehicles_Click(object sender, RoutedEventArgs e)
        {
            if (FrmModule.Content.Equals(_pageVehicles)) return;
            RectSide.Margin = new Thickness(RectSide.Margin.Left, btnVehicles.Margin.Top, RectSide.Margin.Right, RectSide.Margin.Bottom);
            if (_pageVehicles != null)
            {
                FrmModule.Content = _pageVehicles;
                _pageVehicles.Refresh();
            }
            else
            {
                _pageVehicles = new PageVehicles(_sqlConn);
                FrmModule.Content = _pageVehicles;
            }
        }

        private void BtnStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (FrmModule.Content.Equals(_pageStatistics)) return;
            RectSide.Margin = new Thickness(RectSide.Margin.Left, btnStatistics.Margin.Top, RectSide.Margin.Right, RectSide.Margin.Bottom);
            if (_pageStatistics != null)
            {
                FrmModule.Content = _pageStatistics;
                _pageStatistics.Refresh();
            }
            else
            {
                _pageStatistics = new PageStatistics(_sqlConn);
                FrmModule.Content = _pageStatistics;
            }
        }

        private void BtnCreateVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (FrmModule.Content.Equals(_pageCreateVehicle)) return;
            RectSide.Margin = new Thickness(RectSide.Margin.Left, btnCreateVehicle.Margin.Top, RectSide.Margin.Right, RectSide.Margin.Bottom);
            if (_pageCreateVehicle != null)
            {
                FrmModule.Content = _pageCreateVehicle;
                _pageCreateVehicle.Refresh();
            }
            else
            {
                _pageCreateVehicle = new PageCreateVehicle(_sqlConn, 1);
                FrmModule.Content = _pageCreateVehicle;
            }
        }

        private void BtnCreateInspection_Click(object sender, RoutedEventArgs e)
        {
            if (FrmModule.Content.Equals(_pageCreateInspection)) return;
            RectSide.Margin = new Thickness(RectSide.Margin.Left, btnCreateInspection.Margin.Top, RectSide.Margin.Right, RectSide.Margin.Bottom);
            if (_pageCreateInspection != null)
            {
                FrmModule.Content = _pageCreateInspection;
                _pageCreateInspection.Refresh();
            }
            else
            {
                _pageCreateInspection = new PageCreateInspection(_sqlConn, 1);
                FrmModule.Content = _pageCreateInspection;
            }
        }

        private void BtnViewNotifications_Click(object sender, RoutedEventArgs e)
        {
            if (FrmModule.Content.Equals(_pageNotifications)) return;
            RectSide.Margin = new Thickness(RectSide.Margin.Left, btnNotifications.Margin.Top, RectSide.Margin.Right, RectSide.Margin.Bottom);
            if (_pageNotifications != null)
            {
                FrmModule.Content = _pageNotifications;
                _pageNotifications.Refresh();
            }
            else
            {
                _pageNotifications = new PageNotifications(_sqlConn);
                FrmModule.Content = _pageNotifications;
            }
        }
    }
}
