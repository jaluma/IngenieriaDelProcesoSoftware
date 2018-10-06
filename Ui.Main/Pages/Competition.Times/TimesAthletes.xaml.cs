using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using Logic.Db.Dto;
using Logic.Db.Util.Services;

namespace Ui.Main.Pages.Competition.Times
{
    /// <summary>
    /// Lógica de interacción para TimesAthletes.xaml
    /// </summary>
    public partial class TimesAthletes : Page {

        private readonly TimesService _service;
        private DataTable _table;

        public TimesAthletes()
        {
            this.
            InitializeComponent();

        }

        public TimesAthletes(CompetitionDto competition) : this() {
            // inicialize data table
            _service = new TimesService(competition, MaleCheckBox.IsChecked, FemaleCheckBox.IsChecked);
            GenerateDataGrid();
        }

        private void GenerateDataGrid() {

            _table = _service.SelectCompetitionTimes();
            _table.Columns[0].ColumnName = Properties.Resources.AthleteDorsal;
            _table.Columns[1].ColumnName = Properties.Resources.AthleteDni;
            _table.Columns[2].ColumnName = Properties.Resources.AthleteName;
            _table.Columns[3].ColumnName = Properties.Resources.AthleteSurname;
            _table.Columns[4].ColumnName = Properties.Resources.AthleteGender;
            _table.Columns[5].ColumnName = Properties.Resources.InitialTime;
            _table.Columns[6].ColumnName = Properties.Resources.FinishTime;

            DataGridTimes.ItemsSource = _table.DefaultView;
        }

        private void CheckBox_OnClick(object sender, RoutedEventArgs e) {
            //DataTable dt = _table; 

            //dt.DefaultView.RowFilter = GenerateFilter();

            //DataGridTimes.ItemsSource = dt.DefaultView;

            _table.DefaultView.RowFilter = GenerateFilter();
            DataGridTimes.ItemsSource = _table.DefaultView;
        }

        private string GenerateFilter() {
            string filter = null;

            if (MaleIsChecked() || FemaleIsChecked()) {
                filter = string.Empty;

                if (MaleIsChecked()) {
                    filter += $"{Properties.Resources.AthleteGender} = 'M'";
                }

                if (MaleIsChecked() && FemaleIsChecked()) {
                    filter += " or ";
                }

                if (FemaleIsChecked()) {
                    filter += $"{Properties.Resources.AthleteGender} = 'F'";
                }
            }

            return filter;
        }

        private bool MaleIsChecked() {
            return (bool) MaleCheckBox.IsChecked;
        }

        private bool FemaleIsChecked() {
            return (bool) FemaleCheckBox.IsChecked;
        }
    }
}
