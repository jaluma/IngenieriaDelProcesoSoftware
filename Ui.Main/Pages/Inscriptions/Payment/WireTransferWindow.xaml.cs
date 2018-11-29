using System.Windows;
using System.Windows.Controls;
using Logic.Db.Dto;

namespace Ui.Main.Pages.Inscriptions.Payment
{
    /// <summary>
    ///     Lógica de interacción para WireTransferWindow.xaml
    /// </summary>
    public partial class WireTransferWindow : Page
    {
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;

        public WireTransferWindow(AthleteDto athlete, CompetitionDto competition) {
            InitializeComponent();

            _athlete = athlete;
            _competition = competition;

            var cont = Properties.Resources.PaymentAmount + " " + _competition.Price + " €";
            LbPaymentAmount.Content = cont;
        }

        private void BtNext_Click(object sender, RoutedEventArgs e) {
            var parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                parentWindow.Content = new InscriptionProofWindow(_athlete, _competition, TypesStatus.Outstanding);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Window.GetWindow(this)?.Close();
        }
    }
}
