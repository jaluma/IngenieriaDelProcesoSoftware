using FirstFloor.ModernUI.Windows.Controls;
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
    /// Interaction logic for DialogPayment.xaml
    /// </summary>
    public partial class DialogPayment : ModernDialog
    {
        public DialogPayment(AthleteDto athlete, CompetitionDto competition)
        {
            InitializeComponent();

            Buttons = null;


            Content = new PaymentMethodSelectionWindow(athlete, competition);
        }
    }
}
