using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Logic.Db.Dto;
using Logic.Db.Dto.Types;
using Logic.Db.Util.Services;
using Ui.Main.Pages.Inscriptions.Payment;

namespace Ui.Main.Pages.Inscriptions.Clubs
{
    /// <summary>
    ///     Lógica de interacción para ClubInscriptionFormTab.xaml
    /// </summary>
    public partial class ClubInscriptionFormTab : UserControl
    {
        private readonly AthletesService _athletesService;
        private readonly CompetitionService _competitionService;
        private readonly EnrollService _enrollService;

        private readonly List<AthleteDto> _athletes;

        private List<long> _columnIds;
        private CompetitionDto _competition;
        private int _count;

        private readonly StringBuilder _stringBuilder;

        public ClubInscriptionFormTab() {
            InitializeComponent();
            DPBirthDate.DisplayDateEnd = new DateTime(DateTime.Now.Year, 12, 31).AddYears(-18);
            _athletesService = new AthletesService();
            _competitionService = new CompetitionService();
            _enrollService = new EnrollService(null);
            _athletes = new List<AthleteDto>();
            _count = 0;
            _stringBuilder = new StringBuilder();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (TxName.Text == null || TxSurname.Text == null || TxDni.Text == null ||
                DPBirthDate.SelectedDate == null) {
                MessageBox.Show(Properties.Resources.IncompleteFields);
                return;
            }

            if (!ComprobarDNI(TxDni.Text)) {
                MessageBox.Show(Properties.Resources.InvalidDNI);
                return;
            }

            var date = (DateTime) DPBirthDate.SelectedDate;

            if (DateTime.Now.Year - date.Year < 18 || DateTime.Now.Year - date.Year > 100) {
                MessageBox.Show(Properties.Resources.InvalidAge);
                return;
            }

            var athlete = new AthleteDto {
                Name = TxName.Text,
                Surname = TxSurname.Text,
                Dni = TxDni.Text.ToUpper(),
                BirthDate = date
            };

            if ((bool) RbMasc.IsChecked)
                athlete.Gender = AthleteDto.MALE;
            else
                athlete.Gender = AthleteDto.FEMALE;

            if (_athletesService.CountAthleteByDni(athlete.Dni) != 0) {
                _stringBuilder.Append(athlete.Dni + " registrado anteriormente.\n\n");
                _athletes.Add(athlete);
            }
            else {
                _stringBuilder.Append(athlete.Dni + " registrado correctamente.\n\n");
                _athletes.Add(athlete);
                _athletesService.InsertAthletesTable(athlete);
            }

            TxFormIns.Text = _stringBuilder.ToString();
            TxName.Text = "";
            TxSurname.Text = "";
            TxDni.Text = "";
            DPBirthDate.SelectedDate = null;
            RbMasc.IsChecked = true;
            GetListCompetition();
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

        private void GetListCompetition() {
            var table = _competitionService.ListCompetitionsToInscribeClubs(_athletes.Count);
            table.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Name;
            table.Columns[2].ColumnName = Properties.Resources.Competition_Type;
            table.Columns[3].ColumnName = Properties.Resources.Competition_Km;
            table.Columns[4].ColumnName = Properties.Resources.Competition_Price;
            table.Columns[5].ColumnName = Properties.Resources.InscriptionOpen;
            table.Columns[6].ColumnName = Properties.Resources.InscriptionClose;
            table.Columns[7].ColumnName = Properties.Resources.Competition_Date;

            _columnIds = table.AsEnumerable()
                .Select(dr => dr.Field<long>(Properties.Resources.Competition_Id)).ToList();

            table.Columns.RemoveAt(0);
            table.Columns.Remove(Properties.Resources.InscriptionOpen);

            if (CompetitionsToSelect != null)
                CompetitionsToSelect.ItemsSource = table.DefaultView;
        }

        private void BtFinish_Click(object sender, RoutedEventArgs e) {
            if (CompetitionsToSelect.SelectedItem == null) {
                MessageBox.Show(Properties.Resources.NothingSelected);
                return;
            }

            var dto = new CompetitionDto {
                ID = _columnIds[CompetitionsToSelect.SelectedIndex]
            };
            _competition = _competitionService.SearchCompetitionById(dto);

            var stringBuilder = new StringBuilder();
            foreach (var a in _athletes)
                if (_enrollService.IsAthleteInComp(_competition, a)) {
                    stringBuilder.Append(a.Dni + " inscrito anteriormente en la competición.\n");
                }
                else {
                    _enrollService.InsertAthleteInCompetition(a, _competition, TypesStatus.Registered);
                    _count++;
                    stringBuilder.Append(a.Dni + " inscrito correctamente.\n");
                }


            var dialog = new DialogPayment(null, null);
            dialog.Content = new InscriptionProofClubs(_competition, stringBuilder.ToString(), _count);
            dialog.Show();

            _count = 0;
            GetListCompetition();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if (e.PropertyType == typeof(DateTime))
                ((DataGridTextColumn) e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }


        private void CompetitionsToSelect_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer2.ScrollToVerticalOffset(ScrollViewer2.VerticalOffset - e.Delta);
        }

        private void CompetitionsToSelect_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var indexSeletected = CompetitionsToSelect.SelectedIndex;

            if (indexSeletected != -1)
                _competition = new CompetitionDto {
                    ID = (int) _columnIds[indexSeletected]
                };
        }
    }
}
