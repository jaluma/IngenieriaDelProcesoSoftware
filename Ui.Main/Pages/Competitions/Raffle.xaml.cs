using Logic.Db.Dto;
using Logic.Db.Util.Services;
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

namespace Ui.Main.Pages.Competitions
{
    /// <summary>
    /// Lógica de interacción para Raffle.xaml
    /// </summary>
    public partial class Raffle : Page
    {
        private CompetitionService _service;
        private List<long> _columnIds;


        public Raffle()
        {
            InitializeComponent();
            GenerateTable();
        }

        private void GenerateTable()
        {
            _service = new CompetitionService();
            DataTable table = _service.ListPreInscriptionCompetitions();
            table.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Name;
            
           

            _columnIds = table.AsEnumerable()
                .Select(dr => dr.Field<long>(Properties.Resources.Competition_Id)).ToList();

            table.Columns.RemoveAt(0);
            table.Columns.RemoveAt(2);
            table.Columns.RemoveAt(2);
            table.Columns.RemoveAt(2);
            table.Columns.RemoveAt(2);
            table.Columns.RemoveAt(2);
            table.Columns.RemoveAt(2);
            table.Columns.RemoveAt(1);




            DataGridResults.ItemsSource = table.DefaultView;
        }

       



        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                ((System.Windows.Controls.DataGridTextColumn)e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }
        private void DataGridCompetition_OnMouseEnter(object sender, MouseEventArgs e)
        {
            DataGridResults.Cursor = System.Windows.Input.Cursors.Hand;
        }

        private void DataGridCompetition_OnMouseLeave(object sender, MouseEventArgs e)
        {
            DataGridResults.Cursor = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridResults.SelectedItem != null) {







            }
        }

        private void Imprimir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGridResults_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - e.Delta);
        }
    }
}
