using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Logic.Db.Dto;
using Logic.Db.Util.Services;
using Ui.Main.Pages.Competitions.categories;
using Control = System.Windows.Controls.Control;
using Cursors = System.Windows.Input.Cursors;
using MessageBox = System.Windows.Forms.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using SelectionMode = System.Windows.Controls.SelectionMode;

namespace Ui.Main.Pages.Competitions
{
    /// <summary>
    ///     Lógica de interacción para ConfigureCompetition.xaml
    /// </summary>
    public partial class ConfigureCompetition : Page
    {
        private CompetitionDto _competition = new CompetitionDto();
        private readonly CompetitionService _serviceCategories = new CompetitionService();

        private readonly CompetitionService _serviceComp = new CompetitionService();
        private readonly CompetitionService _serviceCompCat = new CompetitionService();
        private EnrollService _serviceEnroll;
        public List<AbsoluteCategory> absolutes = new List<AbsoluteCategory>();
        private byte[] bytes;
        private IEnumerable<AbsoluteCategory> list;
        public List<RefundDto> refundsList = new List<RefundDto>();


        public ConfigureCompetition() {
            InitializeComponent();
            GridMountain.Visibility = Visibility.Collapsed;
            GridInscription.Visibility = Visibility.Visible;
            InicioPlazo.IsEnabled = false;
            FinPlazo.IsEnabled = false;
            FechaCompeticion.DisplayDateStart = DateTime.Now;
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e) {
            var dialog = new OpenFileDialog {
                Filter = @"PDF Files|*.pdf",
                Multiselect = false
            };

            if (dialog.ShowDialog() == DialogResult.OK) {
                var path = dialog.FileName;
                using (var reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) {
                    Reglamento.Text = dialog.SafeFileName ?? throw new InvalidOperationException();
                }

                bytes = File.ReadAllBytes(Path.GetFullPath(path));
            }
        }

        private void OnMouseEnter(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = Cursors.Hand;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = null;
        }

        private bool MountainIsChecked() {
            return (bool) RBMountain.IsChecked;
        }

        private bool AsphaltIsChecked() {
            return (bool) RBAsphalt.IsChecked;
        }

        private bool PreinscriptionIsChecked() {
            return (bool) RBPreinscripcion.IsChecked;
        }


        private void CalculateDesnivel() {
            if (DPos.Text == "")
                DPos.Text = "0";
            if (DNeg.Text == "")
                DNeg.Text = "0";


            var cant = double.Parse(DPos.Text) - double.Parse(DNeg.Text);
            DTotal.Text = cant.ToString();
        }

        private void CheckBox_OnClick(object sender, RoutedEventArgs e) {
            if (MountainIsChecked())

                GridMountain.Visibility = Visibility.Visible;

            else
                GridMountain.Visibility = Visibility.Collapsed;
        }

        private void DPos_LostFocus(object sender, RoutedEventArgs e) {
            if (!DPos.IsFocused)
                CalculateDesnivel();
        }

        private void DNeg_LostFocus(object sender, RoutedEventArgs e) {
            if (!DNeg.IsFocused)
                CalculateDesnivel();
        }

        private void ComboBox_Initialized(object sender, EventArgs e) {
            list = _serviceCompCat.SelectAllCategories();

            foreach (var c in list) {
                Categories.SelectionMode = SelectionMode.Single;
                Categories.Items.Add(c);
            }
        }

        private void BtModificar_Click(object sender, RoutedEventArgs e) {
            if (Categories.SelectedItem != null) {
                var catD = new CategoriesDialog((AbsoluteCategory) Categories.SelectedItem, absolutes);
                catD.ShowDialog();
                absolutes = catD.absolutes;
                Categories.Items.Refresh();
            }
        }

        private void BtReset_Click(object sender, RoutedEventArgs e) {
            list = _serviceCompCat.SelectAllCategories();

            Categories.Items.Clear();

            foreach (var c in list) {
                Categories.SelectionMode = SelectionMode.Single;
                Categories.Items.Add(c);
            }
        }

        private void RBPreinscripcion_Click(object sender, RoutedEventArgs e) {
            if (PreinscriptionIsChecked()) {
                PrecioInscripcion.Watermark = "días";
                LBLPrecio.Content = "Días para formalización";
                LBLPreinscripcion.Content = "Plazo Preinscripción";
                BtBorrar_Click(sender, e);
                FechaCompeticion_LostFocus(sender, e);
            }
            else {
                PrecioInscripcion.Visibility = Visibility.Visible;
                LBLPrecio.Visibility = Visibility.Visible;
                LBLPreinscripcion.Content = "Fecha de Inscripción";
                BtBorrar_Click(sender, e);
                FechaCompeticion_LostFocus(sender, e);
            }
        }


