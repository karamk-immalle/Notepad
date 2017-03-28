using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace WpfApplication5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Persoon> ps = new List<Persoon>();
        private string currentFile = "";
        private string initialDir;
        
        public MainWindow()
        {
            InitializeComponent();
            initialDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ps.Add(new Persoon() { Voornaam = "Jos", Achternaam = "Joske" });
            datagrid.ItemsSource = ps;
            string fileContents = textBox.Text;
        }
       

        
        private void OpenItem_Click(object sender, RoutedEventArgs e)
        {
            StreamReader inputStream;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = initialDir;
            if (dialog.ShowDialog() == true)
            {
                currentFile = dialog.FileName;
                inputStream = File.OpenText(currentFile);
                textBox.Text = inputStream.ReadToEnd();
                inputStream.Close();
            }
        }

        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile == "")
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.InitialDirectory = initialDir;
                if (dialog.ShowDialog() == true)
                {
                    currentFile = dialog.FileName;
                }
            }
            StreamWriter outputStream = File.CreateText(currentFile);
            outputStream.Write(textBox.Text);
            outputStream.Close();
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HelpItem_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Parse_Click(object sender, RoutedEventArgs e)
        {
            var parsedPersonen = new List<Persoon>();

            string[] filedata = textBox.Text.Split('\n');
            try
            {
                foreach (var row in filedata)
                {
                    // MessageBox.Show(String.Format("[{0}]", row.Trim()));

                    // is row valid?
                    string[] fields = row.Trim().Split(';');

                    if (fields.Length != 3)
                    {
                        MessageBox.Show(
                            String.Format("Couldn't parse [{0}]. Number of fields: {1}.",
                                row.Trim(),
                                fields.Length)
                        );
                    }
                    else if (row.Trim() == "")
                    {
                        // ignore empty rows
                    }
                    else
                    {
                        var p = new Persoon();
                        p.Voornaam = fields[0];
                        p.Achternaam = fields[1];
                        p.GeboorteDatum = DateTime.Parse(fields[2]);
                        parsedPersonen.Add(p);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            ps.AddRange(parsedPersonen);
            datagrid.ItemsSource = ps;
        }
    }
}
