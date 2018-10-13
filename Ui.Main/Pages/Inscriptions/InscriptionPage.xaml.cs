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
using Ui.Main.Pages.MenuInitial;

namespace Ui.Main.Pages.Inscriptions
{
    /// <summary>
    /// Lógica de interacción para InscriptionPage.xaml
    /// </summary>
    public partial class InscriptionPage : Page
    {
        private readonly AthletesService _athletesService;

        public InscriptionPage()
        {
            InitializeComponent();

            _athletesService = new AthletesService();
        }

        private void BtNext_Click(object sender, RoutedEventArgs e)
        {
            if (TxName.Text == null || TxSurname.Text == null || TxDNI.Text == null || DPBirthDate.SelectedDate == null)
            {
                MessageBox.Show(Properties.Resources.IncompleteFields);
                return;
            }
            
            AthleteDto athlete = new AthleteDto
            {
                Name = TxName.Text,
                Surname = TxSurname.Text,
                Dni = TxDNI.Text,
                BirthDate = (DateTime)DPBirthDate.SelectedDate
            };
            if ((bool)RBMasc.IsChecked)
                athlete.Gender = Gender.Male;
            else
                athlete.Gender = Gender.Female;

            //if (_athletesService.CountAthleteByDni(athlete.Dni) == 0)
                _athletesService.InsertAthletesTable(athlete);

            CompetitionSelectionWindow.Dni = athlete.Dni;
            MainMenu.ChangeMenuSelected(Properties.Resources.TileAthletes, Properties.Resources.TileAthletesInscriptionCompetition);
        }
    }
}
