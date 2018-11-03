using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


using Logic.Db.Dto;
using Logic.Db.Util.Services;
using Ui.Main.Pages.MenuInitial;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logic.Db.Util;
using Logic.Db.Properties;
using System.IO;
using System.Windows.Forms;
using DataGridTextColumn = FirstFloor.ModernUI.Windows.Controls.DataGridTextColumn;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace Ui.Main.Pages.Competitions
{
    /// <summary>
    /// Lógica de interacción para ListOpenCompetition.xaml
    /// </summary>
    public partial class ListOpenCompetition : Page
    {
        private readonly CompetitionService _service;
        private List<long> _columnIds;
        byte[] bytes;
      


        public ListOpenCompetition()
        {
            //inicializamos los componentes
            InitializeComponent();
           
            _service = new CompetitionService();
            DataTable table = _service.ListOpenCompetitions();
            table.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Name;
            table.Columns[2].ColumnName = Properties.Resources.Competition_Type;
            table.Columns[3].ColumnName = Properties.Resources.Competition_Km;
            table.Columns[4].ColumnName = Properties.Resources.Competition_Price;
            table.Columns[5].ColumnName = Properties.Resources.InscriptionOpen;
            table.Columns[6].ColumnName = Properties.Resources.InscriptionClose;
            table.Columns[7].ColumnName = Properties.Resources.Competition_Date;
            table.Columns[8].ColumnName = Properties.Resources.Rules;

            _columnIds = table.AsEnumerable()
                .Select(dr => dr.Field<long>(Properties.Resources.Competition_Id)).ToList();

            table.Columns.RemoveAt(0);
            DataGridCompetition.ItemsSource = table.DefaultView;

           
            

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int indexSeletected = DataGridCompetition.SelectedIndex;

            int id = (int)_columnIds[indexSeletected];
            CompetitionDto competition = new CompetitionDto()
            { ID = id};

            CompetitionService service = new CompetitionService();
            bytes = service.GetRules(competition);
            CompetitionService service1 = new CompetitionService();
            string nombre = service1.SearchCompetitionById(competition).Name;

            BinaryWriter writer = new BinaryWriter(File.Open(@"C:\Users\Public\Downloads\Reglamento de "+ nombre + ".pdf", FileMode.CreateNew));
            writer.Write(bytes);

            writer.Close();

            System.Diagnostics.Process.Start(@"C:\Users\Public\Downloads\Reglamento de" + nombre + ".pdf");


        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                ((System.Windows.Controls.DataGridTextColumn)e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }
        private void DataGridCompetition_OnMouseEnter(object sender, MouseEventArgs e)
        {
            DataGridCompetition.Cursor = System.Windows.Input.Cursors.Hand;
        }

        private void DataGridCompetition_OnMouseLeave(object sender, MouseEventArgs e)
        {
            DataGridCompetition.Cursor = null;
        }



    }
}
