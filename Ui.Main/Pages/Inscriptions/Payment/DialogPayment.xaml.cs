using FirstFloor.ModernUI.Windows.Controls;
using Logic.Db.Dto;

namespace Ui.Main.Pages.Inscriptions.Payment
{
    /// <summary>
    ///     Interaction logic for DialogPayment.xaml
    /// </summary>
    public partial class DialogPayment : ModernDialog
    {
        public DialogPayment(AthleteDto athlete, CompetitionDto competition) {
            InitializeComponent();

            Buttons = null;


            Content = new PaymentMethodSelectionWindow(athlete, competition);
        }
    }
}
