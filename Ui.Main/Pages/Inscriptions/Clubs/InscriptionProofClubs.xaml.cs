using System.Windows;
using System.Windows.Controls;
using Logic.Db.Dto;

namespace Ui.Main.Pages.Inscriptions.Clubs
{
    /// <summary>
    ///     Lógica de interacción para InscriptionProofWindow.xaml
    /// </summary>
    public partial class InscriptionProofClubs : Page
    {
        private readonly CompetitionDto _competition;


        public InscriptionProofClubs(CompetitionDto competition, string atletas, int nuevos) {
            _competition = competition;

            InitializeComponent();

            TxJustificante.Text = "Competición: " + _competition.Name + ".\n\n" + atletas + "\nSe han inscrito " +
                                  nuevos + " atletas.\n\nPrecio de la inscripción: " + _competition.Price * nuevos +
                                  "€";
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Window.GetWindow(this)?.Close();
        }
    }
}
