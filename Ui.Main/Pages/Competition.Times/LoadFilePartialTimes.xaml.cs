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

            if (loader.Returned is IEnumerable<PartialTimesObjects> objects) {
                string[] dnis = new string[objects.Count()];
                for (int i = 0; i < objects.Count(); i++) {
                    dnis[i] = service.SelectDniFromDorsal(objects.ElementAt(i).Dorsal, objects.ElementAt(i).CompetitionId);
                }

                foreach (PartialTimesObjects times in objects) {
                    int countI = 0;
                    int countU = 0;
                    IList<PartialTimesObjects> noInserted = new List<PartialTimesObjects>();
                    for (int i = 0; i < dnis.Length; i++) {
                        try {
                            timesService.InsertPartialTime(dnis[i], objects.ElementAt(i).Times);
                            countI++;
                        } catch (InvalidOperationException) {
                            noInserted.Add(objects.ElementAt(i));
                        }

                    }
                    printInsert(countI, countU);
                }
                
            }
        }

        private void printInsert(int countI, int countU) {
            string str = "";
            if (countI != 0 && countU == 0) {
                str = $"Se han insertado correctamente {countI} elementos.";
            } else if (countI == 0 && countU != 0) {
                str = $"Se han actualizado correctamente {countU} elementos.";
            } else {
                str = $"Se han insertado {countI} elementos y actualizado {countU} elementos.";
            }

            LbUpdate.Content = str;
            LbUpdate.IsEnabled = true;
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
