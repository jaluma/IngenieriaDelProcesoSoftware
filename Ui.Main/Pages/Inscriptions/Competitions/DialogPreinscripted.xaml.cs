using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows.Controls;
using Logic.Db.Dto;

namespace Ui.Main.Pages.Inscriptions.Competitions
{
    /// <summary>
    ///     Interaction logic for DialogPayment.xaml
    /// </summary>
    public partial class DialogPreinscripted : ModernDialog
    {
        public DialogPreinscripted(AthleteDto athlete, CompetitionDto competition) {
            InitializeComponent();

            var customButton = new Button {Content = Properties.Resources.Close, Margin = new Thickness(4)};
            customButton.Click += (ss, ee) => { Close(); };
            Buttons = new[] {customButton};

            LbConfirmation.Text = $"El atleta {athlete.Name} se ha preregistrado correctamente en {competition.Name}.";
        }
    }
}
