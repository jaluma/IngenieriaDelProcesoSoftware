using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logic.Db.Dto;
using Logic.Db.Util.Services;

namespace Ui.Main.Pages.Inscriptions.InscriptionsPaidControl
{
    /// <summary>
    /// Lógica de interacción para FinishInscriptionPage.xaml
    /// </summary>
    public partial class FinishInscriptionPage : Page
    {
        private readonly EnrollService _enrollService;
        private string _file;

        public FinishInscriptionPage()
        {
            _enrollService = new EnrollService(null);
            InitializeComponent();
        }

        private void BtSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "*.csv|*.CSV";
            openFile.Multiselect = false;
            openFile.ShowDialog();
            _file = openFile.FileName;
            txFileName.Text = _file;
        }

        private List<string[]> LeerExtracto(string file)
        {
            List<string[]> list = new List<string[]>();
            using (StreamReader readFile = new StreamReader(file))
            {
                string line;
                string[] row = new string[5];

                while ((line = readFile.ReadLine()) != null)
                {
                    row = line.Split(',');
                    list.Add(row);
                }
            }
            if (list.Count == 0)
                throw new ArgumentException();

            return list;
        }

        private void BtActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (_file == null || _file.Equals(""))
            {
                System.Windows.MessageBox.Show(Properties.Resources.NotSelectedFile);
                return;
            }

            List<string[]> list = new List<string[]>();
            try
            {
                 list = LeerExtracto(_file);
            } catch (ArgumentException)
            {
                System.Windows.MessageBox.Show(Properties.Resources.EmptyDocument);
                return;
            }

            List<PaymentDto> extracto = new List<PaymentDto>();
            foreach (string[] s in list)
            {
                try
                {
                    PaymentDto dto = new PaymentDto()
                    {
                        Dni = s[0],
                        Name = s[1],
                        Surname = s[2],
                        Date = DateTime.Parse(s[3]),
                        Amount = float.Parse(s[4])
                    };
                    extracto.Add(dto);
                }catch (Exception)
                {
                    System.Windows.MessageBox.Show(Properties.Resources.InvalidDocument);
                    return;
                }

            }
            
            List<PaymentDto> preregistered = _enrollService.SelectPreregisteredAthletes();

            //comprobaciones
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PaymentDto prereg in preregistered)
            {
                float cantidadPagada = 0;
                bool pago = false;
                foreach (PaymentDto payment in extracto)
                {
                    if (prereg.Dni.Equals(payment.Dni))
                    {
                        pago = true;
                        TimeSpan time = payment.Date.Subtract(prereg.Date);
                        if (time.Days <= 2)
                            cantidadPagada += payment.Amount;
                    }
                }
                if (pago)
                {
                    if (cantidadPagada >= prereg.Amount)
                    {
                        _enrollService.UpdateInscriptionStatus(prereg.Dni, prereg.Id, "REGISTERED");
                        stringBuilder.Append("El atleta con dni " + prereg.Dni + " ha sido inscrito en la competición con ID " + prereg.Id + ".\n\n");
                    }
                    else
                    {
                        _enrollService.UpdateInscriptionStatus(prereg.Dni, prereg.Id, "CANCELED");
                        stringBuilder.Append("El atleta con dni " + prereg.Dni + " no ha realizado un pago válido.\n\n");
                    }
                }
            }

            TxActualizado.Text = stringBuilder.ToString();
        }
    }
}
