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
            if (TxNumero.Text.Length == 0 || TxCaducidadMes.Text.Length == 0 || TxCaducidadYear.Text.Length == 0 || TxCvc.Text.Length == 0)
            {
                MessageBox.Show(Properties.Resources.IncompleteFields);
                return;
            }
            if (!CheckNumber(TxNumero.Text))
            {
                MessageBox.Show(Properties.Resources.InvalidNumber);
                return;
            }
            switch (CheckDate(TxCaducidadMes.Text, TxCaducidadYear.Text))
            {
                case -1:
                    MessageBox.Show(Properties.Resources.ExpiredCard);
                    return;
                case 0:
                    break;
                case 1:
                    MessageBox.Show(Properties.Resources.InvalidDate);
                    return;
                default:
                    break;
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

        private bool CheckCvc(string cvc)
        {
            if (cvc.Length != 3)
                return false;
            foreach (char c in cvc)
                if (!Char.IsDigit(c))
                    return false;
            return true;
        }

        private int CheckDate(string mes, string year)
        {
            if (mes.Length != 2 || year.Length != 2)
                return 1;
            foreach (char c in mes)
                if (!Char.IsDigit(c))
                    return 1;
            foreach (char c in year)
                if (!Char.IsDigit(c))
                    return 1;
            int m = int.Parse(mes);
            int y = int.Parse(year) + 2000;
            if (m < 1 || m > 12 || y < 1)
                return 1;
            DateTime dt = new DateTime(y, m, DateTime.DaysInMonth(y, m));
            if (dt.Date > DateTime.Today.Date)
                return 0;
            return -1;
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

        private void OnlyNumber_KeyPress(object sender, TextChangedEventArgs e)
        {

        }
    }
}
