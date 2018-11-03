using Logic.Db.Dto;
using Logic.Db.Util.Services;
using System;
using System.Collections.Generic;
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

namespace Ui.Main.Pages.Competitions
{
    /// <summary>
    /// Lógica de interacción para ConfigureCompetition.xaml
    /// </summary>
    public partial class ConfigureCompetition : Page
    {

        private CompetitionService _serviceComp = new CompetitionService();
        private CompetitionDto _competition = new CompetitionDto();
        byte[] bytes;

        public ConfigureCompetition()
        {
            InitializeComponent();
        }


        



        private void BtSearch_Click(object sender, RoutedEventArgs e)
        {


            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PDF Files|*.pdf"; 
            dialog.Multiselect = false; 
            if (dialog.ShowDialog() == DialogResult.OK) 
            {
                String path = dialog.FileName;
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) 
                {
                    Reglamento.Text = dialog.SafeFileName;
                }

                bytes = File.ReadAllBytes(System.IO.Path.GetFullPath(path));
            }
        }

       


        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
           
            _competition.Date = (DateTime) FechaCompeticion.SelectedDate;
            _competition.Km =Double.Parse(Km.Text);
            _competition.Name = Nombre.Text;
            if ((bool)RBMountain.IsChecked)
                _competition.Type = TypeCompetition.Mountain;
            else
                _competition.Type = TypeCompetition.Asphalt;
            _competition.NumberPlaces = 150;
            _competition.Price = 100;
            _competition.Rules = bytes;
            _competition.Status = "OPEN";

            _serviceComp.AddCompetition(_competition);

        }



    }
}
