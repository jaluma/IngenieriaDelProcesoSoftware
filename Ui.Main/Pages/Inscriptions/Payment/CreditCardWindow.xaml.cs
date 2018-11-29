using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Logic.Db.Dto;
using Logic.Db.Dto.Types;

namespace Ui.Main.Pages.Inscriptions.Payment
{
    /// <summary>
    ///     Lógica de interacción para CreditCardWindow.xaml
    /// </summary>
    public partial class CreditCardWindow : Page
    {
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;

        public CreditCardWindow(AthleteDto athlete, CompetitionDto competition) {
            InitializeComponent();

            _athlete = athlete;
            _competition = competition;
        }

        private void BtNext_Click(object sender, RoutedEventArgs e) {
            if (TxNumero.Text.Length == 0 || TxCaducidadMes.Text.Length == 0 || TxCaducidadYear.Text.Length == 0 ||
                TxCvc.Text.Length == 0) {
                MessageBox.Show(Properties.Resources.IncompleteFields);
                return;
            }

            if (!CheckNumber(TxNumero.Text)) {
                MessageBox.Show(Properties.Resources.InvalidNumber);
                return;
            }

            switch (CheckDate(TxCaducidadMes.Text, TxCaducidadYear.Text)) {
                case -1:
                    MessageBox.Show(Properties.Resources.ExpiredCard);
                    return;
                case 0:
                    break;
                case 1:
                    MessageBox.Show(Properties.Resources.InvalidDate);
                    return;
            }

            if (!CheckCvc(TxCvc.Text)) {
                MessageBox.Show(Properties.Resources.IncorrectCvc);
                return;
            }

            var parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
                parentWindow.Content = new InscriptionProofWindow(_athlete, _competition, TypesStatus.Registered);
        }

        private bool CheckCvc(string cvc) {
            if (cvc.Length != 3)
                return false;
            return true;
        }

        private int CheckDate(string mes, string year) {
            if (mes.Length != 2 || year.Length != 2)
                return 1;
            var m = int.Parse(mes);
            var y = int.Parse(year) + 2000;
            if (m < 1 || m > 12 || y < 1)
                return 1;
            var dt = new DateTime(y, m, DateTime.DaysInMonth(y, m));
            if (dt.Date > DateTime.Today.Date)
                return 0;
            return -1;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Window.GetWindow(this)?.Close();
        }

        public static bool CheckNumber(string creditCardNumber) {
            var digitsOnly = new StringBuilder();
            foreach (var c in creditCardNumber)
                if (char.IsDigit(c))
                    digitsOnly.Append(c);

            if (digitsOnly.Length > 18 || digitsOnly.Length < 15)
                return false;

            var sum = 0;
            var digit = 0;
            var addend = 0;
            var timesTwo = false;

            for (var i = digitsOnly.Length - 1; i >= 0; i--) {
                digit = int.Parse(digitsOnly.ToString(i, 1));
                if (timesTwo) {
                    addend = digit * 2;
                    if (addend > 9) addend -= 9;
                }
                else {
                    addend = digit;
                }

                sum += addend;
                timesTwo = !timesTwo;
            }

            return sum % 10 == 0;
        }

        private void NumerosTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
