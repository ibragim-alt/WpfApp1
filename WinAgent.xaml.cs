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
    /// Логика взаимодействия для WinAgent.xaml
    /// </summary>
    public partial class WinAgent : Window
    {
        public bool index;
        public WinAgent()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (TBlastAgent.Text.Length != 0) // проверка на заполненность
            {
                if (TBfirstAgent.Text.Length != 0) // проверка на заполненность
                {
                    if (TBmiddleAgent.Text.Length != 0) // проверка на заполненность
                    {
                        using (AgencyContainer db = new AgencyContainer()) // подключаем БД Агенство
                        {


                            if (index == true)
                            {
                                Person person = new Person(); // подключение таблицы Person
                                Agents agents = new Agents(); // подключение таблицы Agents
                                person.id = int.Parse(TBidAgent.Text); // заполняем id
                                agents.id = int.Parse(TBidAgent.Text); // заполняем id
                                person.LastName = TBlastAgent.Text;  // заполняем фамилию
                                person.FirstName = TBfirstAgent.Text; // заполняем имя
                                person.MiddleName = TBmiddleAgent.Text; // заполняем отчество
                                agents.DealShare = double.Parse(TBdealshare.Text); // заполняем коэффициент
                                db.Agents.Add(agents); // заполняем таблицу
                                db.Person.Add(person); // заполняем таблицу
                                db.SaveChanges();  // сохраняем в БД
                                MessageBox.Show("Агент добавлен");  // вывод сообщения

                            }
                            if (index == false)
                            {
                                Person person = db.Person.Find(int.Parse(TBidAgent.Text)); // поиск по id
                                Agents agents = db.Agents.Find(int.Parse(TBidAgent.Text)); // поиск по id
                                person.LastName = TBlastAgent.Text; // изменяем фамилию
                                person.FirstName = TBfirstAgent.Text; // изменяем имя
                                person.MiddleName = TBmiddleAgent.Text; // изменяем отчество
                                agents.DealShare = double.Parse(TBdealshare.Text); // изменяем коифициент
                                db.SaveChanges(); // сохраняем в БД
                                MessageBox.Show("Агент изменен"); // вывод сообщения
                            }



                        }
                        MainWindow main = new MainWindow(); // объявляем экземпляр
                        main.Show(); // показываем экземпляр
                        this.Hide();  // скрываем текущее
                    }
                    else
                    {
                        MessageBox.Show("Поле Отчество не заполнено"); // вывод сообщения 
                    }
                }
                else
                {
                    MessageBox.Show("Поле Имя не заполнено"); // вывод сообщения 
                }
            }
            else
            {
                MessageBox.Show("Поле Фамилия не заполнено");  // вывод сообщения 
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(); // объявляем экземпляр
            mainWindow.Show(); // показываем экземпляр
            this.Close(); // закрываем текущее
        }

        private void TBdealshare_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text,0)) // если введен символ то он закрыт
            {
                e.Handled = false;

            }
            else
            {
                e.Handled = true;
            }
        }
    }   
}
