using Logic.Db.Dto;
using Logic.Db.Util.Services;
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

namespace Ui.Main.Pages.Inscriptions {
    /// <summary>
    /// Lógica de interacción para CancelInscriptionPage.xaml
    /// </summary>
    public partial class CancelInscriptionPage : Page {
        public static string Dni;

        private readonly EnrollService _enrollService;
        private readonly AthletesService _athletesService;
        private readonly CompetitionService _competitionService;
        private AthleteDto _athlete;

        private CompetitionDto _competition;
        private List<long> _columnIds;
        private List<double> _refunds;
        private List<string> _status;

        public CancelInscriptionPage() {
            _enrollService = new EnrollService(null);
            _athletesService = new AthletesService();
            _competitionService = new CompetitionService();
            InitializeComponent();
        }

        private void PlaceData() {
            TxDni.Text = _athlete.Dni;
            LbNameSurname.Content = _athlete.Name + " " + _athlete.Surname;
            LbBirthDate.Content = _athlete.BirthDate.ToShortDateString();
            if (_athlete.Gender == AthleteDto.MALE)
                LbGender.Content = Properties.Resources.Man;
            else
                LbGender.Content = Properties.Resources.Woman;
        }

        private void LoadData(string dni) {
            List<AthleteDto> atleList = _athletesService.SelectAthleteTable();

            if (dni != null) {
                try {
                    _athlete = atleList.First(a => a.Dni.ToUpper().Equals(dni.ToUpper()));

                    PlaceData();

                    GetListCompetition();

                } catch (InvalidOperationException) { }
            }

        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) {
            if (e.PropertyType == typeof(System.DateTime))
                ((DataGridTextColumn) e.Column).Binding.StringFormat = "dd/MM/yyyy";

            if (e.PropertyType == typeof(double)) {
                ((DataGridTextColumn) e.Column).Binding.StringFormat = "{0:P2}";
            }
        }

        private void TxDni_TextChanged(object sender, TextChangedEventArgs e) {
            LoadData(TxDni.Text);
        }

        private void CancelInscription_OnLoaded(object sender, RoutedEventArgs e) {
            if (_athlete == null || _athlete.Dni == null || Dni == null || !Dni.Equals(_athlete.Dni)) {
                LoadData(Dni);
            }
        }

        private void GetListCompetition() {
            DataTable table = _enrollService.NotCanceledInscriptions(_athlete.Dni);
            table.Columns[0].ColumnName = Properties.Resources.Competition_Id;
            table.Columns[1].ColumnName = Properties.Resources.Competition_Name;
            table.Columns[2].ColumnName = Properties.Resources.Competition_Status;
            table.Columns[3].ColumnName = Properties.Resources.InscriptionDate;
            table.Columns[4].ColumnName = Properties.Resources.Refund;
            table.Columns[5].ColumnName = Properties.Resources.CancelFinishDate;

            _columnIds = table.AsEnumerable()
                .Select(dr => dr.Field<long>(Properties.Resources.Competition_Id)).ToList();

            _refunds = table.AsEnumerable().Select(dr => dr.Field<double>(Properties.Resources.Refund)).ToList();

            _status = table.AsEnumerable().Select(dr => dr.Field<string>(Properties.Resources.Competition_Status)).ToList();

            table.Columns.RemoveAt(0);

            if (CompetitionsToCancel != null)
                CompetitionsToCancel.ItemsSource = table.DefaultView;
        }

        private void CompetitionsToCancel_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int indexSeletected = CompetitionsToCancel.SelectedIndex;

            if (indexSeletected != -1) {
                _competition = new CompetitionDto() {
                    ID = (int) _columnIds[indexSeletected]
                };
            }
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e) {
            if (CompetitionsToCancel.SelectedItem == null) {
                MessageBox.Show(Properties.Resources.NothingSelected);
                return;
            }

            double dev = CalcularDevolucion();
            string message = Properties.Resources.InscriptionCanceled + " " + dev + "€";
            int index = CompetitionsToCancel.SelectedIndex;
            if (_status[index] != "REGISTERED")
                message += "(No se había realizado el pago)";
            MessageBox.Show(message);
            _enrollService.UpdateInscriptionStatus(_athlete.Dni, _competition.ID, "CANCELED");
            _enrollService.UpdateRefund(_athlete.Dni, _competition.ID, dev);

            //actualizar la tabla
            GetListCompetition();
        }

        private double CalcularDevolucion() {
            int index = CompetitionsToCancel.SelectedIndex;
            if (_status[index] != "REGISTERED")
                return 0;
            double precio = _competitionService.SelectCompetitionPrice(_athlete.Dni, _competition.ID);
            double refund = _refunds[index];
            return precio * refund;
        }
    }
}
