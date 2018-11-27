using iTextSharp.text;
using iTextSharp.text.pdf;
using Logic.Db.Dto;
using Logic.Db.Util.Services;
using System;
using System.Collections.Generic;
using System.Data;
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
using MessageBox = System.Windows.Forms.MessageBox;

namespace Ui.Main.Pages.Competitions
{
    /// <summary>
    /// Lógica de interacción para Raffle.xaml
    /// </summary>
    public partial class Raffle : Page
    {
        private CompetitionService _service;
        private AthletesService _serviceA;
        private List<long> _columnIds;
        CompetitionDto competition = new CompetitionDto();
        DataTable table;


        public Raffle()
        {
            InitializeComponent();
            GenerateTable();
            DataGridResults.ItemsSource = table.DefaultView;
        }

        private void GenerateTable()
        {
            _service = new CompetitionService();
            table = _service.SelectRaffleCompetitions();
            table.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Name;



            _columnIds = table.AsEnumerable()
                .Select(dr => dr.Field<long>(Properties.Resources.Competition_Id)).ToList();



            table.Columns.RemoveAt(0);



        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                ((System.Windows.Controls.DataGridTextColumn)e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }
        private void DataGridCompetition_OnMouseEnter(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DataGridResults.Cursor = System.Windows.Input.Cursors.Hand;
        }

        private void DataGridCompetition_OnMouseLeave(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DataGridResults.Cursor = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridResults.SelectedItem != null)
            {

                const string message = "¿Está segur@ que desea realizar el sorteo de adjudicación de plazas para esta competición? Esta opción no se puede deshacer";
                const string caption = "Atención";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);


                if (result == DialogResult.No)
                {
                    return;
                }

                else
                {
                    if (Resultados.Items.IsEmpty)
                    {

                        competition.ID = _columnIds[DataGridResults.SelectedIndex];

                        Resultados.Items.Clear();
                        Resultados.Items.Add("ADMITIDOS");
                        _serviceA = new AthletesService();

                        List<AthleteDto> atletas = _serviceA.SelectAtheletesRaffle(_columnIds[DataGridResults.SelectedIndex]);
                        int contador = _service.SelectNumberRaffle(_columnIds[DataGridResults.SelectedIndex]);

                        foreach (AthleteDto d in atletas)
                        {
                            if (contador > 0)
                            {
                                Resultados.Items.Add(d);
                                _serviceA.ChangeStatusAthlete(d, competition.ID);
                                contador--;

                            }
                        }



                        Resultados.Items.Add("RESERVA");
                        foreach (AthleteDto d in atletas)
                        {

                            if (!Resultados.Items.Contains(d))
                                Resultados.Items.Add(d);

                        }

                       

                        table.Rows[DataGridResults.SelectedIndex].Delete();

                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, guarde los resultados del sorteo haciendo click en imprimir");
                return;
            }

        }


        private void Imprimir_Click(object sender, RoutedEventArgs e)
        {

            Document doc = new Document(PageSize.LETTER);
            using (var fbd = new FolderBrowserDialog())
            {

                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string path = fbd.SelectedPath;
                    string nombre = _service.SearchCompetitionById(competition).Name;
                    string filename = $"Adjudicación de plazas de {nombre}.pdf";
                    string absolutePath = System.IO.Path.Combine(path, filename);
                    for (int count = 1; File.Exists(absolutePath); count++)
                    {
                        filename = $"Adjudicación de plazas de {nombre} (Copia {count}).pdf";
                        absolutePath = System.IO.Path.Combine(path, filename);
                    }
                    PdfWriter writer = PdfWriter.GetInstance(doc,
                                            new FileStream(absolutePath, FileMode.CreateNew));

                    doc.Open();
                    iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


                    doc.Add(new iTextSharp.text.Paragraph($"Adjudicación de plazas de {nombre}"));
                    doc.Add(Chunk.NEWLINE);

                    foreach (Object d in Resultados.Items)
                    {

                        doc.Add(new iTextSharp.text.Paragraph(d.ToString()));

                    }

                    doc.Close();
                    writer.Close();
                    System.Diagnostics.Process.Start(absolutePath);
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

      
 