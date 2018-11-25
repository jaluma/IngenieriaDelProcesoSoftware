using Logic.Db.Dto;
using Logic.Db.Util.Services;
using System;
using System.Collections.Generic;
using System.Data;
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
using Ui.Main.Pages.Inscriptions.Payment;

namespace Ui.Main.Pages.Inscriptions.Clubs
{
    /// <summary>
    /// Lógica de interacción para ClubInscriptionFileTab.xaml
    /// </summary>
    public partial class ClubInscriptionFileTab : System.Windows.Controls.UserControl
    {
        private readonly CompetitionService _competitionService;
        private readonly AthletesService _athletesService;
        private readonly EnrollService _enrollService;

        private List<long> _columnIds;

        private List<AthleteDto> _athletes;
        private List<AthleteDto> _validAthletes;
        private CompetitionDto _competition;
        private int _count;

        public ClubInscriptionFileTab()
        {
            InitializeComponent();
            _athletesService = new AthletesService();
            _competitionService = new CompetitionService();
            _enrollService = new EnrollService(null);
            _athletes = new List<AthleteDto>();
            _validAthletes = new List<AthleteDto>();
            _count = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = @"*.csv|*.CSV",
                Multiselect = false
            };
            openFile.ShowDialog();
            ProcesarFichero(openFile.FileName);
        }

        private void ProcesarFichero(string file)
        {
            List<string[]> list = new List<string[]>();
            using (StreamReader readFile = new StreamReader(file))
            {
                string line;
                string[] row = new string[6];

                while ((line = readFile.ReadLine()) != null)
                {
                    row = line.Split(',');
                    list.Add(row);
                }
            }
            if (list.Count == 0)
            {
                System.Windows.MessageBox.Show(Properties.Resources.EmptyDocument);
                return;
            }

            foreach (string[] s in list)
            {
                try
                {
                    AthleteDto athlete = new AthleteDto()
                    {
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
                catch (Exception)
                {
                    System.Windows.MessageBox.Show(Properties.Resources.InvalidDocument);
                    return;
                }
            }

            InsertarAtletas(_athletes);
        }

        private void InsertarAtletas(List<AthleteDto> athletes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (AthleteDto a in athletes)
            {
                if (!ComprobarDNI(a.Dni))
                {
                    stringBuilder.Append(a.Dni + " no registrado: DNI inválido.\n\n");
                    continue;
                }
                int cont = _athletesService.CountAthleteByDni(a.Dni);
                if (cont != 0)
                { 
                    stringBuilder.Append(a.Dni + " registrado anteriormente.\n\n");
                    _validAthletes.Add(a);
                    continue;
                }
                if (DateTime.Now.Year - a.BirthDate.Year < 18 || DateTime.Now.Year - a.BirthDate.Year > 100)
                {
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

        private bool ComprobarDNI(string dni)
        {
            if (!(dni.Length == 9))
                return false;

            for (int i = 0; i < 8; i++)
                if (!Char.IsDigit(dni[i]))
                    return false;

            if (!Char.IsLetter(dni[8]))
                return false;

            return true;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                ((DataGridTextColumn)e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }


        private void CompetitionsToSelect_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer2.ScrollToVerticalOffset(ScrollViewer2.VerticalOffset - e.Delta);
        }

        private void CompetitionsToSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexSeletected = CompetitionsToSelect.SelectedIndex;

            if (indexSeletected != -1)
            {
                _competition = new CompetitionDto()
                {
                    ID = (int)_columnIds[indexSeletected]
                };
            }

        }

        private void BtFinish_Click(object sender, RoutedEventArgs e)
        {
            if (CompetitionsToSelect.SelectedItem == null)
            {
                System.Windows.MessageBox.Show(Properties.Resources.NothingSelected);
                return;
            }

            /*try {*/
            CompetitionDto dto = new CompetitionDto()
            {
                ID = _columnIds[CompetitionsToSelect.SelectedIndex]
            };
            _competition = _competitionService.SearchCompetitionById(dto);

            StringBuilder stringBuilder = new StringBuilder();
            foreach (AthleteDto a in _validAthletes)
            {
                if (_enrollService.IsAthleteInComp(_competition, a))
                    stringBuilder.Append(a.Dni + " inscrito anteriormente en la competición.\n");
                else
                {
                    _enrollService.InsertAthleteInCompetition(a, _competition, TypesStatus.Registered);
                    _count++;
                    stringBuilder.Append(a.Dni + " inscrito correctamente.\n");
                }
            }


            DialogPayment dialog = new DialogPayment(null, null);
            dialog.Content = new InscriptionProofClubs(_competition, stringBuilder.ToString(), _count);
            dialog.Show();
        }

        private void GetListCompetition()
        {
            DataTable table = _competitionService.ListCompetitionsToInscribeClubs(_validAthletes.Count);
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

            if (CompetitionsToSelect != null)
                CompetitionsToSelect.ItemsSource = table.DefaultView;
        }
    }
}
