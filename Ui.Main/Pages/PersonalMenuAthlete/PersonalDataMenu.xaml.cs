using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Logic.Db.Dto;
using Logic.Db.Util.Services;
using Ui.Main.Pages.Competition.Times;
using Ui.Main.Pages.MenuInitial;

namespace Ui.Main.Pages.PersonalMenuAthlete
{
    /// <summary>
    ///     Lógica de interacción para PersonalDataMenu.xaml
    /// </summary>
    public partial class PersonalDataMenu : Page
    {
        public static CompetitionDto Competition = new CompetitionDto();


        private AthletesService _serviceAthlete;
        private AthletesService _serviceAthleteResult;
        private CompetitionService _serviceComp;
        private DataTable _tableCat;
        private DataTable _tableInscripcion;
        private DataTable _tablePersonal;
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
            var table = _serviceAthlete.SelectAthleteByDni(Dni.Text.ToUpper());
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


            while (_tablePersonal.Rows.Count > 1) _tablePersonal.Rows.RemoveAt(_tablePersonal.Rows.Count - 1);

            _tablePersonal.Columns.RemoveAt(0);
            _tablePersonal.Columns.RemoveAt(2);
            _tablePersonal.Columns.RemoveAt(2);

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
            _tableResult = _serviceAthleteResult.SelectParticipatedByDni(Dni.Text.ToUpper());

            _tableResult.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            _tableResult.Columns[1].ColumnName = Properties.Resources.FinishTime;
            _tableResult.Columns[2].ColumnName = Properties.Resources.Competition;
            _tableResult.Columns[3].ColumnName = Properties.Resources.AthleteGender;
            _tableResult.Columns.Add(Properties.Resources.Time + "(s)", typeof(string));

            _tableResult.Columns.RemoveAt(3);

            foreach (DataRow row in _tableResult.Rows) row[3] = PartialTimeString(row[1] is long ? (long) row[1] : 0);

            _tableResult.Columns.RemoveAt(1);

            DataGridResults.ItemsSource = _tableResult.DefaultView;

            if (DataGridResults.Columns.Count >= 1)
                DataGridResults.Columns.ElementAt(0).Visibility = Visibility.Collapsed;

            GenerateCategoriasDataTable();
        }


        private void GenerateCategoriasDataTable() {
            _serviceAthleteResult = new AthletesService();
            _tableCat = new DataTable();
            _tableCat = _serviceAthleteResult.SelectParticipatedByDni(Dni.Text.ToUpper());

            _tableCat.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            _tableCat.Columns[1].ColumnName = Properties.Resources.FinishTime;
            _tableCat.Columns[2].ColumnName = Properties.Resources.Competition;
            _tableCat.Columns[3].ColumnName = Properties.Resources.AthleteGender;


            _tableCat.Columns.RemoveAt(3);

            _tableCat.Columns.RemoveAt(1);

            DataGridPosiciones.ItemsSource = _tableCat.DefaultView;

            if (DataGridPosiciones.Columns.Count >= 1)
                DataGridPosiciones.Columns.ElementAt(0).Visibility = Visibility.Collapsed;
        }


        private bool ComprobarDNI(string dni) {
            if (!(dni.Length == 9))
                return false;

            for (var i = 0; i < 8; i++)
                if (!char.IsDigit(dni[i]))
                    return false;

            if (!char.IsLetter(dni[8]))
                return false;

            return true;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if (e.PropertyType == typeof(DateTime))
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

        private void DataGridResults_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) { }

        private static object PartialTimeString(long time) {
            if (time == 0) {
                return "---";
            }

            var seconds = time;
            var timespan = TimeSpan.FromSeconds(seconds);
            return timespan.ToString(@"hh\:mm\:ss");
        }

        private void DataGridInscriptions_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        private void DataGridDataPersonal_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        private void DataGridResults_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var id = _tableResult.Rows[DataGridResults.SelectedIndex].ItemArray.ElementAt(0).ToString();
            Competition.ID = long.Parse(id);
            PartialTimesAthletes.Competition = Competition;


            var atleList = new AthletesService().SelectAthleteTable();

            try {
                PartialTimesAthletes.Athlete = atleList.First(a => a.Dni.ToUpper().Equals(Dni.Text.ToUpper()));


                MainMenu.ChangeMenuSelected(Properties.Resources.TileTimes, Properties.Resources.SubMenuPartialTimes);
            }
            catch (IndexOutOfRangeException) { }
        }

        private void DataGridPosiciones_SelectionChanged(object sender, SelectionChangedEventArgs e) { }

        private void DataGridPosiciones_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var id = _tableCat.Rows[DataGridPosiciones.SelectedIndex].ItemArray.ElementAt(0).ToString();
            Competition.ID = long.Parse(id);
            TimesAthletes.Competition = Competition;


            var atleList = new AthletesService().SelectAthleteTable();

            try {
                TimesAthletes.Athlete = atleList.First(a => a.Dni.ToUpper().Equals(Dni.Text.ToUpper()));


                MainMenu.ChangeMenuSelected(Properties.Resources.TileTimes, Properties.Resources.TitleTimesCompetition);
            }
            catch (IndexOutOfRangeException) { }
        }
    }
}
