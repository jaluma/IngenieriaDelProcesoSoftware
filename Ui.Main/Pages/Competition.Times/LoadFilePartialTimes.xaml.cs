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
using Logic.Db.Dto;
using Logic.Db.Util.Services;
using Control = System.Windows.Controls.Control;
using Cursors = System.Windows.Input.Cursors;
using MessageBox = System.Windows.MessageBox;
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

                int index = 0;
                IList<PartialTimesDto> noInserted = new List<PartialTimesDto>();
                PartialTimesDto dto = null;
                int countI = 0;
                int countU = 0;
                string notes = "";
                foreach (PartialTimesObjects times in objects) {
                    try {
                         dto = new PartialTimesDto() {
                            CompetitionDto = new CompetitionService().SearchCompetitionById(new CompetitionDto() {
                                ID = times.CompetitionId
                            }),
                            Time =  times.Times

                        };
                        if (dto.CompetitionDto.Status.Equals("FINISH")) {
                            timesService.InsertPartialTime(dnis[index], dto);
                            countI++;
                        } else {
                            notes = $"Linea {index}: La competicion {dto.CompetitionDto.Name} no esta finalizada. \n";
                        }
                    } catch (InvalidOperationException) {
                        if (dto != null)
                            noInserted.Add(dto);
                    }
                    index++;
                }

                if (noInserted.Count > 0) {
                    MessageBoxResult  result = MessageBoxResult.None;
                    string message = "¿Quiere sobreescribir los tiempos?";
                    result = MessageBox.Show(message, "Error", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes) {
                        index = 0;
                        foreach (var noInsert in noInserted) {
                            timesService.UpdateAthleteRegisteredDorsal(dnis[index++], noInsert);
                            countU++;
                        }
                    } 
                }

                PrintInsert(notes, countI, countU);
                
            }
        }

        private void PrintInsert(string notes, int countI, int countU) {
            string str = "";
            if (countI != 0 && countU == 0) {
                str = $"Se han insertado correctamente {countI} elementos.";
            } else if (countI == 0 && countU != 0) {
                str = $"Se han actualizado correctamente {countU} elementos.";
            } else {
                str = $"Se han insertado {countI} elementos y actualizado {countU} elementos.";
            }

            str += "\n" + notes;

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
