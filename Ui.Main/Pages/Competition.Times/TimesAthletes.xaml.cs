using System;
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
    public partial class TimesAthletes : Page {

        private DataTable _table;
        private List<long> _ids;
        private List<string> _list;
        private IEnumerable<CategoryDto> categories;
        public static CompetitionDto Competition;
        public static CategoryDto CategorySelected;
        private readonly CompetitionService _competitionService;
        private readonly TimesService _service;

        public TimesAthletes() {
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


        private void CompetitionList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            LayoutButtons.Children.Clear();
            Button but = GenerateButton();

            CategoryDto cat = new CategoryDto() {
                Name = Properties.Resources.Absolute.ToUpper(),
                MinAge = 18,
                MaxAge = int.MaxValue,
            };

            but.Content = cat.Name;

            Competition = new CompetitionDto() {
                ID = _ids[CompetitionList.SelectedIndex]
            };

            Competition = _competitionService.SearchCompetitionById(Competition);

            categories = _competitionService.SelectAllCategoriesByCompetitionId(Competition);
            categories = categories.Reverse().Append(cat).Reverse();

            int count = 0;
            foreach (CategoryDto category in categories) {
                but = GenerateButton();
                but.Content = category.Name.Replace('_', ' ').ToUpper();
                but.Tag = count++;

                LayoutButtons.Children.Add(but);
            }

            ButtonBase_OnClick(LayoutButtons.Children[0], null);
            CategorySelected = categories.First();
        }

        private Button GenerateButton() {
            Button bt = new Button() {
                VerticalAlignment = VerticalAlignment.Center,
                BorderThickness = new Thickness(0, 0, 0, 0),
                Background = Brushes.Transparent,
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 14,
                Height = 50
            };
            bt.MouseEnter += OnMouseEnter;
            bt.MouseLeave += OnMouseLeave;
            bt.Click += ButtonBase_OnClick;
            return bt;
        }

        private void OnMouseEnter(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = Cursors.Hand;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = null;
        }

        private void TimesAthletes_OnLoaded(object sender, RoutedEventArgs e) {
            if (Competition != null) {
                CompetitionList.SelectedIndex = _ids.IndexOf(Competition.ID);
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            Button bt = (Button) sender;
            if ((Int32?) bt.Tag != null) {
                CategorySelected = categories.ElementAt((int) bt.Tag);

                GenerateDataGrid();
            }
        }

        internal void GenerateDataGrid() {

            if (Competition != null) {
                _table = new DataTable();
                DataColumn column = new DataColumn(Properties.Resources.AthletePosition, typeof(string)) {
                    AllowDBNull = true
                };
                _table.Columns.Add(column);

                _table.Columns[Properties.Resources.AthletePosition].AutoIncrement = true;
                _table.Columns[Properties.Resources.AthletePosition].AutoIncrementSeed = 1;
                _table.Columns[Properties.Resources.AthletePosition].AutoIncrementStep = 1;

                _table.Merge(_service.SelectCompetitionTimes(Competition, CategorySelected));
                _table.Columns[1].ColumnName = Properties.Resources.AthleteDorsal;
                _table.Columns[2].ColumnName = Properties.Resources.AthleteDni;
                _table.Columns[3].ColumnName = Properties.Resources.AthleteName;
                _table.Columns[4].ColumnName = Properties.Resources.AthleteSurname;
                _table.Columns[5].ColumnName = Properties.Resources.AthleteGender;
                _table.Columns[6].ColumnName = Properties.Resources.InitialTime;
                _table.Columns[7].ColumnName = Properties.Resources.FinishTime;

                DataTable dtClone = _table.Clone();
                dtClone.Columns[6].ColumnName = Properties.Resources.Time;
                dtClone.Columns[Properties.Resources.Time].DataType = typeof(string);


                foreach (DataRow row in _table.Rows) {
                    object[] dr = row.ItemArray as object[];
                    if (dr[6] is DBNull || dr[7] is DBNull) {
                        dr[6] = "---";
                    } else {
                        var seconds = (long) dr[7] - (long) dr[6];
                        var timespan = TimeSpan.FromSeconds(seconds);
                        dr[6] = timespan.ToString(@"hh\:mm\:ss");
                    }

                    var desRow = dtClone.NewRow();
                    desRow.ItemArray = dr;
                    dtClone.Rows.Add(desRow);
                }

                //_table.Columns.Remove(Properties.Resources.InitialTime);
                dtClone.Columns.Remove(Properties.Resources.FinishTime);

                _table = dtClone;

                DataGridTimes.ItemsSource = _table.DefaultView;
            }
        }
    }
}
