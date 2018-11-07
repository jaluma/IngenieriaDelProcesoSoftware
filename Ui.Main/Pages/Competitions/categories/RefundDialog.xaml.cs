using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;
using Logic.Db.Dto;


namespace Ui.Main.Pages.Competitions.categories
{
    /// <summary>
    /// Lógica de interacción para RefundDialog.xaml
    /// </summary>
    public partial class RefundDialog : ModernDialog
    {
        InscriptionDatesDto limites = new InscriptionDatesDto();
        public List<RefundDto> refunds = new List<RefundDto>();

        List<string> devoluciones = new List<string> {
                "100%",
                "75%",
                "50%",
                "25%",
                 "0%"
            };

        public RefundDialog(InscriptionDatesDto limites)
        {
            InitializeComponent();
            this.limites = limites;
            Limite.DisplayDateEnd = limites.fechaFin;
            Limite.DisplayDateStart = limites.fechaInicio;
            CloseButton.Visibility = Visibility.Collapsed;

        }

        private void Devolucion_Initialized(object sender, EventArgs e)
        {
           

            Refund.ItemsSource = devoluciones;

        }

        private void BtPlazo_Click(object sender, RoutedEventArgs e)
        {
         
            if (Limite.SelectedDate != null && Refund.SelectedItem != null) {                
                RefundDto nuevoRefund = new RefundDto {                   
                    date_refund = (DateTime)Limite.SelectedDate,
                    refund = Double.Parse(Refund.SelectedItem.ToString().Replace("%", ""))
                };
                Plazos_list.Items.Add(nuevoRefund);
                Limite.SelectedDate = null;
                if (nuevoRefund.date_refund.Date.Year == limites.fechaFin.Date.Year && nuevoRefund.date_refund.Date.Month == limites.fechaFin.Date.Month && nuevoRefund.date_refund.Date.Day == limites.fechaFin.Date.Day)
                    Limite.IsEnabled = false;
              
                else
                    Limite.DisplayDateStart = nuevoRefund.date_refund.AddDays(1);
                Refund.SelectedItem = null;
                

            }


        }

        private void BtModificar_Click(object sender, RoutedEventArgs e)
        {
            Plazos_list.Items.Clear();
            Limite.DisplayDateEnd = limites.fechaFin;
            Limite.DisplayDateStart = limites.fechaInicio;
            Limite.IsEnabled = true;
            Refund.ItemsSource = devoluciones;

        }

        private void BtAceptar_Click(object sender, RoutedEventArgs e)
        {
            foreach (var c in Plazos_list.Items)
                refunds.Add((RefundDto)c);

            Close();
        }
    }
}


