using System;
using System.Collections.Generic;
using System.Data;
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

namespace Ui.Main.Pages.OpenCompetitions
{
    /// <summary>
    /// Lógica de interacción para ListOpenCompetition.xaml
    /// </summary>
    public partial class ListOpenCompetition : Page
    {
        private readonly CompetitionService _service;
       

        public ListOpenCompetition()
        {

            InitializeComponent();
           
            _service = new CompetitionService();
            DataTable table = _service.ListOpenCompetitions();
            table.Columns[0].ColumnName = Properties.Resources.Competition_Name;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Type;
            table.Columns[2].ColumnName = Properties.Resources.Competition_Km;
            table.Columns[3].ColumnName = Properties.Resources.Competition_Price;
            table.Columns[4].ColumnName = Properties.Resources.InscriptionOpen;
            table.Columns[5].ColumnName = Properties.Resources.LimitInscription;            
            table.Columns[6].ColumnName = Properties.Resources.Competition_Number;

           


            DataGridCompetition.ItemsSource = table.DefaultView;
        }

    
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

    }
}
