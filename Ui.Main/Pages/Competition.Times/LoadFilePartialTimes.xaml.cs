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
using Logic.Db;
using Logic.Db.Csv;
using Logic.Db.Csv.Object;
using Logic.Db.Util.Services;
using Control = System.Windows.Controls.Control;
using Cursors = System.Windows.Input.Cursors;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace Ui.Main.Pages.Competition.Times {
    /// <summary>
    /// Lógica de interacción para LoadFilePartialTimes.xaml
    /// </summary>

    public partial class LoadFilePartialTimes : Page {
        private string _file;
        private readonly AthletesService service;
        private readonly TimesService timesService;
        public LoadFilePartialTimes() {
            service = new AthletesService();
            timesService = new TimesService();
            InitializeComponent();
        }

        private void BtSeleccionar_OnClick(object sender, RoutedEventArgs e) {
            OpenFileDialog openFile = new OpenFileDialog {
                Filter = @"*.csv|*.CSV",
                Multiselect = false
            };
            openFile.ShowDialog();
            _file = openFile.FileName;

            CsvLoader loader = new CsvTimes(new string[] {_file});
            PartialTimesObjects times = loader.Returned as PartialTimesObjects;

            if (times != null) {
                string[] dnis = new string[times.Times.Length];
                for (int i = 0; i < times.Times.Length; i++) {
                    dnis[i] = service.SelectDniFromDorsal(times.Dorsal);
                }

                for (int i = 0; i < dnis.Length; i++) {
                    timesService.InsertPartialTime(dnis[i], times.Times);
                    // algo que imprimir
                }
            }
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
