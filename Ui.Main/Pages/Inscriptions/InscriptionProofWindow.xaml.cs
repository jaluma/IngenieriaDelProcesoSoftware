using System;
using System.Windows;
using System.Windows.Controls;
using Logic.Db.Dto;
using Logic.Db.Dto.Types;
using Logic.Db.Util.Services;

namespace Ui.Main.Pages.Inscriptions
{
    /// <summary>
    ///     Lógica de interacción para InscriptionProofWindow.xaml
    /// </summary>
    public partial class InscriptionProofWindow : Page
    {
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;
        private readonly CompetitionService _competitionService;
        private readonly EnrollService _enrollService;


        public InscriptionProofWindow(AthleteDto athlete, CompetitionDto competition, TypesStatus status) {
            _athlete = athlete;
            _competition = competition;

            InitializeComponent();

            _competitionService = new CompetitionService();
            _enrollService = new EnrollService(_competition);

            var category = _enrollService.GetCategory(_athlete, _competition);

            _enrollService.InsertAthleteInCompetition(_athlete, _competition, status);

            TxJustificante.Text =
                $"Atleta: {_athlete.Name} {_athlete.Surname}\nCompetición: {_competition.Name}\nCategoría: {category}\nFecha de inscripción: {DateTime.Now.ToShortDateString()}\nPrecio de la inscripción: {_competition.Price} €";
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Window.GetWindow(this)?.Close();
        }
    }
}
