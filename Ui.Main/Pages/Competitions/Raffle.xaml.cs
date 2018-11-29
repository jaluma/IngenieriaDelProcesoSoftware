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
using iTextSharp.text;
using iTextSharp.text.pdf;
using Logic.Db.Dto;
using Logic.Db.Util.Services;
using Cursors = System.Windows.Input.Cursors;
using MessageBox = System.Windows.Forms.MessageBox;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace Ui.Main.Pages.Competitions
{
    /// <summary>
    ///     Lógica de interacción para Raffle.xaml
    /// </summary>
    public partial class Raffle : Page
    {
        private List<long> _columnIds;
        private CompetitionService _service;
        private AthletesService _serviceA;
        private readonly CompetitionDto competition = new CompetitionDto();
        private DataTable table;


        public Raffle() {
            InitializeComponent();
            GenerateTable();
            DataGridResults.ItemsSource = table.DefaultView;
        }

        private void GenerateTable() {
            _service = new CompetitionService();
            table = _service.SelectRaffleCompetitions();
            table.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Name;


            _columnIds = table.AsEnumerable()
                .Select(dr => dr.Field<long>(Properties.Resources.Competition_Id)).ToList();


            table.Columns.Remove(Properties.Resources.Competition_Id);
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if (e.PropertyType == typeof(DateTime))
                ((DataGridTextColumn) e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void DataGridCompetition_OnMouseEnter(object sender, MouseEventArgs e) {
            DataGridResults.Cursor = Cursors.Hand;
        }

        private void DataGridCompetition_OnMouseLeave(object sender, MouseEventArgs e) {
            DataGridResults.Cursor = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (DataGridResults.SelectedItem != null) {
                const string message =
                    "¿Está segur@ que desea realizar el sorteo de adjudicación de plazas para esta competición? Esta opción no se puede deshacer";
                const string caption = "Atención";
                var result = MessageBox.Show(message, caption,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);


                if (result == DialogResult.No) { }

                else {
                    if (Resultados.Items.IsEmpty) {
                        competition.ID = _columnIds[DataGridResults.SelectedIndex];

                        Resultados.Items.Clear();
                        Resultados.Items.Add("ADMITIDOS");
                        _serviceA = new AthletesService();

                        var atletas = _serviceA.SelectAtheletesRaffle(_columnIds[DataGridResults.SelectedIndex]);
                        var contador = _service.SelectNumberRaffle(_columnIds[DataGridResults.SelectedIndex]);

                        foreach (var d in atletas)
                            if (contador > 0) {
                                Resultados.Items.Add(d);
                                _serviceA.ChangeStatusAthlete(d, competition.ID);
                                contador--;
                            }


                        Resultados.Items.Add("RESERVA");
                        foreach (var d in atletas)
                            if (!Resultados.Items.Contains(d))
                                Resultados.Items.Add(d);


                        table.Rows[DataGridResults.SelectedIndex].Delete();
                    }
                    else {
                        MessageBox.Show("Por favor, guarde los resultados del sorteo haciendo click en imprimir");
                    }
                }
            }
            else {
                MessageBox.Show("Por favor, seleccione una competición");
            }
        }


        private void Imprimir_Click(object sender, RoutedEventArgs e) {
            var doc = new Document(PageSize.LETTER);
            using (var fbd = new FolderBrowserDialog()) {
                var result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) {
                    var path = fbd.SelectedPath;
                    var nombre = _service.SearchCompetitionById(competition).Name;
                    var filename = $"Adjudicación de plazas de {nombre}.pdf";
                    var absolutePath = Path.Combine(path, filename);
                    for (var count = 1; File.Exists(absolutePath); count++) {
                        filename = $"Adjudicación de plazas de {nombre} (Copia {count}).pdf";
                        absolutePath = Path.Combine(path, filename);
                    }

                    var writer = PdfWriter.GetInstance(doc,
                        new FileStream(absolutePath, FileMode.CreateNew));

                    doc.Open();
                    var _standardFont = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, BaseColor.BLACK);


                    doc.Add(new Paragraph($"Adjudicación de plazas de {nombre}"));
                    doc.Add(Chunk.NEWLINE);

                    foreach (var d in Resultados.Items) doc.Add(new Paragraph(d.ToString()));

                    doc.Close();
                    writer.Close();
                    Process.Start(absolutePath);
                    _service.ChangeToClosed(competition.ID);
                    Resultados.Items.Clear();
                }
            }
        }

        private void DataGridResults_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - e.Delta);
        }
    }
}
