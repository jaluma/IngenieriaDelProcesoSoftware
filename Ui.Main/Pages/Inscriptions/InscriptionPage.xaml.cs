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

            if (!ComprobarDNI(TxDNI.Text))
            {
                MessageBox.Show(Properties.Resources.InvalidDNI);
                return;
            }

            DateTime date = (DateTime)DPBirthDate.SelectedDate;

            if (DateTime.Now.Year - date.Year < 18 || DateTime.Now.Year - date.Year > 100)
            {
                MessageBox.Show(Properties.Resources.InvalidAge);
                return;
            }

            AthleteDto athlete = new AthleteDto
            {
                Name = TxName.Text,
                Surname = TxSurname.Text,
                Dni = TxDNI.Text.ToUpper(),
                BirthDate = date
            };
            if ((bool)RBMasc.IsChecked)
                athlete.Gender = AthleteDto.MALE;
            else
                athlete.Gender = AthleteDto.FEMALE;

            if (_athletesService.CountAthleteByDni(athlete.Dni) == 0)
                _athletesService.InsertAthletesTable(athlete);

            CompetitionInscription.Dni = athlete.Dni;
            MainMenu.ChangeMenuSelected(Properties.Resources.TileAthletes, Properties.Resources.TileAthletesInscriptionCompetition);
        }

        private bool ComprobarDNI(string dni)
        {
            if (! (dni.Length == 9))
                return false;

            for (int i = 0; i < 8; i++)
                if (!Char.IsDigit(dni[i]))
                    return false;

            if (!Char.IsLetter(dni[8]))
                return false;

            return true;
        }
    }
}
