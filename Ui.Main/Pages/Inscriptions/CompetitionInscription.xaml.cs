using Logic.Db.Dto;
using Logic.Db.Util.Services;
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
using Ui.Main.Pages.Inscriptions.Payment;

namespace Ui.Main.Pages.Inscriptions
{
    /// <summary>
    /// Lógica de interacción para CompetitionSelectionPage.xaml
    /// </summary>
    public partial class CompetitionInscription : Page {
        public static string Dni;

        private readonly CompetitionService _competitionService;
        private readonly AthletesService _athletesService;
        private AthleteDto _athlete;

        private CompetitionDto _competition;

        private List<long> _columnIds;

        public CompetitionInscription()
        {
            _competitionService = new CompetitionService();
            _athletesService = new AthletesService();
            InitializeComponent();
        }

        private void PlaceData() {
            TxDni.Text = _athlete.Dni;
            LbNameSurname.Content = _athlete.Name + " " + _athlete.Surname;
            LbBirthDate.Content = _athlete.BirthDate.ToShortDateString();
            if (_athlete.Gender == AthleteDto.MALE)
                LbGender.Content = Properties.Resources.Man;
            else
                LbGender.Content = Properties.Resources.Woman;
        }

        private void LoadData(string dni) {
            List<AthleteDto> atleList = _athletesService.SelectAthleteTable();

            try {
                _athlete = atleList.First(a => a.Dni.Equals(dni));

                PlaceData();

                GetListCompetition();

            } catch(InvalidOperationException) { }
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
            EnrollService enrollService = new EnrollService(_competition);
            try
            {
                enrollService.InsertAthleteInCompetition(_athlete, _competition);

                //new InscriptionProofWindow(_athlete, _competition).ShowDialog();
                new DialogPayment(_athlete, _competition).ShowDialog();

            } catch (ApplicationException)
            {
                MessageBox.Show(Properties.Resources.PreviouslyEnrolled);
            }
            
        }

        private void CompetitionsToSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexSeletected = CompetitionsToSelect.SelectedIndex;
            
            _competition = new CompetitionDto()
            {
                ID = (int)_columnIds[indexSeletected]
            };
        }

        private void GetListCompetition() {
            DataTable table = _competitionService.ListCompetitionsToInscribe();
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

            if (CompetitionsToSelect != null)
                CompetitionsToSelect.ItemsSource = table.DefaultView;
        }

        private void TxDni_TextChanged(object sender, TextChangedEventArgs e) {
            LoadData(TxDni.Text);
        }

        private void CompetitionSelectionWindow_OnLoaded(object sender, RoutedEventArgs e) {
            if (_athlete == null || _athlete.Dni == null || !Dni.Equals(_athlete.Dni)) {
                LoadData(Dni);
            }
        }
    }
}
