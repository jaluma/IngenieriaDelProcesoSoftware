using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Logic.Db;
using Logic.Db.Dto;
using Logic.Db.Util.Services;
using Xceed.Wpf.Toolkit.Core.Converters;

namespace Ui.Main.Pages.Competition.Times
{
    /// <summary>
    /// Lógica de interacción para TimesAthletes.xaml
    /// </summary>
    public partial class PartialTimesAthletes : Page {

        private DataTable _table;
        private List<long> _ids;
        private List<string> _list;
        public static CompetitionDto Competition;
        public static AbsoluteCategory CategorySelected;
        private readonly CompetitionService _competitionService;
        private readonly TimesService _service;
        public static AthleteDto Athlete;

        public PartialTimesAthletes() {
            this.InitializeComponent();

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

            if (_list.Count > 0)
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
            if (Competition != null) {
                CompetitionList.SelectedIndex = _ids.IndexOf(Competition.ID);
            }

            if (Athlete != null) {
                TxDni.Text = Athlete.Dni;
            }
        }

        private void CompetitionList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Competition = new CompetitionDto() {
                ID = _ids[CompetitionList.SelectedIndex]
            };

            Competition = _competitionService.SearchCompetitionById(Competition);

            IEnumerable<PartialTimesDto> lista = _service.SelectPartialTimes(Competition);

            GenerateTable(lista);

            //if (Athlete == null) {
            //    Athlete = new AthletesService().SelectAthleteByDniObject(_table.Rows[0][0] as string);
            //}

            if (Athlete != null) {
                LbNameSurname.Content = $"{Athlete.Name} {Athlete.Surname}";

                var category = _competitionService.SelectCompetitionByAthleteAndCompetition(Competition, Athlete);
                LbCategory.Content = $"{category.Name.Replace('_', ' ')} ({category.MinAge} - {category.MaxAge})";

                HasParticipatedDto p = _service.SelectCompetitionHasParticipated(Competition, Athlete);
                LbTiempoTotal.Content = PartialTimeString(p.FinishTime == 0 ? 0 : p.FinishTime - p.InitialTime);
            }
            
        }

        private void GenerateTable(IEnumerable<PartialTimesDto> lista) {
            _table = new DataTable();
            _table.Columns.Add(Properties.Resources.AthleteDni);
            for (int i = 1; i <= Competition.NumberMilestone; i++) {
                _table.Columns.Add(Properties.Resources.MilestoneNumber + " " + i);
            }

            foreach (var partialTime in lista) {
                object[] row = new object[Competition.NumberMilestone+1];
                row[0] = partialTime.Athlete.Dni;
                GeneratePartialTimes(partialTime, row);
                GeneratePartialTimes(partialTime, row);

                if (Athlete == null || Athlete.Dni.Equals(row[0] as string)) {
                    _table.Rows.Add(row);
                }
            }

            DataGridTimes.ItemsSource = _table.DefaultView;
        }

        private static void GeneratePartialTimes(PartialTimesDto partialTime, object[] row) {
            for (int i = 1; i <= Competition.NumberMilestone; i++) {
                row[i] = PartialTimeString(partialTime.Time[i-1]);
            }
        }

        private static object PartialTimeString(long time) {
            if (time == 0) {
                return "---";
            } else {
                var seconds = time;
                var timespan = TimeSpan.FromSeconds(seconds);
                return timespan.ToString(@"hh\:mm\:ss");
            }
        }

        private void TxDni_TextChanged(object sender, TextChangedEventArgs e) {
            LoadData(TxDni.Text);
        }

        private void LoadData(string dni) {
            if (dni != null) {
                try {
                    List<AthleteDto> atleList = new AthletesService().SelectAthleteTable();
                    Athlete = atleList.First(a => a.Dni.ToUpper().Equals(dni.ToUpper()));
                    LbNameSurname.Content = $"{Athlete.Name} {Athlete.Surname}";

                    var category = _competitionService.SelectCompetitionByAthleteAndCompetition(Competition, Athlete);
                    LbCategory.Content = $"{category.Name.Replace('_', ' ')} ({category.MinAge} - {category.MaxAge})";

                    HasParticipatedDto p = _service.SelectCompetitionHasParticipated(Competition, Athlete);
                    LbTiempoTotal.Content = PartialTimeString(p.FinishTime == 0 ? 0 : p.FinishTime - p.InitialTime);

                    PartialTimesDto partial  = _service.SelectPartialTimesByAthlete(Competition, Athlete);

                    IEnumerable<PartialTimesDto> lista = new List<PartialTimesDto>() {
                        partial
                    };
                    GenerateTable(lista);

                } catch(InvalidOperationException) { }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            Athlete = null;
            TxDni.Text = "";
            LbNameSurname.Content = "";
            LbCategory.Content = "";
            LbTiempoTotal.Content = "";
            CompetitionList_SelectionChanged(null, null);
        }
    }
    
}
