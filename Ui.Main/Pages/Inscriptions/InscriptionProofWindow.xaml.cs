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
using System.Windows.Shapes;
using Logic.Db.Dto;
using Logic.Db.Util.Services;

namespace Ui.Main.Pages.Inscriptions
{
    /// <summary>
    /// Lógica de interacción para InscriptionProofWindow.xaml
    /// </summary>
    public partial class InscriptionProofWindow : Window
    {
        private readonly CompetitionService _competitionService;
        private readonly EnrollService _enrollService;
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;
      

        public InscriptionProofWindow(AthleteDto athlete, CompetitionDto competition)
        {
            InitializeComponent();

            _athlete = athlete;

            _competitionService = new CompetitionService();
            _enrollService = new EnrollService(_competition);

            _competition = _competitionService.SearchCompetitionById(competition);

            string category = _enrollService.GetCategory(_athlete, _competition);

            TxJustificante.Text = "Atleta: " + _athlete.Name + " " + _athlete.Surname + "\nCompetición: " + _competition.Name +
                "\nCategoría: " + category + "\nFecha de inscripción: " + DateTime.Now.ToShortDateString() + "\nCantidad a abonar: " +_competition.Price + " €";
        }
    }
}
