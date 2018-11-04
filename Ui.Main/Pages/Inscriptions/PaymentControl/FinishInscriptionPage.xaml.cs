using System;
using System.Collections.Generic;
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

namespace Ui.Main.Pages.Inscriptions.InscriptionsPaidControl
{
    /// <summary>
    /// Lógica de interacción para FinishInscriptionPage.xaml
    /// </summary>
    public partial class FinishInscriptionPage : Page
    {

        private String _file;

        public FinishInscriptionPage()
        {
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
    }
}
