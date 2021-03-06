﻿using System;
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
using bl;
using core.dbmappers;
using core.models;


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
            List <Vehicle> vehicles = VehicleLogic.FindAll(_sqlConn);
            cbVehicle.ItemsSource = vehicles;
            cbVehicle.DisplayMemberPath = "Title";
            cbVehicle.SelectedValuePath = "Id";

            List<InspectionStation> stations = InspectionStationLogic.FindAll(_sqlConn);
            cbStation.ItemsSource = stations;
            cbStation.DisplayMemberPath = "Company";
            cbStation.SelectedValuePath = "Id";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Inspection im = new Inspection()
                {
                    Id = -1,
                    Vehicle = VehicleLogic.FindById(_sqlConn, Convert.ToInt32(cbVehicle.SelectedValue)),
                    InspectionDate = inspectionDatePicker.SelectedDate.Value,
                    ValidTo = validToPicker.SelectedDate.Value,
                    InspectionStation = InspectionStationLogic.FindById(_sqlConn, Convert.ToInt32(cbStation.SelectedValue)),
                    ProtocolNumber = tbProtocolNr.Text,
                    Tachometer = Convert.ToInt32(tbTachometer.Text),
                    Price = Convert.ToInt32(tbPrice.Text),
                    Defects = tbDefects.Text
                };

                bool result = InspectionLogic.Insert(_sqlConn, im, out var error);
                if (!result)
                {
                    MessageBox.Show(error, "Stop", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                else
                {
                    MessageBox.Show($"Prohlídka byla uložena.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Vyplněné údaje jsou chybné:\n{ex.Message}",
                    "Stop", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

        }

        private void cbVehicle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Inspection newInsp = InspectionLogic.PrepareNewInspection(_sqlConn, Convert.ToInt32(cbVehicle.SelectedValue));
            if (newInsp == null) return;
            inspectionDatePicker.SelectedDate = newInsp.InspectionDate;
            validToPicker.SelectedDate = newInsp.ValidTo;
            cbStation.SelectedValue = newInsp.InspectionStation.Id;
            tbPrice.Text = newInsp.Price.ToString();

        }
    }
}
