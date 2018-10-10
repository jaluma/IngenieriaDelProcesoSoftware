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
            DataTable dt = new DataTable();
            //defines las columnas del datatable
            dt.Columns.Add(Properties.Resources.Competition_Id);
            dt.Columns.Add(Properties.Resources.Competition_Name);
            dt.Columns.Add(Properties.Resources.Competition_Type);
            dt.Columns.Add(Properties.Resources.Competition_Km);
            dt.Columns.Add(Properties.Resources.Competition_Price);
            dt.Columns.Add(Properties.Resources.Competition_Date);
            dt.Columns.Add(Properties.Resources.Competition_Number);
            

            foreach (var item in _service.ListOpenCompetitions())
            {
                if (!(item.Status.Equals("FINISH"))){
                    DataRow row = dt.NewRow();

                    row[Properties.Resources.Competition_Id] = item.ID;
                    row[Properties.Resources.Competition_Name] = item.Name;
                    row[Properties.Resources.Competition_Type] = item.Type;
                    row[Properties.Resources.Competition_Km] = item.Km;
                    row[Properties.Resources.Competition_Price] = item.Price;
                    row[Properties.Resources.Competition_Date] = item.Date;
                    row[Properties.Resources.Competition_Number] = item.NumberPlaces;
                    dt.Rows.Add(row);

                } }

            DataGridCompetition.ItemsSource = dt.DefaultView;
            
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataRowView row = DataGridCompetition.SelectedItems[0] as DataRowView;

            long id = (long)row[0];

            CompetitionDto competition = new CompetitionDto()
            {
                ID = (int)id
            };

            _service.Dispose();



        }

    }
}
