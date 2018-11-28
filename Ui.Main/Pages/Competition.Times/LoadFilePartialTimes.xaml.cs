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
        private PartialTimesDto _dto;
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

            CsvLoader loader = new CsvTimes(new string[] { _file });

            if (loader.Returned is IEnumerable<PartialTimesObjects> objects) {
                string[] dnis = new string[objects.Count()];
                for (int i = 0; i < objects.Count(); i++) {
                    dnis[i] = service.SelectDniFromDorsal(objects.ElementAt(i).Dorsal, objects.ElementAt(i).CompetitionId);
                }

                int index = 0;
                IList<PartialTimesDto> noInserted = new List<PartialTimesDto>();
                IList<string> noInsertedDni = new List<string>();
                int countI = 0;
                int countU = 0;
                string notes = string.Join("\n", loader.Errores);
                foreach (PartialTimesObjects times in objects) {
                    try {
                        _dto = new PartialTimesDto() {
                            CompetitionDto = new CompetitionService().SearchCompetitionById(new CompetitionDto() {
                                ID = times.CompetitionId
                            }),
                            Time = times.Times
                        };


                        string status = new EnrollService(_dto.CompetitionDto).SelectStatusEnroll(dnis[index]);

                        if (dnis[index] != null && status.Equals("REGISTERED")) {
                            new EnrollService(_dto.CompetitionDto).InsertHasRegisteredTimes(dnis[index], _dto.Time[0], _dto.Time[_dto.Time.Length - 1]);
                            timesService.InsertPartialTime(dnis[index], _dto);
                            countI++;

                        } else {
                            string s = $"El atleta {times.Dorsal} no esta registrado. \n";
                            if (notes.Contains(s)) {
                                notes += s;
                            } else {
                                notes += $"Linea {index + 1}: {s}";
                            }
                        }
                    } catch (InvalidOperationException) {
                        if (_dto != null) {
                            noInserted.Add(_dto);
                            noInsertedDni.Add(dnis[index]);
                        }

                    }
                    index++;
                }

                if (noInserted.Count > 0) {
                    MessageBoxResult result = MessageBoxResult.None;
                    string message = "¿Quiere sobreescribir los tiempos?";
                    result = MessageBox.Show(message, "Error", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes) {
                        index = 0;
                        foreach (var noInsert in noInserted) {
                            timesService.UpdateAthleteRegisteredDorsal(noInsertedDni[index++], noInsert);
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
                str = $"Se han insertado correctamente {countI} elemento(s).";
            } else if (countI == 0 && countU != 0) {
                str = $"Se han actualizado correctamente {countU} elemento(s).";
            } else if (countI == 0 && countU != 0) {
                str = $"No se ha insertado o actualizado ningún registro.";
            } else {
                str = $"Se han insertado {countI} elemento(s) y actualizado {countU} elemento(s).";
            }

            str += "\n" + notes;

            TxUpdate.Text = str;
            TxUpdate.IsEnabled = true;
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
