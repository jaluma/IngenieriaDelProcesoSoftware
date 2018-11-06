using Logic.Db.Dto;
using Logic.Db.Util.Services;
using System;
using System.Collections.Generic;
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

namespace Ui.Main.Pages.Inscriptions
{
    /// <summary>
    /// Lógica de interacción para CancelInscriptionPage.xaml
    /// </summary>
    public partial class CancelInscriptionPage : Page
    {
        public static string Dni;

        private readonly CompetitionService _competitionService;
        private readonly AthletesService _athletesService;
        private AthleteDto _athlete;

        private CompetitionDto _competition;

        public CancelInscriptionPage()
        {
            _competitionService = new CompetitionService();
            _athletesService = new AthletesService();
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
            List<AthleteDto> atleList = _athletesService.SelectAthleteTable();

            if (dni != null)
            {
                try
                {
                    _athlete = atleList.First(a => a.Dni.ToUpper().Equals(dni.ToUpper()));

                    PlaceData();

                    //GetListCompetition();

                }
                catch (InvalidOperationException) { }
            }

        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                ((DataGridTextColumn)e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void TxDni_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData(TxDni.Text);
        }

        private void CancelInscription_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_athlete == null || _athlete.Dni == null || Dni == null || !Dni.Equals(_athlete.Dni))
            {
                LoadData(Dni);
            }
        }

        private void CompetitionsToCancel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
