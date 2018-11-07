using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;
using Logic.Db.Dto;

namespace Ui.Main.Pages.Competitions.categories
{
    /// <summary>
    /// Lógica de interacción para NewCategoriesDialog.xaml
    /// </summary>
    public partial class CategoriesDialog : ModernDialog
    {

        public AbsoluteCategory cat = new AbsoluteCategory();
        public CategoryDto femenino = new CategoryDto();
        public CategoryDto masculino = new CategoryDto();


        public CategoriesDialog(AbsoluteCategory category)
        {
            InitializeComponent();
            this.cat = category;
            Nombre.Text = category.Name;
            DesdeF.Text = category.CategoryF.MinAge.ToString();
            HastaF.Text = category.CategoryF.MaxAge.ToString();
            DesdeM.Text = category.CategoryM.MinAge.ToString();
            HastaM.Text = category.CategoryM.MaxAge.ToString();
        }

        private void BtNueva_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(DesdeF.Text)> int.Parse(HastaF.Text) || int.Parse(DesdeM.Text)> int.Parse(HastaM.Text))
            {
                MessageBox.Show(Properties.Resources.InvalidDNI);
                return;
            }

            femenino.MinAge = int.Parse(DesdeF.Text);
            femenino.MaxAge = int.Parse(HastaF.Text);
            femenino.Gender = "F";

            masculino.MinAge = int.Parse(DesdeM.Text);
            masculino.MaxAge = int.Parse(HastaM.Text);
            masculino.Gender = "M";

            cat.CategoryF = femenino;
            cat.CategoryM = masculino;

            Close();
                                           
        }

        private void HastaF_LostFocus(object sender, RoutedEventArgs e)
        {
            if(HastaF.Text=="")
                HastaF.Text = cat.CategoryF.MaxAge.ToString();
            if(DesdeF.Text=="")
                DesdeF.Text = cat.CategoryF.MinAge.ToString();
            if(HastaM.Text=="")
                HastaM.Text = cat.CategoryM.MaxAge.ToString();
            if (DesdeM.Text == "")
                DesdeM.Text = cat.CategoryM.MinAge.ToString();
        }
    }
    }

