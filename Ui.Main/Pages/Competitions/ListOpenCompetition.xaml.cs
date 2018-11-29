using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Logic.Db.Dto;
using Logic.Db.Util.Services;
using Cursors = System.Windows.Input.Cursors;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace Ui.Main.Pages.Competitions
{
    /// <summary>
    ///     Lógica de interacción para ListOpenCompetition.xaml
    /// </summary>
    public partial class ListOpenCompetition : Page
    {
        private List<long> _columnIds;
        private List<long> _columnIds2;
        private CompetitionService _service;
        private byte[] bytes;


        public ListOpenCompetition() {
            //inicializamos los componentes
            InitializeComponent();

            GenerateTable();
            GenerateTable2();
        }

        private void GenerateTable() {
            _service = new CompetitionService();
            var table = _service.ListOpenCompetitions();
            table.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Name;
            table.Columns[2].ColumnName = Properties.Resources.Competition_Type;
            table.Columns[3].ColumnName = Properties.Resources.Competition_Km;
            table.Columns[4].ColumnName = Properties.Resources.Competition_Price;
            table.Columns[5].ColumnName = "Desnivel";
            table.Columns[6].ColumnName = Properties.Resources.InscriptionOpen;
            table.Columns[7].ColumnName = Properties.Resources.InscriptionClose;
            table.Columns[8].ColumnName = Properties.Resources.Competition_Date;
            table.Columns[9].ColumnName = "b";

            _columnIds = table.AsEnumerable()
                .Select(dr => dr.Field<long>(Properties.Resources.Competition_Id)).ToList();

            table.Columns.RemoveAt(0);

            var column = new DataColumn(Properties.Resources.Rules, typeof(string));
            table.Columns.Add(column);

            foreach (DataRow row in table.Rows)
                if (row.Field<byte[]>("b") != null)
                    row.SetField(column, "Descargar");
                else if (row.Field<byte[]>("b") == null) row.SetField(column, "No adjunto");

            table.Columns.RemoveAt(8);
            table.Columns.Remove(Properties.Resources.InscriptionOpen);
            DataGridCompetition.ItemsSource = table.DefaultView;
        }

        private void GenerateTable2() {
            _service = new CompetitionService();
            var table = _service.ListPreInscriptionCompetitions();
            table.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Name;
            table.Columns[2].ColumnName = Properties.Resources.Competition_Type;
            table.Columns[3].ColumnName = Properties.Resources.Competition_Km;
            table.Columns[4].ColumnName = "Desnivel";
            table.Columns[5].ColumnName = "Preinscripción desde";
            table.Columns[6].ColumnName = "Preinscripción hasta";
            table.Columns[7].ColumnName = Properties.Resources.Competition_Date;
            table.Columns[8].ColumnName = "b";

            _columnIds2 = table.AsEnumerable()
                .Select(dr => dr.Field<long>(Properties.Resources.Competition_Id)).ToList();

            table.Columns.RemoveAt(0);


            var column = new DataColumn(Properties.Resources.Rules, typeof(string));
            table.Columns.Add(column);

            foreach (DataRow row in table.Rows)
                if (row.Field<byte[]>("b") != null)
                    row.SetField(column, "Descargar");
                else if (row.Field<byte[]>("b") == null) row.SetField(column, "No adjunto");

            table.Columns.RemoveAt(7);
            table.Columns.Remove("Preinscripción desde");
            DataGridCompetition_P.ItemsSource = table.DefaultView;
        }

        private void DataGridCompetition_OnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var indexSeletected = DataGridCompetition.SelectedIndex;

            var id = (int) _columnIds[indexSeletected];

            var competition = new CompetitionDto {ID = id};

            var service = new CompetitionService();
            bytes = service.GetRules(competition);

            if (bytes != null)
                using (var fbd = new FolderBrowserDialog()) {
                    var result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) {
                        var path = fbd.SelectedPath;
                        var service1 = new CompetitionService();
                        var nombre = service1.SearchCompetitionById(competition).Name;
                        var filename = $"Reglamento de {nombre}.pdf";

                        var absolutePath = Path.Combine(path, filename);
                        for (var count = 1; File.Exists(absolutePath); count++) {
                            filename = $"Reglamento de {nombre} (Copia {count}).pdf";
                            absolutePath = Path.Combine(path, filename);
                        }

                        var writer = new BinaryWriter(File.Open(absolutePath, FileMode.CreateNew));

                        writer.Write(bytes);

                        writer.Close();

                        Process.Start(absolutePath);
                    }
                }
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if (e.PropertyType == typeof(DateTime))
                ((DataGridTextColumn) e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void DataGridCompetition_OnMouseEnter(object sender, MouseEventArgs e) {
            DataGridCompetition.Cursor = Cursors.Hand;
        }

        private void DataGridCompetition_OnMouseLeave(object sender, MouseEventArgs e) {
            DataGridCompetition.Cursor = null;
        }

        private void ListOpenCompetition_OnLoaded(object sender, RoutedEventArgs e) {
            GenerateTable();
        }

        private void DataGridCompetition_P_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var indexSeletected = DataGridCompetition_P.SelectedIndex;

            var id = (int) _columnIds2[indexSeletected];

            var competition = new CompetitionDto {ID = id};

            var service = new CompetitionService();
            bytes = service.GetRules(competition);

            if (bytes != null)
                using (var fbd = new FolderBrowserDialog()) {
                    var result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) {
                        var path = fbd.SelectedPath;
                        var service1 = new CompetitionService();
                        var nombre = service1.SearchCompetitionById(competition).Name;
                        var filename = $"Reglamento de {nombre}.pdf";

                        var absolutePath = Path.Combine(path, filename);
                        for (var count = 1; File.Exists(absolutePath); count++) {
                            filename = $"Reglamento de {nombre} (Copia {count}).pdf";
                            absolutePath = Path.Combine(path, filename);
                        }

                        var writer = new BinaryWriter(File.Open(absolutePath, FileMode.CreateNew));

                        writer.Write(bytes);

                        writer.Close();

                        Process.Start(absolutePath);
                    }
                }
        }

        private void DataGridCompetition_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer1.ScrollToVerticalOffset(ScrollViewer1.VerticalOffset - e.Delta);
        }

        private void DataGridCompetition_P_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer2.ScrollToVerticalOffset(ScrollViewer2.VerticalOffset - e.Delta);
        }
    }
}
