using System.Windows;
using System.Windows.Controls;
using Logic.Db.Dto;

namespace Ui.Main.Pages.Inscriptions.Payment
{
    /// <summary>
    ///     Lógica de interacción para PaymentMethodSelection.xaml
    /// </summary>
    public partial class PaymentMethodSelectionWindow : Page
    {
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;

        public PaymentMethodSelectionWindow(AthleteDto athlete, CompetitionDto competition) {
            InitializeComponent();

            _athlete = athlete;
            _competition = competition;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if ((bool) RBWireTransfer.IsChecked) {
                var parentWindow = Window.GetWindow(this);
                if (parentWindow != null)
                    parentWindow.Content = new WireTransferWindow(_athlete, _competition);
            }

            if ((bool) RBCreditCard.IsChecked) {
                var parentWindow = Window.GetWindow(this);
                if (parentWindow != null)
                    parentWindow.Content = new CreditCardWindow(_athlete, _competition);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            Window.GetWindow(this)?.Close();
        }
    }
}
