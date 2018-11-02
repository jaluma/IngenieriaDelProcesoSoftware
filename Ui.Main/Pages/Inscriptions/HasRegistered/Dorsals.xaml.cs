using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logic.Db.ActionObjects.AthleteLogic;
using Logic.Db.Dto;
using Logic.Db.Util.Services;
using Cursors = System.Windows.Input.Cursors;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace Ui.Main.Pages.Inscriptions.HasRegistered
{
    /// <summary>
    /// Lógica de interacción para AddDorsalsAndRegisteredInCompetition.xaml
    /// </summary>
    public partial class Dorsals : Page {
        private EnrollService _enroll;
        private CompetitionService _competitionService;
        private CompetitionDto _competition;
        private DataTable _table;
        private List<int> _ids;

        public Dorsals()
        {
            InitializeComponent();

            _competitionService = new CompetitionService();
            _table = _competitionService.ListNotRealizedCompetitions();

            int index = _table.Columns.IndexOf("Competition_Name");
            List<string> list = new List<string>();
            _ids = new List<int>();

            foreach (DataRow row in _table.Rows) {
                _ids.Add(int.Parse(row[_table.Columns.IndexOf("Competition_ID")].ToString()));
                list.Add(row[_table.Columns.IndexOf("Competition_Name")].ToString());
            }

            CompetitionList.ItemsSource = list;
        }

        private void GenerateTable() {
            _enroll = new EnrollService(_competition);
            _table = _enroll.SelectAthleteRegistered();

            _table.Columns[0].ColumnName = Properties.Resources.AthleteDni;
            _table.Columns[1].ColumnName = Properties.Resources.Competition_Id;
            _table.Columns[2].ColumnName = Properties.Resources.Competition_Status;
            _table.Columns[3].ColumnName = Properties.Resources.Competition_Date;
            _table.Columns[4].ColumnName = Properties.Resources.AthleteDorsal;

            DataGridCompetition.ItemsSource = _table.DefaultView;

            if (_table.Rows.Count > 0) {
                BtDorsals.IsEnabled = true;
            } else {
                BtDorsals.IsEnabled = false;
            }
        }

        private void BtDorsals_Click(object sender, RoutedEventArgs e) {
            try {
                bool dorsals = _enroll.IsDorsalsInCompetition(_competition);

                MessageBoxResult result = MessageBoxResult.None;
                if (dorsals) {
                    result = MessageBox.Show("Quiere reemplazar los dorsales?");
                }

                if (result == MessageBoxResult.Yes) {
                    // insertar
                } else {
                    _enroll.UpdateAthleteRegisteredDorsal(_competition);
                }

                GenerateTable();

                BtDorsals.IsEnabled = false;
            } catch (NullReferenceException) {
                MessageBox.Show("Escoja primero la competición");
                return;
            }
        }

        private void CompetitionList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            _competition = new CompetitionDto() {
                ID = _ids[CompetitionList.SelectedIndex]
            };

            GenerateTable();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                ((System.Windows.Controls.DataGridTextColumn)e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }
    }
}
