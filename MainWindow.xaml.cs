using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace OrgProcessor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Organization> lista = new List<Organization>();
        List<string> orszagLista = new List<string>();
        List<string> evLista = new List<string>();
        string fileName = "organizations-100.csv";
        public MainWindow()
        {
            AdattalBetolt();
            InitializeComponent();
            dtgAdatok.ItemsSource = lista;
            EvekkelFeltolt();
        }
        public void AdattalBetolt()
        {
            string line = "";
            StreamReader sr = new StreamReader(fileName);
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "Index;Organization Id;Name;Website;Country;Description;Founded;Industry;Number of employees")
                {
                    continue;
                }
                Organization elem = new Organization(line.Split(';'));
                lista.Add(elem);
            }
            sr.Close();
        }
        public void EvekkelFeltolt()
        {
            lista.GroupBy(x => x.Country).DistinctBy(x => x.Key).ToList().ForEach(x => cmbCountryName.Items.Add(x.Key));
            lista.GroupBy(x => x.Founded).DistinctBy(x => x.Key).ToList().ForEach(x => cmbFoundingDate.Items.Add(x.Key));

        }


        private void selectedDataExport(object sender, SelectionChangedEventArgs e)
        {
            if (dtgAdatok.SelectedItem is Organization) 
            {
                Organization valasztottObjektum = dtgAdatok.SelectedItem as Organization;
            lblAzonosytó.Content = "Azonosító: " + valasztottObjektum.Id.ToString();
            lblWebcim.Content = "Weboldal: "+valasztottObjektum.Website;
            lblLeiras.Content = "Leírás: " + valasztottObjektum.Description;
            
            }
            else
            {
                MessageBox.Show("Hiba!");
            }

        }
    }
}
