using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Logic.Db.Dto;
using Logic.Db.Util.Services;

namespace Ui.Main.Pages.Competition.Times
{
    /// <summary>
    ///     Lógica de interacción para TimesAthletes.xaml
    /// </summary>
    public partial class PartialTimesAthletes : Page
    {
        public static CompetitionDto Competition;
        public static AbsoluteCategory CategorySelected;
        public static AthleteDto Athlete;
        private CompetitionService _competitionService;
        private List<long> _ids;
        private List<string> _list;
        private TimesService _service;

        private DataTable _table;

        public PartialTimesAthletes() {
            InitializeComponent();

            Initialize();
        }

        private void Initialize() {
            _service = new TimesService();

            _competitionService = new CompetitionService();
            _table = _competitionService.SelectCompetitionFinish();


            _list = new List<string>();
            _ids = new List<long>();

            foreach (DataRow row in _table.Rows) {
                _ids.Add(long.Parse(row[_table.Columns.IndexOf("Competition_ID")].ToString()));
                _list.Add(row[_table.Columns.IndexOf("Competition_Name")].ToString());
            }

            CompetitionList.ItemsSource = _list;

            if (_list.Count > 0 && Competition == null)
                CompetitionList.SelectedIndex = 0;
        }

        private void OnMouseEnter(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = Cursors.Hand;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = null;
        }

        private void PartialTimesAthletes_OnLoaded(object sender, RoutedEventArgs e) {
            Initialize();

            if (Competition != null) CompetitionList.SelectedIndex = _ids.IndexOf(Competition.ID);

            if (Athlete != null) TxDni.Text = Athlete.Dni;
        }

        private void CompetitionList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Competition = new CompetitionDto {
                ID = _ids[CompetitionList.SelectedIndex]
            };

            Competition = _competitionService.SearchCompetitionById(Competition);

            var lista = _service.SelectPartialTimes(Competition);

            GenerateTable(lista);

            //if (Athlete == null) {
            //    Athlete = new AthletesService().SelectAthleteByDniObject(_table.Rows[0][0] as string);
            //}

            if (Athlete != null) {
                LbNameSurname.Content = $"{Athlete.Name} {Athlete.Surname}";

                var category = _competitionService.SelectCompetitionByAthleteAndCompetition(Competition, Athlete);
                LbCategory.Content = $"{category.Name.Replace('_', ' ')} ({category.MinAge} - {category.MaxAge})";

                var p = _service.SelectCompetitionHasParticipated(Competition, Athlete);
                var time = PartialTimeString(p.FinishTime == 0 ? 0 : p.FinishTime - p.InitialTime);
                LbTiempoTotal.Content = $"T. Final: {time}";
            }
        }

        private void GenerateTable(IEnumerable<PartialTimesDto> lista) {
            _table = new DataTable();
            _table.Columns.Add(Properties.Resources.AthleteDni);
            _table.Columns.Add(Properties.Resources.AthleteDorsal);
            _table.Columns.Add(Properties.Resources.InitialTime);
            for (var i = 2; i <= Competition.NumberMilestone + 1; i++)
                _table.Columns.Add(Properties.Resources.MilestoneNumber + " " + (i - 1));
            _table.Columns.Add(Properties.Resources.FinishTime);

            foreach (var partialTime in lista) {
                var row = new object[Competition.NumberMilestone + 2 +
                                     2]; // +1 por tener el dni y dorsal // +2 por los tiempos
                if (partialTime.Dorsal == 0) continue;

                row[0] = partialTime.Athlete.Dni;
                row[1] = partialTime.Dorsal;
                row[2] = PartialTimeString(partialTime.InitialTime, false);
                GeneratePartialTimes(partialTime, row);
                row[Competition.NumberMilestone + 3] = PartialTimeString(partialTime.FinishTime);

                if (Athlete == null || Athlete.Dni.Equals(row[0] as string)) _table.Rows.Add(row);
            }

            _table.Columns.Remove(Properties.Resources.AthleteDni);

            _table.DefaultView.Sort = Properties.Resources.FinishTime;
            DataGridTimes.ItemsSource = _table.DefaultView;
        }

        private static void GeneratePartialTimes(PartialTimesDto partialTime, object[] row) {
            for (var i = 3; i <= Competition.NumberMilestone + 2; i++)
                row[i] = PartialTimeString(partialTime.Time[i - 3]);
        }

        private static object PartialTimeString(long time, bool format = true) {
            if (time == 0 && format) return "---";

            var seconds = time;
            var timespan = TimeSpan.FromSeconds(seconds);
            return timespan.ToString(@"hh\:mm\:ss");
        }

        private void TxDni_TextChanged(object sender, TextChangedEventArgs e) {
            LoadData(TxDni.Text);
        }

        private void LoadData(string dni) {
            if (dni != null)
                try {
                    var atleList = new AthletesService().SelectAthleteTable();
                    Athlete = atleList.First(a => a.Dni.ToUpper().Equals(dni.ToUpper()));
                    LbNameSurname.Content = $"{Athlete.Name} {Athlete.Surname}";

                    var category = _competitionService.SelectCompetitionByAthleteAndCompetition(Competition, Athlete);
                    LbCategory.Content = $"{category.Name.Replace('_', ' ')} ({category.MinAge} - {category.MaxAge})";

                    var p = _service.SelectCompetitionHasParticipated(Competition, Athlete);
                    var time = PartialTimeString(p.FinishTime == 0 ? 0 : p.FinishTime - p.InitialTime);
                    LbTiempoTotal.Content = $"T. Final: {time}";

                    var partial = _service.SelectPartialTimesByAthlete(Competition, Athlete);

                    IEnumerable<PartialTimesDto> lista = new List<PartialTimesDto> {
                        partial
                    };
                    GenerateTable(lista);
                }
                catch (InvalidOperationException) { }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            Athlete = null;
            TxDni.Text = "";
            LbNameSurname.Content = "";
            LbCategory.Content = "";
            LbTiempoTotal.Content = "";
            CompetitionList_SelectionChanged(null, null);
        }

        private void DataGridTimes_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - e.Delta);
        }
    }
}
