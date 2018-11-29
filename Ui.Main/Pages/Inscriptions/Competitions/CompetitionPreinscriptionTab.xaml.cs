using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Logic.Db.Dto;
using Logic.Db.Util.Services;
using Ui.Main.Pages.Inscriptions.Payment;

namespace Ui.Main.Pages.Inscriptions.Competitions
{
    /// <summary>
    /// Lógica de interacción para CompetitionSelectionPage.xaml
    /// </summary>
    public partial class CompetitionPreinscriptionTab : UserControl
    {

        private readonly CompetitionService _competitionService;
        private readonly AthletesService _athletesService;
        private readonly EnrollService _enrollService;
        private AthleteDto _athlete;

        private CompetitionDto _competition;

        private List<long> _columnIds;

        public CompetitionPreinscriptionTab()
        {
            _competitionService = new CompetitionService();
            _athletesService = new AthletesService();
            _enrollService = new EnrollService(null);
            InitializeComponent();
        }

        private void PlaceData()
        {
            TxDni.Text = _athlete.Dni;
            LbNameSurname.Content = _athlete.Name + " " + _athlete.Surname;
            LbBirthDate.Content = _athlete.BirthDate.ToShortDateString();
            if (_athlete.Gender == AthleteDto.MALE)
                LbGender.Content = Properties.Resources.Man;
            else
                LbGender.Content = Properties.Resources.Woman;
        }

        private void LoadData(string dni)
        {
            if (dni != null)
            {
                try
                {
                    List<AthleteDto> atleList = _athletesService.SelectAthleteTable();

                    _athlete = atleList.First(a => a.Dni.ToUpper().Equals(dni.ToUpper()));

                    PlaceData();

                    GetListCompetition();

                }
                catch (InvalidOperationException) { }
            }

        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                ((DataGridTextColumn)e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void BtFinish_Click(object sender, RoutedEventArgs e)
        {
            if (CompetitionsToSelect.SelectedItem == null)
            {
                MessageBox.Show(Properties.Resources.NothingSelected);
                return;
            }

            _competition = _competitionService.ListCompetitionsToPreinscribeObject(_athlete)
                .ElementAt(CompetitionsToSelect.SelectedIndex);

            if (_enrollService.IsAthleteInComp(_competition, _athlete))
            {
                MessageBox.Show(Properties.Resources.PreviouslyEnrolled);
                return;
            }
            new DialogPreinscripted(_athlete, _competition).ShowDialog();

            CompetitionService competitionService = new CompetitionService();
            EnrollService enrollService = new EnrollService(_competition);

            string category = _enrollService.GetCategory(_athlete, _competition);

            _enrollService.InsertAthleteInCompetition(_athlete, _competition, TypesStatus.PreRegistered);
            LoadData(TxDni.Text);

        }

        private void CompetitionsToSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexSeletected = CompetitionsToSelect.SelectedIndex;

            if (indexSeletected != -1)
            {
                _competition = new CompetitionDto()
                {
                    ID = (int)_columnIds[indexSeletected]
                };
            }

        }

        private void GetListCompetition()
        {
            DataTable table = _competitionService.ListCompetitionsToPreinscribe(_athlete);
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

        private void TxDni_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData(TxDni.Text);
            CompetitionInscription.Dni = TxDni.Text;
        }

        private void CompetitionSelectionWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_athlete?.Dni == null || CompetitionInscription.Dni == null || !CompetitionInscription.Dni.Equals(_athlete.Dni))
            {
                LoadData(CompetitionInscription.Dni);
            }
        }

        private void CompetitionsToSelect_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - e.Delta);
        }
    }
}
