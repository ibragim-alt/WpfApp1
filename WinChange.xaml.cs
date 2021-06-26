using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Data.Entity;
using Microsoft.Win32;
using System.Reflection;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для WinChange.xaml
    /// </summary>
    public partial class WinChange : Window
    {
        List<Client> clients = new List<Client>();
        List<Person> persons = new List<Person>();
        public WinChange()
        {
            InitializeComponent();
        }
        
        

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }
    }
}
