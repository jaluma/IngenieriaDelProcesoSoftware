using System.Collections.Generic;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using Logic.Db.Dto;

namespace Ui.Main.Pages.Competitions.categories
{
    /// <summary>
    ///     Lógica de interacción para NewCategoriesDialog.xaml
    /// </summary>
    public partial class CategoriesDialog : ModernDialog
    {
        public List<AbsoluteCategory> absolutes;

        public AbsoluteCategory cat = new AbsoluteCategory();
        public CategoryDto femenino = new CategoryDto();
        public CategoryDto masculino = new CategoryDto();
        public int nuevoF;
        public int nuevoM;


        public CategoriesDialog(AbsoluteCategory category, List<AbsoluteCategory> absolutes) {
            InitializeComponent();
            cat = category;
            this.absolutes = absolutes;
            Nombre.Text = category.Name;

            DesdeF.Text = category.CategoryF.MinAge.ToString();
            HastaF.Text = category.CategoryF.MaxAge.ToString();
            DesdeM.Text = category.CategoryM.MinAge.ToString();
            HastaM.Text = category.CategoryM.MaxAge.ToString();
        }

        private void BtNueva_Click(object sender, RoutedEventArgs e) {
            if (int.Parse(DesdeF.Text) > int.Parse(HastaF.Text) || int.Parse(DesdeM.Text) > int.Parse(HastaM.Text)) {
                MessageBox.Show("Edades incoherentes");
                return;
            }

            cat.Name = Nombre.Text;

            femenino.MinAge = int.Parse(DesdeF.Text);
            femenino.MaxAge = int.Parse(HastaF.Text);
            femenino.Gender = "F";
            femenino.Name = cat.Name + "_F";

            masculino.MinAge = int.Parse(DesdeM.Text);
            masculino.MaxAge = int.Parse(HastaM.Text);
            masculino.Gender = "M";
            masculino.Name = cat.Name + "_M";

            cat.CategoryF = femenino;
            cat.CategoryM = masculino;

            absolutes.Add(cat);

            Close();
        }

        private void HastaF_LostFocus(object sender, RoutedEventArgs e) {
            if (HastaF.Text == "")
                HastaF.Text = cat.CategoryF.MaxAge.ToString();
            if (DesdeF.Text == "")
                DesdeF.Text = cat.CategoryF.MinAge.ToString();
            if (HastaM.Text == "")
                HastaM.Text = cat.CategoryM.MaxAge.ToString();
            if (DesdeM.Text == "")
                DesdeM.Text = cat.CategoryM.MinAge.ToString();
        }
    }
}
