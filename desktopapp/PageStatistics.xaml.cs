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
           
        }
    }
}
