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
using System.Windows.Input;
using Button = System.Windows.Forms.Button;
using DataGridTextColumn = FirstFloor.ModernUI.Windows.Controls.DataGridTextColumn;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace Ui.Main.Pages.Competitions
{
    /// <summary>
    /// Lógica de interacción para ListOpenCompetition.xaml
    /// </summary>
    public partial class ListOpenCompetition : Page
    {
        private CompetitionService _service;
        private List<long> _columnIds;
        byte[] bytes;


        public ListOpenCompetition() {
            //inicializamos los componentes
            InitializeComponent();

            GenerateTable();
        }

        private void GenerateTable() {
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
            table.Columns[8].ColumnName = "b";

            _columnIds = table.AsEnumerable()
                .Select(dr => dr.Field<long>(Properties.Resources.Competition_Id)).ToList();

            table.Columns.RemoveAt(0);
            DataGridCompetition.ItemsSource = table.DefaultView;

            DataColumn column = new DataColumn(Properties.Resources.Rules, typeof(string));
            table.Columns.Add(column);

            foreach (DataRow row in table.Rows) {
                if (row.Field<byte[]>("b") != null)
                    row.SetField<string>(column, "Descargar");
            }


            table.Columns.RemoveAt(7);
        }

        private void DataGridCompetition_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {


            int indexSeletected = DataGridCompetition.SelectedIndex;

            int id = (int)_columnIds[indexSeletected];

            CompetitionDto competition = new CompetitionDto()
                { ID = id};

            CompetitionService service = new CompetitionService();
            bytes = service.GetRules(competition);

            if (bytes != null)
            {
                using(var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        string path= fbd.SelectedPath;
                        CompetitionService service1 = new CompetitionService();
                        string nombre = service1.SearchCompetitionById(competition).Name;
                        string filename = $"Reglamento de {nombre}.pdf";

                        string absolutePath = System.IO.Path.Combine(path, filename);
                        for (int count = 1; File.Exists(absolutePath); count++) {
                            filename = $"Reglamento de {nombre} (Copia {count}).pdf";
                            absolutePath = System.IO.Path.Combine(path, filename);
                        }

                        BinaryWriter writer = new BinaryWriter(File.Open(absolutePath, FileMode.CreateNew));

                        writer.Write(bytes);

                        writer.Close();

                        System.Diagnostics.Process.Start(absolutePath);
                    }
                }
            }


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

        private void ListOpenCompetition_OnLoaded(object sender, RoutedEventArgs e) {
            GenerateTable();
        }
    }
}
