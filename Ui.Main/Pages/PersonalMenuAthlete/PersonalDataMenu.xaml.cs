using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logic.Db.Dto;
using Logic.Db.Util;
using Logic.Db.Util.Services;
using Logic.Db.Properties;

namespace Ui.Main.Pages.PersonalMenuAthlete
{
    /// <summary>
    /// Lógica de interacción para PersonalDataMenu.xaml
    /// </summary>
    public partial class PersonalDataMenu : Page
    {
     
      
        private AthletesService _serviceAthlete;
        private CompetitionService _serviceComp;

        public PersonalDataMenu()
        {
            InitializeComponent();
            GeneratePersonalDataTable();
            GenerateInscriptionsDataTable();
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!ComprobarDNI(Dni.Text))
            {
                MessageBox.Show(Properties.Resources.InvalidDNI);
                return;
            }
            DataTable table = _serviceAthlete.SelectAthleteByDni(Dni.Text.ToUpper());
            if (table.Rows.Count == 0)
            {
                MessageBox.Show(Properties.Resources.NoRegistrado);
                return;
            }

            GeneratePersonalDataTable();
           
        }

        private void GeneratePersonalDataTable()
        {
            _serviceAthlete = new AthletesService();           

            DataTable table = _serviceAthlete.SelectAthleteByDni(Dni.Text.ToUpper());
           
            table.Columns[0].ColumnName = Properties.Resources.AthleteDni;
            table.Columns[1].ColumnName = Properties.Resources.AthleteName;
            table.Columns[2].ColumnName = Properties.Resources.AthleteSurname;
           

            DataGridDataPersonal.ItemsSource = table.DefaultView;
            GenerateInscriptionsDataTable();
        }

        private void GenerateInscriptionsDataTable()
        {
            _serviceComp = new CompetitionService();

            DataTable table = _serviceComp.SelectAllCompetitionsInscripted(Dni.Text.ToUpper());
            table.Columns[0].ColumnName = Properties.Resources.Competition_Name;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Status;
            table.Columns[2].ColumnName = Properties.Resources.Competition_Date;
            table.Columns[3].ColumnName = Properties.Resources.AthleteDorsal;
           
           
            DataGridInscriptions.ItemsSource = table.DefaultView;

        }

        private void GenerateResultsDataTable()
        {
           

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

        private void DataGridDataPersonal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
