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

        private void BtNext_Click(object sender, RoutedEventArgs e)
        {
            if (TxNumero.Text == null || TxCaducidadMes.Text == null || TxCaducidadYear.Text == null || TxCvc.Text == null)
            {
                MessageBox.Show(Properties.Resources.IncompleteFields);
                return;
            }
            if (!CheckNumber(TxNumero.Text))
            {
                MessageBox.Show(Properties.Resources.InvalidNumber);
                return;
            }
            if (!CheckDate(TxCaducidadMes.Text, TxCaducidadYear.Text))
            {
                MessageBox.Show(Properties.Resources.ExpiredCard);
                return;
            }
            if (!CheckCvc(TxCvc.Text))
            {
                MessageBox.Show(Properties.Resources.IncorrectCvc);
                return;
            }

            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                parentWindow.Content = new InscriptionProofWindow(_athlete, _competition, "REGISTERED");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)?.Close();
        }

        public static bool CheckNumber(string creditCardNumber)
        {
            StringBuilder digitsOnly = new StringBuilder();
            foreach (char c in creditCardNumber)
            {
                if (Char.IsDigit(c))
                    digitsOnly.Append(c);
            }

            if (digitsOnly.Length > 18 || digitsOnly.Length < 15)
                return false;

            int sum = 0;
            int digit = 0;
            int addend = 0;
            bool timesTwo = false;

            for (int i = digitsOnly.Length - 1; i >= 0; i--)
            {
                digit = Int32.Parse(digitsOnly.ToString(i, 1));
                if (timesTwo)
                {
                    addend = digit * 2;
                    if (addend > 9)
                    {
                        addend -= 9;
                    }
                }
                else
                {
                    addend = digit;
                }
                sum += addend;
                timesTwo = !timesTwo;
            }
            return (sum % 10) == 0;
        }
    }
}
