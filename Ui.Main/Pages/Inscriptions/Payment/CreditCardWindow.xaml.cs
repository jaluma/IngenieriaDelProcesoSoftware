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
using Logic.Db.Dto;

namespace Ui.Main.Pages.Inscriptions.Payment
{
    /// <summary>
    /// Lógica de interacción para CreditCardWindow.xaml
    /// </summary>
    public partial class CreditCardWindow : Page
    {
        private AthleteDto _athlete;
        private CompetitionDto _competition;

        public CreditCardWindow(AthleteDto athlete, CompetitionDto competition)
        {
            InitializeComponent();

            _athlete = athlete;
            _competition = competition;
        }

        /*private void BtNext_Click(object sender, RoutedEventArgs e)
        {

            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                parentWindow.Content = new InscriptionProofWindow(_athlete, _competition);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }*/
    }
}
