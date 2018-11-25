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
using FirstFloor.ModernUI.Windows.Controls;
using Logic.Db.Dto;
using Logic.Db.Util.Services;

namespace Ui.Main.Pages.Inscriptions {
    /// <summary>
    /// Lógica de interacción para InscriptionProofWindow.xaml
    /// </summary>
    public partial class InscriptionProofClubs : Page {
        private readonly string _athletes;
        private readonly CompetitionDto _competition;


        public InscriptionProofClubs(CompetitionDto competition, string atletas, int nuevos) {
            String _athletes = atletas;
            _competition = competition;

            InitializeComponent();

            TxJustificante.Text = "\n" + atletas + "Se han inscrito " + nuevos + " atletas.\nPrecio de la inscripción: " + (_competition.Price * nuevos);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e) {

            Window.GetWindow(this)?.Close();
        }
    }
}
