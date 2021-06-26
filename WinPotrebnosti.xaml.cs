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
    /// Логика взаимодействия для WinPotrebnosti.xaml
    /// </summary>
    public partial class WinPotrebnosti : Window
    {
        public WinPotrebnosti()
        {
            InitializeComponent();
            FillTable();
        }

        private void FillTable()
        {

            using (AgencyContainer db = new AgencyContainer()) // подключаем БД
            {
                var cperson = (from c in db.Demand
                              select new
                              { Id = c.id, Agents = c.idAgent, Clients = c.idClient, Property = c.idTypeDem, Adres = c.Address, MaxPrice = c.MaxPrice, MinPrice = c.MinPrice }).ToList();
                DGPot.ItemsSource = cperson;  // записываем в дата грид
            }
        }

       

        
       

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddPot pot = new AddPot(); // подключаем экземпляр
            pot.LabAdd.Content = "Добавить объект недвижимости"; //  изменяем Label
            pot.index = true;
            pot.Show(); // показываем экземпляр
            this.Hide(); // скрываем текущее
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            var index = DGPot.SelectedItem; // выбираем строки в дата грид
            if (index != null)
            {
                int id = int.Parse((DGPot.SelectedCells[0].Column.GetCellContent(index) as TextBlock).Text);
                {

                    AddPot pot = new AddPot(); // подключаем окно
                    pot.index = false;  // если фалсе то заполняются поля
                    pot.LabAdd.Content = "Изменение объекта";  // изменение заголовка
                    {
                        using (AgencyContainer db = new AgencyContainer()) // подключение к БД
                        {

                            Demand demand = db.Demand.Find(id); // выбор по ID

                            pot.TBid.Text = demand.id.ToString(); // заполняем ID
                            pot.TBClient.Text = demand.idClient.ToString(); // заполняем город
                            pot.TBAgent.Text = demand.idAgent.ToString(); // заполняем дом
                            pot.TBType.Text = demand.idTypeDem.ToString(); // заполняем номер дома
                            pot.TBAdres.Text = demand.Address.ToString(); // заполняем координаты широты 
                            pot.TBMinPrice.Text = demand.MinPrice.ToString(); // заполняем координаты долготы
                            pot.TBMaxPrice.Text = demand.MaxPrice.ToString(); // заполняем улицу
                            pot.Show(); // показываем окно
                            this.Hide(); // скрываем окно


                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Объект не выбран"); // если ничего не выбрано , то выводим такое сообщение
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DGPot.Items.Count > 0)
            {
                var index = DGPot.SelectedItem;
                if (index != null)
                {
                    int id = int.Parse((DGPot.SelectedCells[0].Column.GetCellContent(index) as TextBlock).Text);
                    using (AgencyContainer db = new AgencyContainer())
                    {
                        DemandsApart apart = db.DemandsApart.Find(id);
                        DemandsHouse house = db.DemandsHouse.Find(id);
                        DemandsLands land = db.DemandsLands.Find(id);
                        Demand demands = db.Demand.Find(id);
                        db.Demand.Remove(demands);
                        db.DemandsApart.Remove(apart);
                        db.DemandsHouse.Remove(house);
                        db.DemandsLands.Remove(land);
                        db.SaveChanges();
                        MessageBox.Show("Потребность удалена");

                    }
                }
            }
        }
    }
}



