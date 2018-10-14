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
     
      
        private AthletesService _service;
        

        




        public PersonalDataMenu()
        {
            InitializeComponent();




        }
        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!ComprobarDNI(Dni.Text))
            {
                MessageBox.Show(Properties.Resources.InvalidDNI);
                return;
            }

            GenerateTable();



        }

        private void GenerateTable()
        {
            _service = new AthletesService();
           

            DataTable table = _service.SelectAthleteByDni(Dni.Text.ToUpper());
            table.Columns[0].ColumnName = Properties.Resources.AthleteDni;
            table.Columns[1].ColumnName = Properties.Resources.AthleteName;
            table.Columns[2].ColumnName = Properties.Resources.AthleteSurname;
            table.Columns[3].ColumnName = Properties.Resources.AthleteBirthDate;
            table.Columns[4].ColumnName = Properties.Resources.AthleteGender;

            DataGridCompetition.ItemsSource = table.DefaultView;

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
    }
}
