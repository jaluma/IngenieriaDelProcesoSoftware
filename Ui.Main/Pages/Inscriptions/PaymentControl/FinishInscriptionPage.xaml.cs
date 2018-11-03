﻿using System;
using System.Collections.Generic;
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
using Logic.Db.Dto;

namespace Ui.Main.Pages.Inscriptions.InscriptionsPaidControl
{
    /// <summary>
    /// Lógica de interacción para FinishInscriptionPage.xaml
    /// </summary>
    public partial class FinishInscriptionPage : Page
    {

        private string _file;

        public FinishInscriptionPage()
        {
            InitializeComponent();
        }

        private void BtSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "*.csv|*.CSV";
            openFile.Multiselect = false;
            openFile.ShowDialog();
            _file = openFile.FileName;
            txFileName.Text = _file;
        }

        private List<string[]> LeerExtracto(string file)
        {
            List<string[]> list = new List<string[]>();
            using (StreamReader readFile = new StreamReader(file))
            {
                string line;
                string[] row;

                while ((line = readFile.ReadLine()) != null)
                {
                    row = line.Split(',');
                    list.Add(row);
                }
            }
            return list;
        }

        private void BtActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (_file == null || _file.Equals(""))
            {
                System.Windows.MessageBox.Show(Properties.Resources.NotSelectedFile);
                return;
            }
                
            List<PaymentDto> list = new List<PaymentDto>();
            foreach (string[] s in LeerExtracto(_file))
            {
                PaymentDto dto = new PaymentDto()
                {
                    Dni = s[0],
                    Name = s[1],
                    Surname = s[2],
                    Date = DateTime.Parse(s[3]),
                    Amount = float.Parse(s[4])
                };
                list.Add(dto);
            }


        }
    }
}