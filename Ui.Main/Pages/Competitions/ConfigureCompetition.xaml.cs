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
            dialog.Filter = "PDF Files|*.pdf"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) // do anything you want, e.g. read it
                {
                    LbReglamento.Text = dialog.SafeFileName;
                    LbReglamento1.Text = System.IO.Path.GetFullPath(path);
                }

                bytes = File.ReadAllBytes(System.IO.Path.GetFullPath(path));
                



            }
        }

       


        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {

            _competition.Date = (DateTime)FechaCompeticion.SelectedDate;
            _competition.Km =Double.Parse(Km.Text);
            _competition.Name = Nombre.Text;           
            _competition.NumberPlaces = 150;
            _competition.Price = 100;
            _competition.Rules = bytes;
            _competition.Status = "OPEN";

            _serviceComp.AddCompetition(_competition);

        }



    }
}
