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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Control = System.Windows.Controls.Control;
using Cursors = System.Windows.Input.Cursors;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace Ui.Main.Pages.Competition.Times {
    /// <summary>
    /// Lógica de interacción para LoadFilePartialTimes.xaml
    /// </summary>

    public partial class LoadFilePartialTimes : Page {
        private string _file;
        public LoadFilePartialTimes() {
            InitializeComponent();
        }

        private void BtSeleccionar_OnClick(object sender, RoutedEventArgs e) {
            OpenFileDialog openFile = new OpenFileDialog {
                Filter = @"*.csv|*.CSV",
                Multiselect = false
            };
            openFile.ShowDialog();
            _file = openFile.FileName;
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
