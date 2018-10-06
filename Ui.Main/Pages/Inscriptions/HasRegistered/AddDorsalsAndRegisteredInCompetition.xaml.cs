using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
using Logic.Db.ActionObjects.AthleteLogic;
using Logic.Db.Dto;
using Logic.Db.Util.Services;

namespace Ui.Main.Pages.Inscriptions.HasRegistered
{
    /// <summary>
    /// Lógica de interacción para AddDorsalsAndRegisteredInCompetition.xaml
    /// </summary>
    public partial class AddDorsalsAndRegisteredInCompetition : Page {
        private EnrollService _enroll;
        private CompetitionDto _competition;
        private DataTable _table;

        public AddDorsalsAndRegisteredInCompetition()
        {
            InitializeComponent();
        }

        private void UIElement_OnFocusableChanged(object sender, DependencyPropertyChangedEventArgs e) {
            var text = sender as TextBox;

            if (text.Text.Equals("ID")) {
                text.Foreground = new SolidColorBrush(Colors.Black);
                text.Text = string.Empty;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            int id;

            try {
                id = int.Parse(CompetitionIDTextBox.Text);
            } catch (FormatException) {
                MessageBox.Show("Error con la ID");
                return;
            }

            _competition = new CompetitionDto() {
                ID = id
            };

            _enroll = new EnrollService(_competition);

            GenerateTable();

            BtDorsals.IsEnabled = true;
            BtSearch.IsEnabled = false;
        }

        private void GenerateTable() {
            _table = _enroll.SelectAthleteRegistered();

            _table.Columns[0].ColumnName = Properties.Resources.AthleteDni;
            _table.Columns[1].ColumnName = Properties.Resources.Competition_Id;
            _table.Columns[2].ColumnName = Properties.Resources.Competition_Status;
            _table.Columns[3].ColumnName = Properties.Resources.Competition_Date;
            _table.Columns[4].ColumnName = Properties.Resources.AthleteDorsal;

            DataGridCompetition.ItemsSource = _table.DefaultView;
        }

        private void BtDorsals_Click(object sender, RoutedEventArgs e) {
            try {
                _enroll.UpdateAthleteRegisteredDorsal(_competition);

                GenerateTable();

                BtDorsals.IsEnabled = false;
            } catch (NullReferenceException) {
                MessageBox.Show("Introduzca primero la competición");
                return;
            }
        }
    }
}
