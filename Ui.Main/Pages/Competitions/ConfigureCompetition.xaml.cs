using Logic.Db.Dto;
using Logic.Db.Util.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ui.Main.Pages.Competitions.categories;
using Control = System.Windows.Controls.Control;
using Cursors = System.Windows.Input.Cursors;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using SelectionMode = System.Windows.Controls.SelectionMode;

namespace Ui.Main.Pages.Competitions
{
    /// <summary>
    /// Lógica de interacción para ConfigureCompetition.xaml
    /// </summary>
    public partial class ConfigureCompetition : Page
    {

        private CompetitionService _serviceComp = new CompetitionService();
        private CompetitionService _serviceCompCat = new CompetitionService();
        private CompetitionDto _competition = new CompetitionDto();
        byte[] bytes;
       private IEnumerable<AbsoluteCategory> list;

        public ConfigureCompetition()
        {
            InitializeComponent();
            GridMountain.Visibility = Visibility.Collapsed;
        }

        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {


            OpenFileDialog dialog = new OpenFileDialog {
                Filter = @"PDF Files|*.pdf",
                Multiselect = false
            };

            if (dialog.ShowDialog() == DialogResult.OK) 
            {
                string path = dialog.FileName;
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) 
                {
                    Reglamento.Text = dialog.SafeFileName ?? throw new InvalidOperationException();
                }

                bytes = File.ReadAllBytes(System.IO.Path.GetFullPath(path));
            }
        }
        
        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
           
            _competition.Date = (DateTime) FechaCompeticion.SelectedDate;
            _competition.Km =Double.Parse(Km.Text);
            _competition.Name = Nombre.Text;
            if (MountainIsChecked())
                _competition.Type = TypeCompetition.Mountain;
            else
                _competition.Type = TypeCompetition.Asphalt;
            _competition.NumberPlaces = 150;
            //_competition.Price = 100;
            _competition.Rules = bytes;
            _competition.Status = "OPEN";

            _serviceComp.AddCompetition(_competition);

        }

        private void OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is System.Windows.Controls.Control component)
                component.Cursor = System.Windows.Input.Cursors.Hand;
        }

        private void OnMouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is System.Windows.Controls.Control component)
                component.Cursor = null;
        }

        private bool MountainIsChecked()
        {
            return (bool)RBMountain.IsChecked;
        }

        private bool AsphaltIsChecked()
        {
            return (bool)RBAsphalt.IsChecked;
        }

        private void CalculateDesnivel() {

            if (DPos.Text == "")
                DPos.Text = "0";
            if (DNeg.Text == "")
                DNeg.Text = "0";

            double cant= (Double.Parse(DPos.Text) - Double.Parse(DNeg.Text));
            DTotal.Text = cant.ToString();

        }

        private void CheckBox_OnClick(object sender, RoutedEventArgs e)
        {
            if (MountainIsChecked())
            
                GridMountain.Visibility = Visibility.Visible;               
            
            else
                GridMountain.Visibility = Visibility.Collapsed;
        }

        

        private void DPos_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!DPos.IsFocused)
                CalculateDesnivel();
        }

        private void DNeg_LostFocus(object sender, RoutedEventArgs e)
        {
            if(!DNeg.IsFocused)
            CalculateDesnivel();
        }

        private void ComboBox_Initialized(object sender, EventArgs e)
        { 
            
            list= _serviceCompCat.SelectAllCategories();

            foreach (var c in list)
            {

                Categories.SelectionMode = SelectionMode.Multiple;
                Categories.Items.Add(c);
            }

        }

        private void Categories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtNueva_Click(object sender, RoutedEventArgs e)
        {

            CategoriesDialog catD = new CategoriesDialog();
            catD.ShowDialog();
            Categories.Items.Add(catD.cat);
            Categories.Items.Refresh();
        }

        private void BtPlazo_Click(object sender, RoutedEventArgs e)
        {
            InscriptionDatesDto plazos = new InscriptionDatesDto {
                fechaInicio = (DateTime) InicioPlazo.SelectedDate,
                fechaFin = (DateTime) FinPlazo.SelectedDate,
                devolucion = Devolucion.Text
            };

            Plazos_list.SelectionMode = SelectionMode.Multiple;
            Plazos_list.Items.Add(plazos);

        }

        private void Devolucion_Initialized(object sender, EventArgs e)
        {
            List<string> devoluciones = new List<string> {
                "NO DEVOLUCIÓN",
                "100%",
                "75%",
                "50%",
                "25%"
            };



            foreach (string c in devoluciones)
            {
                Devolucion.Items.Add(c);
            }
           
        }
        private void OnMouseEnter(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = Cursors.Hand;
        }

        private void OnMouseEnterBeam(object sender, System.Windows.Input.MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = Cursors.IBeam;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = null;
        }

        private void OnMouseLeaveBeam(object sender, System.Windows.Input.MouseEventArgs e) {
            if (sender is Control component)
                component.Cursor = null;
        }
    }
}
