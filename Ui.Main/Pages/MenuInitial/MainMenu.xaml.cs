using System.Windows.Controls;
using Ui.Main.Pages.Competition.Times;

namespace Ui.Main.Pages.MenuInitial {
    /// <summary>
    /// Lógica de interacción para MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page {
        public MainMenu() {
            InitializeComponent();
        }

        private void TileAthletes_Click(object sender, System.Windows.RoutedEventArgs e) {
            
        }

        private void TileCompetition_Click(object sender, System.Windows.RoutedEventArgs e) {

        }

        private void TileCompetitionFinish_Click(object sender, System.Windows.RoutedEventArgs e) {
            Content = new Frame() {
                Content = new SelectionCompetition()
            };

        }
    }
}
