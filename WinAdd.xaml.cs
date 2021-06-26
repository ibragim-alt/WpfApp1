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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для WinAdd.xaml
    /// </summary>
    public partial class WinAdd : Window
    {
        List<Client> clients = new List<Client>();
        List<Person> persons = new List<Person>();
        public WinAdd()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (TBEmail.Text.Length >0 || TBPhone.Text.Length > 0)    // проверка на наличие Email или Телефона
            {
                
                
                    using (AgencyContainer db = new AgencyContainer())
                    {
                        Client client = new Client();    // подключение таблицы Client
                        Person person = new Person();    // подключение табилцы Person
                        client.id = int.Parse(TBId.Text);    // сохранение записи из TextBox
                        person.id = int.Parse(TBId.Text);   // сохранение записи из TextBox
                        client.Phone = TBPhone.Text;// сохранение записи из TextBox
                        client.Email = TBEmail.Text;
                        person.LastName = TBLastname.Text;// сохранение записи из TextBox
                        person.FirstName = TBFirstname.Text;// сохранение записи из TextBox
                        person.MiddleName = TBMiddlename.Text;// сохранение записи из TextBox
                        db.Client.Add(client);
                        db.Person.Add(person);
                        db.SaveChanges();
                        MessageBox.Show("Данные добавлены");   // вывод сообщения
                        clients.Clear();   // очистка данных для новых записей
                        persons.Clear();     // очистка данных для новых записей
                        foreach (Person y in db.Person)
                            persons.Add(y);
                        foreach (Client u in db.Client)
                            clients.Add(u);
                        MainWindow main = new MainWindow();     // подключение окна
                        main.DGClient.ItemsSource = clients;    // вывод созданой записи в DataGrid
                    }
                
            }
            else
            {
                MessageBox.Show("Заполните Email или Телефон");   // вывод сообщения
                return;   // не позволяет создать запись без заполнения необходимых полей
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }
    }
}
