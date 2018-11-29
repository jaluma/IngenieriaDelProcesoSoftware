using System;
using System.Windows;
using System.Windows.Controls;
using Logic.Db.Dto;
using Logic.Db.Util.Services;
using Ui.Main.Pages.Inscriptions.Competitions;
using Ui.Main.Pages.MenuInitial;

namespace Ui.Main.Pages.Inscriptions
{
    /// <summary>
    ///     Lógica de interacción para InscriptionPage.xaml
    /// </summary>
    public partial class InscriptionPage : Page
    {
        private readonly AthletesService _athletesService;

        public InscriptionPage() {
            InitializeComponent();
            DPBirthDate.DisplayDateEnd = new DateTime(DateTime.Now.Year, 12, 31).AddYears(-18);
            _athletesService = new AthletesService();
        }

        private void BtNext_Click(object sender, RoutedEventArgs e) {
            if (TxName.Text == null || TxSurname.Text == null || TxDNI.Text == null ||
                DPBirthDate.SelectedDate == null) {
                MessageBox.Show(Properties.Resources.IncompleteFields);
                return;
            }

            if (!ComprobarDNI(TxDNI.Text)) {
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
                Dni = TxDNI.Text.ToUpper(),
                BirthDate = date
            };
            if ((bool) RBMasc.IsChecked)
                athlete.Gender = AthleteDto.MALE;
            else
                athlete.Gender = AthleteDto.FEMALE;

            if (_athletesService.CountAthleteByDni(athlete.Dni) == 0)
                _athletesService.InsertAthletesTable(athlete);

            CompetitionInscription.Dni = athlete.Dni;
            MainMenu.ChangeMenuSelected(Properties.Resources.TileAthletes,
                Properties.Resources.TileAthletesInscriptionCompetition);
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
    }
}
