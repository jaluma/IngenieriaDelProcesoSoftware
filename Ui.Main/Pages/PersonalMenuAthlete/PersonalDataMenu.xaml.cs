using System;
using System.Collections.Generic;
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
using Logic.Db.Util;
using Logic.Db.Util.Services;
using Logic.Db.Properties;


namespace Ui.Main.Pages.PersonalMenuAthlete {
    /// <summary>
    /// Lógica de interacción para PersonalDataMenu.xaml
    /// </summary>
    public partial class PersonalDataMenu : Page {


        private AthletesService _serviceAthlete;
        private AthletesService _serviceAthleteResult;
        private CompetitionService _serviceComp;
        private DataTable _tablePersonal;
        private DataTable _tableInscripcion;
        private DataTable _tableResult;


        public PersonalDataMenu() {
            InitializeComponent();

        }

        private void BtSearch_Click(object sender, RoutedEventArgs e) {


            if (!ComprobarDNI(Dni.Text)) {
                MessageBox.Show(Properties.Resources.InvalidDNI);
                return;
            }
            _serviceAthlete = new AthletesService();
            DataTable table = _serviceAthlete.SelectAthleteByDni(Dni.Text.ToUpper());
            if (table.Rows.Count == 0) {
                MessageBox.Show(Properties.Resources.NoRegistered);
                return;
            }

            GeneratePersonalDataTable();


        }

        private void GeneratePersonalDataTable() {
            _serviceAthlete = new AthletesService();

            _tablePersonal = _serviceAthlete.SelectAthleteByDni(Dni.Text.ToUpper());

            _tablePersonal.Columns[0].ColumnName = Properties.Resources.AthleteDni;
            _tablePersonal.Columns[1].ColumnName = Properties.Resources.AthleteName;
            _tablePersonal.Columns[2].ColumnName = Properties.Resources.AthleteSurname;
            _tablePersonal.Columns[3].ColumnName = Properties.Resources.Competition;
            _tablePersonal.Columns[4].ColumnName = Properties.Resources.AthleteGender;


            for (int i = 1; i < _tablePersonal.Rows.Count; i++) {
                _tablePersonal.Rows.RemoveAt(i);
            }

            _tablePersonal.Columns.RemoveAt(3);

            DataGridDataPersonal.ItemsSource = _tablePersonal.DefaultView;
            GenerateInscriptionsDataTable();


        }

        private void GenerateInscriptionsDataTable() {
            _serviceComp = new CompetitionService();

            _tableInscripcion = _serviceComp.SelectAllCompetitionsInscripted(Dni.Text.ToUpper());
            _tableInscripcion.Columns[0].ColumnName = Properties.Resources.Competition;
            _tableInscripcion.Columns[1].ColumnName = Properties.Resources.Competition_Status;
            _tableInscripcion.Columns[2].ColumnName = Properties.Resources.Competition_Date;
            _tableInscripcion.Columns[3].ColumnName = Properties.Resources.AthleteDorsal;

            DataGridInscriptions.ItemsSource = _tableInscripcion.DefaultView;
            GenerateResultsDataTable();

        }

        private void GenerateResultsDataTable() {
            _serviceAthleteResult = new AthletesService();
            _tableResult = new DataTable();

            DataColumn column = new DataColumn(Properties.Resources.AthletePosition, typeof(string)) {
                AllowDBNull = true
            };
            _tableResult.Columns.Add(column);

            _tableResult.Columns[Properties.Resources.AthletePosition].AutoIncrement = true;
            _tableResult.Columns[Properties.Resources.AthletePosition].AutoIncrementSeed = 1;
            _tableResult.Columns[Properties.Resources.AthletePosition].AutoIncrementStep = 1;

            _tableResult.Merge(_serviceAthleteResult.SelectParticipatedByDni(Dni.Text.ToUpper()));



            _tableResult.Columns[1].ColumnName = Properties.Resources.FinishTime;
            _tableResult.Columns[2].ColumnName = Properties.Resources.Competition;
            _tableResult.Columns[3].ColumnName = Properties.Resources.AthleteGender;
            _tableResult.Columns.Add(Properties.Resources.Time, typeof(string));

            foreach (DataRow row in _tableResult.Rows) {
                row[4] = PartialTimeString(row[1] is long ? (long) row[1] : 0);
            }



            DataGridResults.ItemsSource = _tableResult.DefaultView;
            if (DataGridResults.Columns.Count >= 1) {
                DataGridResults.Columns.ElementAt(1).Visibility = Visibility.Collapsed;
                DataGridResults.Columns.ElementAt(3).Visibility = Visibility.Collapsed;
            }
        }

        private void CheckBox_OnClick(object sender, RoutedEventArgs e) {
            _tableResult.DefaultView.RowFilter = GenerateFilter();
            DataGridResults.ItemsSource = _tableResult.DefaultView;
        }

        private string GenerateFilter() {
            string filter = string.Empty;

            if (MaleIsChecked() && FemaleIsChecked()) {
                filter = null;
            } else if (MaleIsChecked()) {
                filter = $" {Properties.Resources.AthleteGender} = 'M'";
            } else if (FemaleIsChecked()) {
                filter = $"{Properties.Resources.AthleteGender}= 'F'";
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

        private bool ComprobarDNI(string dni) {
            if (!(dni.Length == 9))
                return false;

            for (int i = 0; i < 8; i++)
                if (!Char.IsDigit(dni[i]))
                    return false;

            if (!Char.IsLetter(dni[8]))
                return false;

            return true;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if (e.PropertyType == typeof(System.DateTime))
                ((DataGridTextColumn) e.Column).Binding.StringFormat = "dd/MM/yyyy";



        }

        private void OnMouseEnter(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = Cursors.Hand;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = null;
        }

        private void DataGridResults_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {

        }

        private static object PartialTimeString(long time) {
            if (time == 0) {
                return "---";
            } else {
                var seconds = time;
                var timespan = TimeSpan.FromSeconds(seconds);
                return timespan.ToString(@"hh\:mm\:ss");
            }
        }
    }
}

