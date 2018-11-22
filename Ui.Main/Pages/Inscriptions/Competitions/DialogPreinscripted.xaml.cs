using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows.Controls;
using Logic.Db.Dto;
using Ui.Main.Pages.Inscriptions.Payment;

namespace Ui.Main.Pages.Inscriptions.Competitions {
    /// <summary>
    /// Interaction logic for DialogPayment.xaml
    /// </summary>
    public partial class DialogPreinscripted : ModernDialog {
        public DialogPreinscripted(AthleteDto athlete, CompetitionDto competition) {
            InitializeComponent();

            Button customButton = new Button() { Content = Properties.Resources.Close, Margin = new Thickness(4) };
            customButton.Click += (ss, ee) => { this.Close(); };
            Buttons = new Button[] { customButton };

            LbConfirmation.Text = $"El atleta {athlete.Name} se ha preregistrado correctamente en {competition.Name}.";
        }
    }
}