        private void BtPlazo_Click(object sender, RoutedEventArgs e) {
            if (PreinscriptionIsChecked()) {
                if (Plazos_list.Items.Count == 0)
                    try {
                        var plazos = new InscriptionDatesDto {
                            FechaInicio = (DateTime) InicioPlazo.SelectedDate,
                            FechaFin = (DateTime) FinPlazo.SelectedDate,
                            precio = 0
                        };
                        Plazos_list.SelectionMode = SelectionMode.Single;
                        Plazos_list.Items.Add(plazos);
                        FinPlazo.IsEnabled = false;
                        InicioPlazo.IsEnabled = false;
                        BtPlazo.IsEnabled = false;
                    }
                    catch (Exception) { }
            }
            else {
                if (Plazos_list.SelectedItem == null)
                    try {
                        var plazos = new InscriptionDatesDto {
                            FechaInicio = (DateTime) InicioPlazo.SelectedDate,
                            FechaFin = (DateTime) FinPlazo.SelectedDate
                        };
                        if (PrecioInscripcion.Text == "")
                            plazos.precio = 0;
                        else
                            plazos.precio = double.Parse(PrecioInscripcion.Text);
                        Plazos_list.SelectionMode = SelectionMode.Single;
                        Plazos_list.Items.Add(plazos);
                        FinPlazo.SelectedDate = null;
                        InicioPlazo.SelectedDate = plazos.FechaFin.AddDays(1);
                        InicioPlazo.DisplayDateStart = plazos.FechaFin;
                        PrecioInscripcion.Text = null;
                        InicioPlazo.IsEnabled = false;
                        FinPlazo.DisplayDateStart = InicioPlazo.SelectedDate;
                    }
                    catch (Exception) { }
            }
        }

        private void InicioPlazo_GotFocus(object sender, RoutedEventArgs e) {
            FinPlazo.SelectedDate = null;
            FinPlazo.DisplayDateStart = InicioPlazo.SelectedDate;
        }

        private void InicioPlazo_LostFocus(object sender, RoutedEventArgs e) {
            if (InicioPlazo.SelectedDate != null) {
                FinPlazo.IsEnabled = true;
                FinPlazo.DisplayDateStart = InicioPlazo.SelectedDate;
            }
        }

        private void BtRefund_Click(object sender, RoutedEventArgs e) {
            if (Plazos_list.Items.Count > 0 && FechaCompeticion.SelectedDate != null) {
                var nuevo = new InscriptionDatesDto();
                nuevo = (InscriptionDatesDto) Plazos_list.Items.GetItemAt(0);
                nuevo.FechaFin = (DateTime) FechaCompeticion.SelectedDate;
                var refunds = new RefundDialog(nuevo);
                refunds.ShowDialog();
                refundsList = refunds.refunds;
            }
            else {
                MessageBox.Show("Por favor, introduzca primero los plazos de inscripción y la fecha de competición");
            }
        }

        private void FechaCompeticion_LostFocus(object sender, RoutedEventArgs e) {
            if (FechaCompeticion.SelectedDate != null && FechaCompeticion.Text != "") {
                InicioPlazo.IsEnabled = true;
                InicioPlazo.DisplayDateStart = DateTime.Now;
                InicioPlazo.DisplayDateEnd = FechaCompeticion.SelectedDate;
                FinPlazo.DisplayDateEnd = FechaCompeticion.SelectedDate;
            }
            else {
                InicioPlazo.IsEnabled = false;
            }
        }

        private void BtBorrar_Click(object sender, RoutedEventArgs e) {
            Plazos_list.Items.Clear();
            InicioPlazo.SelectedDate = null;
            FinPlazo.SelectedDate = null;
            InicioPlazo.IsEnabled = true;
            FinPlazo.IsEnabled = false;
            InicioPlazo.DisplayDateStart = DateTime.Now;
            BtPlazo.IsEnabled = true;
        }

        private void OnMouseEnter(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = Cursors.Hand;
        }

        private void OnMouseEnterBeam(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = Cursors.IBeam;
        }

        private void OnMouseLeave(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = null;
        }

