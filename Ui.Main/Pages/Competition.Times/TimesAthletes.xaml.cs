using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Logic.Db.Util.Services;

namespace Ui.Main.Pages.Competition.Times
{
    /// <summary>
    /// Lógica de interacción para TimesAthletes.xaml
    /// </summary>
    public partial class TimesAthletes : Page {

        private readonly TimesService _service;
        public static CompetitionDto Competition;
        private DataTable _table;
        private List<long> _ids;
        private List<string> _list;

        public TimesAthletes()
        {
            this.
            InitializeComponent();

            CompetitionService competitionService = new CompetitionService();
            _table = competitionService.SelectCompetitionFinish();

            _service = new TimesService();
            
            
            _list = new List<string>();
            _ids = new List<long>();

            foreach (DataRow row in _table.Rows) {
                _ids.Add(long.Parse(row[_table.Columns.IndexOf("Competition_ID")].ToString()));
                _list.Add(row[_table.Columns.IndexOf("Competition_Name")].ToString());
            }

            CompetitionList.ItemsSource = _list;

            //if (_list.Count > 0)
            //    CompetitionList.SelectedIndex = 0;
        }

        private void GenerateDataGrid() {

            if (Competition != null) {
                _table = new DataTable();
                DataColumn column = new DataColumn(Properties.Resources.AthletePosition, typeof(string)) {
                    AllowDBNull = true
                };
                _table.Columns.Add(column);

                _table.Columns[Properties.Resources.AthletePosition].AutoIncrement = true;
                _table.Columns[Properties.Resources.AthletePosition].AutoIncrementSeed = 1;
                _table.Columns[Properties.Resources.AthletePosition].AutoIncrementStep = 1;

                _table.Merge(_service.SelectCompetitionTimes(Competition));
                _table.Columns[1].ColumnName = Properties.Resources.AthleteDorsal;
                _table.Columns[2].ColumnName = Properties.Resources.AthleteDni;
                _table.Columns[3].ColumnName = Properties.Resources.AthleteName;
                _table.Columns[4].ColumnName = Properties.Resources.AthleteSurname;
                _table.Columns[5].ColumnName = Properties.Resources.AthleteGender;
                _table.Columns[6].ColumnName = Properties.Resources.InitialTime;
                _table.Columns[7].ColumnName = Properties.Resources.FinishTime;

                _table.Columns.Remove(Properties.Resources.InitialTime);
                _table.Columns.Remove(Properties.Resources.FinishTime);

                DataGridTimes.ItemsSource = _table.DefaultView;
            }
        }

        private void CheckBox_OnClick(object sender, RoutedEventArgs e) {
            _table.DefaultView.RowFilter = GenerateFilter();
            DataGridTimes.ItemsSource = _table.DefaultView;
        }

        private string GenerateFilter() {
            string filter = string.Empty;

            if (MaleIsChecked() && FemaleIsChecked()) {
                filter = null;
            } else if (MaleIsChecked()) {
                filter = $"{Properties.Resources.AthleteGender} = 'M'";
            } else if (FemaleIsChecked()) {
                filter = $"{Properties.Resources.AthleteGender} = 'F'";
            } else {
                filter = $"{Properties.Resources.AthleteGender} <> 'F' and {Properties.Resources.AthleteGender} <> 'M'";
            }

            return filter;
        }

        private bool MaleIsChecked() {
            return (bool) MaleCheckBox.IsChecked;
        }

        private bool FemaleIsChecked() {
            return (bool) FemaleCheckBox.IsChecked;
        }

        private void CompetitionList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Competition = new CompetitionDto() {
                ID = _ids[CompetitionList.SelectedIndex]
            };

            GenerateDataGrid();
        }

        private void OnMouseEnter(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = Cursors.Hand;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = null;
        }

        private void TimesAthletes_OnLoaded(object sender, RoutedEventArgs e) {
            if (Competition != null) {
                CompetitionList.SelectedIndex = _ids.IndexOf(Competition.ID);
            }
        }
    }
}
