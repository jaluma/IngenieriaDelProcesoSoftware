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

        public CategoryDto cat = new CategoryDto();

        public CategoriesDialog()
        {
            InitializeComponent();
            
        }

        private void BtNueva_Click(object sender, RoutedEventArgs e)
        {
            
            
            cat.Name = Nombre.Text;
            //cat.Min_Age = int.Parse(Desde.Text);
            //cat.Min_Age = int.Parse(Hasta.Text);
            Close();
                                           
        }

        

        
    }
    }