        private void OnMouseLeaveBeam(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = null;
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e) {
            if (CheckAll()) {
                checkFields();

                _competition.Rules = bytes;
                _competition.Status = "OPEN";
                if (PreinscriptionIsChecked()) {
                    _competition.Preinscription = true;
                    _competition.DaysPreinscription = int.Parse(PrecioInscripcion.Text);
                }
                else {
                    _competition.Preinscription = false;
                }

                checkAges();

                foreach (AbsoluteCategory c in Categories.Items
                ) //modificar las categorias que te devuelve el dialogo no el listbox
                {
                    _serviceCategories.AddCategory(c.CategoryF);
                    _serviceCategories.AddCategory(c.CategoryM);
                }

                //añadir absoluta categoria vincular
                _serviceComp.AddCompetition(_competition);
                _competition.ID = _serviceComp.GetIdCompetition(_competition);
                _serviceEnroll = new EnrollService(_competition);
                foreach (AbsoluteCategory c in Categories.Items) {
                    var idm = _serviceCategories.GetCategory(c.CategoryM);
                    var idf = _serviceCategories.GetCategory(c.CategoryF);
                    var nueva = new AbsoluteCategory {
                        Name = c.Name,
                        CategoryF = idf,
                        CategoryM = idm
                    };

                    _serviceCategories.AddAbsoluteCategory(nueva);
                    long id = _serviceComp.GetIdAbsolute(nueva);

                    _serviceEnroll.EnrollAbsoluteCompetition(_competition.ID, id);
                }


                //vincular refunds y competicion
                foreach (var c in refundsList)
                    _serviceEnroll.EnrollRefundsCompetition(_competition.ID, c.date_refund, c.refund / 100);


                //METER PLAZOS en inscription dates

                foreach (InscriptionDatesDto p in Plazos_list.Items)
                    _serviceComp.AddInscriptionDate(p, _competition);


                MessageBox.Show("Competicion agregada correctamente.");

                clearAll();
            }
            else {
                MessageBox.Show("Por favor, revise que todos los campos se han introducido correctamente");
            }
        }


        private bool CheckAll() {
            if ((FechaCompeticion.SelectedDate == null) | (Km.Text == "") || Nombre.Text == "" ||
                !MountainIsChecked() && !AsphaltIsChecked() || MountainIsChecked() && DTotal.Text == "" ||
                NumeroPlazas.Text == ""
                || Plazos_list.Items.IsEmpty)
                return false;


            return true;
        }


        private void checkAges() {
            for (var i = 0; i < Categories.Items.Count - 1; i++) {
                var a = Categories.Items.GetItemAt(i) as AbsoluteCategory;
                var b = Categories.Items.GetItemAt(i + 1) as AbsoluteCategory;

                if (a.CategoryF.MaxAge != b.CategoryF.MinAge - 1) {
                    MessageBox.Show(
                        "Por favor, compruebe que los rangos de edades se acoplan correctamente y no queda ninguna edad sin incorporar");
                    return;
                }

                if (a.CategoryM.MaxAge != b.CategoryM.MinAge - 1) {
                    MessageBox.Show(
                        "Por favor, compruebe que los rangos de edades se acoplan correctamente y no queda ninguna edad sin incorporar");
                    return;
                }
            }
        }


        private void checkFields() {
            _competition.Date = (DateTime) FechaCompeticion.SelectedDate;
            if (!double.TryParse(Km.Text, out _competition.Km) || double.Parse(Km.Text) < 0) {
                MessageBox.Show("Por favor, introduzca un número de km válido.");
                return;
            }

            _competition.Name = Nombre.Text;
            if (Hitos.Text != "")
                if (!int.TryParse(Hitos.Text, out _competition.NumberMilestone)) {
                    MessageBox.Show("Por favor, introduzca un número de hitos válido.");
                    return;
                }

            if (MountainIsChecked()) {
                _competition.Type = TypeCompetition.Mountain;
                _competition.Slope = double.Parse(DTotal.Text);
            }
            else {
                _competition.Type = TypeCompetition.Asphalt;
            }

            if (!int.TryParse(NumeroPlazas.Text, out _competition.NumberPlaces) || int.Parse(NumeroPlazas.Text) < 0) {
                MessageBox.Show("Por favor, introduzca un número de plazas válido.");
            }
        }

        private void BtValidar_Click(object sender, RoutedEventArgs e) {
            checkAges();
            MessageBox.Show("Todo correcto!");
        }

        private void clearAll() {
            InitializeComponent();
            GridMountain.Visibility = Visibility.Collapsed;
            GridInscription.Visibility = Visibility.Visible;
            InicioPlazo.IsEnabled = false;
            FinPlazo.IsEnabled = false;
            FechaCompeticion.SelectedDate = null;
            FechaCompeticion.DisplayDateStart = DateTime.Now;
            Categories.Items.Clear();
            list = _serviceCompCat.SelectAllCategories();

            _competition = new CompetitionDto();
            Plazos_list.Items.Clear();
            Km.Text = "";
            Hitos.Text = "";
            NumeroPlazas.Text = "";
            Nombre.Text = "";
            DPos.Text = "";
            DNeg.Text = "";
            DTotal.Text = "";
            PrecioInscripcion.Text = "";
            Reglamento.Text = "";
            BtReset_Click(null, null);
        }
    }
}
