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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Client> clients = new List<Client>();
        List<Person> persons = new List<Person>();
        public MainWindow()
        {
            InitializeComponent();
            FillTable();
            FillTableAgents();
        }

        private void FillTable()
        {

            using (AgencyContainer db = new AgencyContainer())   // подключение БД
            {
                var person = (from c in db.Client
                                     from v in db.Person
                                     where c.id == v.id
                                     select new
                                     {
                                         id = c.id,
                                         FirstName = v.FirstName,
                                         LastName = v.LastName,
                                         MiddleName = v.MiddleName,
                                         Phone= c.Phone,
                                         Email = c.Email
                                     }).ToList();             
                DGClient.ItemsSource = person;     // вывод результата в DataGrid

                
            }
        }

        public void FillTableAgents()
        {
             
            using (AgencyContainer db = new AgencyContainer())
            {
                var polis = (from i in db.Person
                             from b in db.Agents
                             where i.id == b.id
                             select new
                             {
                                 id = i.id,
                                 FirstName = i.FirstName,
                                 LastName = i.LastName,
                                 MiddleName = i.MiddleName,
                                 DealShare = b.DealShare
                             }).ToList();

                DGAgent.ItemsSource = polis;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            WinAdd winAdd = new WinAdd(); // подключение окна WinAdd
            winAdd.Show();  // показ окна winAdd
           
        }

        private void ButtonСhange_Click(object sender, RoutedEventArgs e)
        {
            if (DGClient.Items.Count > 0)
            {
                var index = DGClient.SelectedItem;
                if (index != null)
                {
                    int id = int.Parse((DGClient.SelectedCells[0].Column.GetCellContent(index) as TextBlock).Text);

                    WinChange change = new WinChange();
                    {
                        using (AgencyContainer db = new AgencyContainer())
                        {
                            Client client = db.Client.Find(id);
                            Person person = db.Person.Find(id);

                            change.TBId.Text = client.id.ToString();
                            change.TBId.Text = person.id.ToString();
                            change.TBPhone.Text = client.Phone.ToString();
                            change.TBEmail.Text = client.Email.ToString();
                            change.TBLastname.Text = person.LastName.ToString();
                            change.TBFirstname.Text = person.FirstName.ToString();
                            change.TBMiddlename.Text = person.MiddleName.ToString();

                            if (!change.ShowDialog().HasValue) return;

                            client.id = int.Parse(change.TBId.Text);
                            person.id = int.Parse(change.TBId.Text);
                            client.Phone = change.TBPhone.Text;
                            client.Email = change.TBEmail.Text;
                            person.LastName = change.TBLastname.Text;
                            person.FirstName = change.TBFirstname.Text;
                            person.MiddleName = change.TBMiddlename.Text;

                            db.SaveChanges();   // сохранение
                            FillTable();
                            
                        }
                    }
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)   // событийная процедура Click
        {
            if (DGClient.Items.Count > 0)
            {
                var index = DGClient.SelectedItem;
                if (index !=null)
                {
                    int id = int.Parse((DGClient.SelectedCells[0].Column.GetCellContent(index) as TextBlock).Text);
                    using (AgencyContainer db = new AgencyContainer())
                    {
                        Client client = db.Client.Find(id);   // поиск по ID
                        Person person = db.Person.Find(id);  // поиск по ID
                        db.Client.Remove(client);    // удаление записи 
                        db.Person.Remove(person);    // удаление записи
                        db.SaveChanges();
                    }
                }
            }
        }

        private void ButtonAddAgent_Click(object sender, RoutedEventArgs e)
        {
            WinAgent winAgent = new WinAgent(); // подключаем экземпляр
            winAgent.LabelAdd.Content = "Создание клиента"; //  изменяем Label
            winAgent.index = true; 
            winAgent.Show(); // показываем экземпляр
            this.Hide(); // скрываем текущее
        }

        private void ButtonСhangeAgent_Click(object sender, RoutedEventArgs e)
        {
            var index = DGAgent.SelectedItem;
            if (index != null)
            {
                int id = int.Parse((DGAgent.SelectedCells[0].Column.GetCellContent(index) as TextBlock).Text);
                {
                    WinAgent win = new WinAgent(); // создаем экземпляр 
                    win.index = false; // если false то идет изменение клиента
                    win.LabelAdd.Content = "Изменение клиента"; // изменение  заголовка
                                      
                    using (AgencyContainer db = new AgencyContainer()) // подключение к БД 
                    {
                        Person person = db.Person.Find(id); // поиск по ID
                        Agents agents = db.Agents.Find(id); // поиск по ID
                        win.TBidAgent.Text = person.id.ToString(); // изменяем ID
                        win.TBlastAgent.Text = person.LastName; // изменяем фамилию 
                        win.TBfirstAgent.Text = person.FirstName; // изменяем имя
                        win.TBmiddleAgent.Text = person.MiddleName; // изменяем отчество
                        win.TBdealshare.Text = agents.DealShare.ToString(); // изменяем коэффициент

                        win.Show(); // показываем экземпляр 
                        this.Hide(); // скрываем текущее

                    }
                }
            }
            else
            {
                MessageBox.Show("Агент не выбран"); // вывод сообщения
            }
        }

        private void ButtonDeleteAgent_Click(object sender, RoutedEventArgs e)
        {
            if (DGAgent.Items.Count > 0)
            {
                var index = DGAgent.SelectedItem;
                if (index != null)
                {
                    int id = int.Parse((DGAgent.SelectedCells[0].Column.GetCellContent(index) as TextBlock).Text); // поиск по ID
                    using (AgencyContainer db = new AgencyContainer()) // подключение к БД
                    {
                        Agents agents = db.Agents.Find(id);   // поиск по ID
                        Person person = db.Person.Find(id);  // поиск по ID
                        db.Agents.Remove(agents);    // удаление записи 
                        db.Person.Remove(person);    // удаление записи
                        db.SaveChanges(); // сохраняет в БД
                    }   MessageBox.Show("Агент удален"); // вывод сообщения
                }
                
            }
            
        }

        private void ButtonObject_Click(object sender, RoutedEventArgs e)
        {
            ObjectHouse ob = new  ObjectHouse();
            ob.Show();
            this.Close();
        }

        private void ButtonPotrebnosti_Click(object sender, RoutedEventArgs e)
        {
            WinPotrebnosti winPotrebnosti = new WinPotrebnosti();
            winPotrebnosti.Show();
            this.Close();
        }
    }
}
