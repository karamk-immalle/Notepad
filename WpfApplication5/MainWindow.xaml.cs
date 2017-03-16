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
        private string currentFile = "";
        private string initialDir;
        public MainWindow()
        {
            InitializeComponent();
            initialDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
            this.Close();
        }

        private void HelpItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
