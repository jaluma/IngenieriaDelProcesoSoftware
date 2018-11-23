using Logic.Db.Dto;
using Logic.Db.Util.Services;
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

namespace Ui.Main.Pages.Inscriptions.Clubs
{
    /// <summary>
    /// Lógica de interacción para ClubInscriptionFileTab.xaml
    /// </summary>
    public partial class ClubInscriptionFileTab : System.Windows.Controls.UserControl
    {
        private readonly AthletesService _athletesService;

        private List<AthleteDto> _athletes;
        private int _count;

        public ClubInscriptionFileTab()
        {
            InitializeComponent();
            _athletesService = new AthletesService();
            _athletes = new List<AthleteDto>();
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
                    continue;
                }
                if (DateTime.Now.Year - a.BirthDate.Year < 18 || DateTime.Now.Year - a.BirthDate.Year > 100)
                {
                    stringBuilder.Append(a.Dni + " no registrado: fecha de nacimiento no válida.\n\n");
                    continue;
                }
                _athletesService.InsertAthletesTable(a);
                stringBuilder.Append(a.Dni + " registrado correctamente.\n\n");
            }
            TxFileIns.Text = stringBuilder.ToString();
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
    }
}
