using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Logic.Db.Dto;
using Logic.Db.Util.Services;
using Control = System.Windows.Controls.Control;
using Cursors = System.Windows.Input.Cursors;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace Ui.Main.Pages.Inscriptions.PaymentControl
{
    /// <summary>
    ///     Lógica de interacción para FinishInscriptionPage.xaml
    /// </summary>
    public partial class FinishInscriptionPage : Page
    {
        private readonly EnrollService _enrollService;
        private string _file;

        public FinishInscriptionPage() {
            _enrollService = new EnrollService(null);
            InitializeComponent();
        }

        private void BtSeleccionar_Click(object sender, RoutedEventArgs e) {
            var openFile = new OpenFileDialog {
                Filter = @"*.csv|*.CSV",
                Multiselect = false
            };
            openFile.ShowDialog();
            _file = openFile.FileName;
            txFileName.Text = _file;
        }

        private List<string[]> LeerExtracto(string file) {
            var list = new List<string[]>();
            using (var readFile = new StreamReader(file)) {
                string line;
                var row = new string[4];

                while ((line = readFile.ReadLine()) != null) {
                    row = line.Split(',');
                    list.Add(row);
                }
            }

            if (list.Count == 0)
                throw new ArgumentException();

            return list;
        }

        private void BtActualizar_Click(object sender, RoutedEventArgs e) {
            if (_file == null || _file.Equals("")) {
                MessageBox.Show(Properties.Resources.NotSelectedFile);
                return;
            }

            var list = new List<string[]>();
            try {
                list = LeerExtracto(_file);
            }
            catch (ArgumentException) {
                MessageBox.Show(Properties.Resources.EmptyDocument);
                return;
            }

            var extracto = new List<PaymentDto>();
            foreach (var s in list)
                try {
                    var dto = new PaymentDto {
                        Dni = s[0],
                        Date = DateTime.Parse(s[1]),
                        Amount = double.Parse(s[2]),
                        Id = int.Parse(s[3])
                    };
                    extracto.Add(dto);
                }
                catch (Exception) {
                    MessageBox.Show(Properties.Resources.InvalidDocument);
                    return;
                }

            var preregistered = _enrollService.SelectOutstandingAthletes();

            //comprobaciones
            var stringBuilder = new StringBuilder();
            foreach (var payment in extracto) {
                var cantidadPagada = payment.Amount;
                var outstandingInCompetition = false;
                foreach (var prereg in preregistered)
                    if (prereg.Dni.Equals(payment.Dni) && prereg.Id == payment.Id) {
                        outstandingInCompetition = true;
                        var time = payment.Date.Subtract(prereg.Date);
                        if (time.Days <= 2 && cantidadPagada >= prereg.Amount) {
                            _enrollService.UpdateInscriptionStatus(prereg.Dni, prereg.Id, "REGISTERED");
                            stringBuilder.Append("El atleta con dni " + prereg.Dni +
                                                 " ha sido inscrito en la competición con ID " + prereg.Id);
                            if (cantidadPagada > prereg.Amount) {
                                _enrollService.UpdateRefund(prereg.Dni, prereg.Id, prereg.Amount - cantidadPagada);
                                stringBuilder.Append(". Deben devolversele " + (cantidadPagada - prereg.Amount) +
                                                     "€ por abonar una cantidad superior al precio");
                            }

                            stringBuilder.Append(".\n\n");
                        }
                        else {
                            _enrollService.UpdateInscriptionStatus(prereg.Dni, prereg.Id, "CANCELED");
                            _enrollService.UpdateRefund(prereg.Dni, prereg.Id, cantidadPagada);
                            stringBuilder.Append("El atleta con dni " + prereg.Dni +
                                                 " no ha realizado un pago válido para la competición con ID " +
                                                 prereg.Id +
                                                 ". Deben devolversele " + cantidadPagada + "€.\n\n");
                        }
                    }

                if (!outstandingInCompetition)
                    stringBuilder.Append("El atleta con dni " + payment.Dni +
                                         " no tenía un pago pendiente en la competición para la que ha realizado el pago. Deben devolversele " +
                                         cantidadPagada + "€.\n\n");
            }

            TxActualizado.Text = stringBuilder.ToString();
        }

        private void OnMouseEnter(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = Cursors.Hand;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = null;
        }
    }
}
