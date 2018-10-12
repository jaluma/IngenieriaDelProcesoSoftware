using Logic.Db.Dto;
using Logic.Db.Util.Services;
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

namespace Ui.Main.Pages.Inscriptions
{
    /// <summary>
    /// Lógica de interacción para CompetitionSelectionPage.xaml
    /// </summary>
    public partial class CompetitionSelectionWindow : Window
    {
        private readonly CompetitionService _competitionService;
        private readonly AthleteDto _athlete;

        private CompetitionDto _competition;

        public CompetitionSelectionWindow(AthleteDto athlete)
        {
            InitializeComponent();

            _athlete = athlete;

            TxNameSurname.Text = _athlete.Name + " " + _athlete.Surname;
            TxDni.Text = _athlete.Dni;
            TxBirthDate.Text = _athlete.BirthDate.ToShortDateString();
            TxGender.Text = _athlete.Gender.ToString();


            _competitionService = new CompetitionService();
            System.Data.DataTable table = _competitionService.ListCompetitionsToInscribe();
            table.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Name;
            table.Columns[2].ColumnName = Properties.Resources.Competition_Type;
            table.Columns[3].ColumnName = Properties.Resources.Competition_Km;
            table.Columns[4].ColumnName = Properties.Resources.Competition_Price;
            table.Columns[5].ColumnName = Properties.Resources.InscriptionOpen;
            table.Columns[6].ColumnName = Properties.Resources.InscriptionClose;
            table.Columns[7].ColumnName = Properties.Resources.Competition_Date;

            CompetitionsToSelect.ItemsSource = table.DefaultView;

            DataGrid dg = new DataGrid();
            dg.DataContext = table;
            dg.Columns[0].Visibility = Visibility.Hidden;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                ((DataGridTextColumn)e.Column).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void BtFinish_Click(object sender, RoutedEventArgs e)
        {
            if (CompetitionsToSelect.SelectedItem == null)
            {
                MessageBox.Show(Properties.Resources.NothingSelected);
                return;
            }
            EnrollService enrollService = new EnrollService(_competition);
            enrollService.InsertAthleteInCompetition(_athlete, _competition);
            Content = new Frame()
            {
                Content = new InscriptionProofWindow(_athlete, _competition)
            };
        }

        private void CompetitionsToSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            _competition = new CompetitionDto()
            {

            };
        }
    }
}
