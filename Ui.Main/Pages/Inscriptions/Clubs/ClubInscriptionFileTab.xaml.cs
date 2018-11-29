using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Logic.Db.Dto;
using Logic.Db.Dto.Types;
using Logic.Db.Util.Services;
using Ui.Main.Pages.Inscriptions.Payment;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace Ui.Main.Pages.Inscriptions.Clubs
{
    /// <summary>
    ///     Lógica de interacción para ClubInscriptionFileTab.xaml
    /// </summary>
    public partial class ClubInscriptionFileTab : UserControl
    {
        private readonly AthletesService _athletesService;
        private readonly CompetitionService _competitionService;
        private readonly EnrollService _enrollService;

        private readonly List<AthleteDto> _athletes;

        private List<long> _columnIds;
        private CompetitionDto _competition;
        private int _count;
        private readonly List<AthleteDto> _validAthletes;

        public ClubInscriptionFileTab() {
            InitializeComponent();
            _athletesService = new AthletesService();
            _competitionService = new CompetitionService();
            _enrollService = new EnrollService(null);
            _athletes = new List<AthleteDto>();
            _validAthletes = new List<AthleteDto>();
            _count = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var openFile = new OpenFileDialog {
                Filter = @"*.csv|*.CSV",
                Multiselect = false
            };
            openFile.ShowDialog();
            if (!openFile.FileName.Equals("")) ProcesarFichero(openFile.FileName);
        }

        private void ProcesarFichero(string file) {
            var list = new List<string[]>();
            using (var readFile = new StreamReader(file)) {
                string line;
                var row = new string[6];

                while ((line = readFile.ReadLine()) != null) {
                    row = line.Split(',');
                    list.Add(row);
                }
            }

            if (list.Count == 0) {
                MessageBox.Show(Properties.Resources.EmptyDocument);
                return;
            }

            foreach (var s in list)
                try {
                    var athlete = new AthleteDto {
                        Dni = s[0].ToUpper(),
                        Name = s[1],
                        Surname = s[2],
                        BirthDate = DateTime.Parse(s[3])
                    };
                    if (s[4][0] == 'M')
                        athlete.Gender = AthleteDto.MALE;
                    else
                        athlete.Gender = AthleteDto.FEMALE;
                    _athletes.Add(athlete);
                }
                catch (Exception) {
                    MessageBox.Show(Properties.Resources.InvalidDocument);
                    return;
                }

            InsertarAtletas(_athletes);
        }

        private void InsertarAtletas(List<AthleteDto> athletes) {
            var stringBuilder = new StringBuilder();
            foreach (var a in athletes) {
                if (!ComprobarDNI(a.Dni)) {
                    stringBuilder.Append(a.Dni + " no registrado: DNI inválido.\n\n");
                    continue;
                }

                var cont = _athletesService.CountAthleteByDni(a.Dni);
                if (cont != 0) {
                    stringBuilder.Append(a.Dni + " registrado anteriormente.\n\n");
                    _validAthletes.Add(a);
                    continue;
                }

                if (DateTime.Now.Year - a.BirthDate.Year < 18 || DateTime.Now.Year - a.BirthDate.Year > 100) {
                    stringBuilder.Append(a.Dni + " no registrado: fecha de nacimiento no válida.\n\n");
                    continue;
                }

                _athletesService.InsertAthletesTable(a);
                _validAthletes.Add(a);
                stringBuilder.Append(a.Dni + " registrado correctamente.\n\n");
            }

            TxFileIns.Text = stringBuilder.ToString();
            GetListCompetition();
        }

        private bool ComprobarDNI(string dni) {
            if (!(dni.Length == 9))
                return false;

            for (var i = 0; i < 8; i++)
                if (!char.IsDigit(dni[i]))
                    return false;

            if (!char.IsLetter(dni[8]))
                return false;

            return true;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if (e.PropertyType == typeof(DateTime))
                ((DataGridTextColumn) e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }


        private void CompetitionsToSelect_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer2.ScrollToVerticalOffset(ScrollViewer2.VerticalOffset - e.Delta);
        }

        private void CompetitionsToSelect_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var indexSeletected = CompetitionsToSelect.SelectedIndex;

            if (indexSeletected != -1)
                _competition = new CompetitionDto {
                    ID = (int) _columnIds[indexSeletected]
                };
        }

        private void BtFinish_Click(object sender, RoutedEventArgs e) {
            if (CompetitionsToSelect.SelectedItem == null) {
                MessageBox.Show(Properties.Resources.NothingSelected);
                return;
            }

            var dto = new CompetitionDto {
                ID = _columnIds[CompetitionsToSelect.SelectedIndex]
            };
            _competition = _competitionService.SearchCompetitionById(dto);

            var stringBuilder = new StringBuilder();
            foreach (var a in _validAthletes)
                if (_enrollService.IsAthleteInComp(_competition, a)) {
                    stringBuilder.Append(a.Dni + " inscrito anteriormente en la competición.\n");
                }
                else {
                    _enrollService.InsertAthleteInCompetition(a, _competition, TypesStatus.Registered);
                    _count++;
                    stringBuilder.Append(a.Dni + " inscrito correctamente.\n");
                }


            var dialog = new DialogPayment(null, null);
            dialog.Content = new InscriptionProofClubs(_competition, stringBuilder.ToString(), _count);
            dialog.Show();

            _count = 0;
            GetListCompetition();
        }

        private void GetListCompetition() {
            var table = _competitionService.ListCompetitionsToInscribeClubs(_validAthletes.Count);
            table.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Name;
            table.Columns[2].ColumnName = Properties.Resources.Competition_Type;
            table.Columns[3].ColumnName = Properties.Resources.Competition_Km;
            table.Columns[4].ColumnName = Properties.Resources.Competition_Price;
            table.Columns[5].ColumnName = Properties.Resources.InscriptionOpen;
            table.Columns[6].ColumnName = Properties.Resources.InscriptionClose;
            table.Columns[7].ColumnName = Properties.Resources.Competition_Date;

            _columnIds = table.AsEnumerable()
                .Select(dr => dr.Field<long>(Properties.Resources.Competition_Id)).ToList();

            table.Columns.RemoveAt(0);
            table.Columns.Remove(Properties.Resources.InscriptionOpen);

            if (CompetitionsToSelect != null)
                CompetitionsToSelect.ItemsSource = table.DefaultView;
        }
    }
}
