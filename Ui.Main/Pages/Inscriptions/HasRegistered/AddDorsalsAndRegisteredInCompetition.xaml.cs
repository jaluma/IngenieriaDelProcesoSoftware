using System;
using System.Collections.Generic;
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
            
            DataGridCompetition.ItemsSource = _enroll.SelectAthleteRegistered().DefaultView;

            BtDorsals.IsEnabled = true;
            BtSearch.IsEnabled = false;
        }

        private void BtDorsals_Click(object sender, RoutedEventArgs e) {
            try {
                _enroll.UpdateAthleteRegisteredDorsal(_competition);

                DataGridCompetition.ItemsSource = _enroll.SelectAthleteRegistered().DefaultView;

                BtDorsals.IsEnabled = false;
            } catch (NullReferenceException) {
                MessageBox.Show("Introduzca primero la competición");
                return;
            }
        }
    }
}
