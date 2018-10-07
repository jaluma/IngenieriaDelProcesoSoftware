using System;
using System.Collections.Generic;
using System.Data;
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
using Logic.Db.Dto;
using Logic.Db.Util;
using Logic.Db.Util.Services;

namespace Ui.Main.Pages.Competition.Times
{
    /// <summary>
    /// Lógica de interacción para SelectionCompetition.xaml
    /// </summary>
    public partial class SelectionCompetition : Page {

        private readonly CompetitionService _service;

        public SelectionCompetition()
        {
            InitializeComponent();
            // change title window

            // inicialize data table
            _service = new CompetitionService();
            DataTable table = _service.SelectCompetitionFinish();
            table.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Name;
            table.Columns[2].ColumnName = Properties.Resources.Competition_Type;
            table.Columns[3].ColumnName = Properties.Resources.Competition_Km;
            table.Columns[4].ColumnName = Properties.Resources.Competition_Price;
            table.Columns[5].ColumnName = Properties.Resources.Competition_Date;
            table.Columns[6].ColumnName = Properties.Resources.Competition_Number;
            table.Columns[7].ColumnName = Properties.Resources.Competition_Status;

            DataGridCompetition.ItemsSource = table.DefaultView;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            DataRowView row = DataGridCompetition.SelectedItems[0] as DataRowView;

            long id = (long) row[0];

            CompetitionDto competition = new CompetitionDto() {
                ID = (int)id
            };

            _service.Dispose();

            Page timesService = new TimesAthletes(competition);
            
            Content = new Frame() {
                Content = timesService
            };

        }

    }
}
