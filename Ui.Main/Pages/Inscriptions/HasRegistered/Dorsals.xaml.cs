using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Logic.Db.Dto;
using Logic.Db.Util.Services;

namespace Ui.Main.Pages.Inscriptions.HasRegistered
{
    /// <summary>
    ///     Lógica de interacción para AddDorsalsAndRegisteredInCompetition.xaml
    /// </summary>
    public partial class Dorsals : Page
    {
        private CompetitionDto _competition;
        private readonly CompetitionService _competitionService;
        private EnrollService _enroll;
        private readonly List<int> _ids;
        private DataTable _table;

        public Dorsals() {
            InitializeComponent();

            _competitionService = new CompetitionService();
            _table = _competitionService.ListNotRealizedCompetitions();

            var index = _table.Columns.IndexOf("Competition_Name");
            var list = new List<string>();
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
            _table.Columns[1].ColumnName = Properties.Resources.Competition_Name;
            _table.Columns[2].ColumnName = Properties.Resources.Competition_Status;
            //_table.Columns[3].ColumnName = Properties.Resources.Competition_Date;
            _table.Columns[3].ColumnName = Properties.Resources.AthleteDorsal;

            _table.Columns.Remove(Properties.Resources.Competition_Name);

            DataGridCompetition.ItemsSource = _table.DefaultView;

            if (_table.Rows.Count > 0)
                BtDorsals.IsEnabled = true;
            else
                BtDorsals.IsEnabled = false;
        }

        private void BtDorsals_Click(object sender, RoutedEventArgs e) {
            try {
                var dorsals = _enroll.IsDorsalsInCompetition(_competition);

                var result = MessageBoxResult.None;
                if (dorsals) {
                    var message = "¿Quiere sobreescribir los dorsales?";
                    result = MessageBox.Show(message, "Error", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes) _enroll.UpdateAthleteRegisteredDorsal(_competition);
                }
                else {
                    _enroll.UpdateAthleteRegisteredDorsal(_competition);
                }


                GenerateTable();

                BtDorsals.IsEnabled = false;
            }
            catch (NullReferenceException) {
                MessageBox.Show("Escoja primero la competición");
            }
        }

        private void CompetitionList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            _competition = new CompetitionDto {
                ID = _ids[CompetitionList.SelectedIndex]
            };

            GenerateTable();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if (e.PropertyType == typeof(DateTime))
                ((DataGridTextColumn) e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void Dorsals_OnLoaded(object sender, RoutedEventArgs e) {
            if (CompetitionList.HasItems) CompetitionList.SelectedIndex = 0;
        }

        private void BtDorsals_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - e.Delta);
        }
    }
}
