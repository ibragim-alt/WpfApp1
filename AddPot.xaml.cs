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
    /// Логика взаимодействия для AddPot.xaml
    /// </summary>
    public partial class AddPot : Window
    {
        public bool index;
        public AddPot()
        {
            InitializeComponent();
            ButtonChange.IsEnabled = false;
            TBType.IsEnabled = false;
            TBAgent.IsEnabled = false;
            TBClient.IsEnabled = false;
            FillComboBoxAgent();
            FillComboBoxClient();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            //Добавление потребности
            if (ComboBoxNeeds.SelectedIndex == 0)
            {

                using (AgencyContainer db = new AgencyContainer())
                {
                    Demand dem = new Demand();
                    DemandsHouse housedem = new DemandsHouse();
                    dem.id = int.Parse(TBid.Text);
                    dem.idTypeDem = int.Parse(TBType.Text);
                    dem.MaxPrice = int.Parse(TBMaxPrice.Text);
                    dem.MinPrice = int.Parse(TBMinPrice.Text);
                    dem.idAgent = int.Parse(TBAgent.Text);
                    dem.idClient = int.Parse(TBClient.Text);
                    housedem.id = int.Parse(TBObject.Text);
                    housedem.MaxArea = double.Parse(TBMaxArea.Text);
                    housedem.MinArea = double.Parse(TBMinArea.Text);
                    housedem.MinFloors = int.Parse(TBMinFloor.Text);
                    housedem.MaxFloors = int.Parse(TBMaxFloor.Text);
                    housedem.MaxRooms = int.Parse(TBMaxRooms.Text);
                    housedem.MinRooms = int.Parse(TBMinRooms.Text);
                    db.Demand.Add(dem);
                    db.DemandsHouse.Add(housedem);
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены");


                }
            }
            if (ComboBoxNeeds.SelectedIndex == 1)
            {
                using (AgencyContainer db = new AgencyContainer())
                {
                    Demand dem = new Demand();
                    DemandsLands landsdem = new DemandsLands();
                    dem.id = int.Parse(TBid.Text);
                    dem.idTypeDem = int.Parse(TBType.Text);
                    dem.MaxPrice = int.Parse(TextBoxMaxPrice.Text);
                    dem.MinPrice = int.Parse(TextBoxMinPrice.Text);
                    dem.AgentID = int.Parse(TextBoxAgent.Text);
                    dem.ClientID = int.Parse(TextBoxClient.Text);
                    landsdem.ID = int.Parse(TextBoxID.Text);
                    landsdem.MaxArea = decimal.Parse(TextBoxMaxArea.Text);
                    landsdem.MinArea = decimal.Parse(TextBoxMinArea.Text);

                    db.Demands.Add(dem);
                    db.LandsDemands.Add(landsdem);
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены");
                }
            }
            if (ComboBoxNeeds.SelectedIndex == 2)
            {
                using (AgencyContainer db = new AgencyContainer())
                {
                    Demands dem = new Demands();
                    AppsDemands appsdem = new AppsDemands();
                    dem.Id = int.Parse(TextBoxID.Text);
                    dem.NedID = int.Parse(TextBoxTypeObject.Text);
                    dem.MaxPrice = int.Parse(TextBoxMaxPrice.Text);
                    dem.MinPrice = int.Parse(TextBoxMinPrice.Text);
                    dem.AgentID = int.Parse(TextBoxAgent.Text);
                    dem.ClientID = int.Parse(TextBoxClient.Text);
                    appsdem.ID = int.Parse(TextBoxID.Text);
                    appsdem.MaxArea = decimal.Parse(TextBoxMaxArea.Text);
                    appsdem.MinArea = decimal.Parse(TextBoxMinArea.Text);
                    appsdem.MinFloor = int.Parse(TextBoxMinFloor.Text);
                    appsdem.MaxFloor = int.Parse(TextBoxMaxFloor.Text);
                    appsdem.MaxRooms = int.Parse(TextBoxMaxRooms.Text);
                    appsdem.MinFloor = int.Parse(TextBoxMinRooms.Text);


                    db.Demands.Add(dem);
                    db.AppsDemands.Add(appsdem);
                    db.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены");
                }
            }
        }


        private void ComboBoxNeeds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Выбор типа недвижимости
            if (ComboBoxNeeds.SelectedIndex == 0)
            {
                MessageBox.Show("Выбран дом");
                TextBoxTypeObject.Text = "3";

                TextBoxMaxRooms.Visibility = Visibility.Visible;
                TextBoxMinRooms.Visibility = Visibility.Visible;
                TextBoxMaxFloor.Visibility = Visibility.Visible;
                TextBoxMinFloor.Visibility = Visibility.Visible;
                LabelMaxFloor.Visibility = Visibility.Visible;
                LabelMinFloor.Visibility = Visibility.Visible;
                LabelMaxRooms.Visibility = Visibility.Visible;
                LabelMinRooms.Visibility = Visibility.Visible;
            }
            if (ComboBoxNeeds.SelectedIndex == 1)
            {
                TextBoxTypeObject.Text = "1";
                MessageBox.Show("Выбрана земля");

                TextBoxMaxRooms.Visibility = Visibility.Hidden;
                TextBoxMinRooms.Visibility
                messagebox.show
                MessageBox.Show

            = Visibility.Hidden;
                TextBoxMaxFloor.Visibility = Visibility.Hidden;
                TextBoxMinFloor.Visibility = Visibility.Hidden;
                LabelMaxFloor.Visibility = Visibility.Hidden;
                LabelMinFloor.Visibility = Visibility.Hidden;
                LabelMaxRooms.Visibility = Visibility.Hidden;
                LabelMinRooms.Visibility = Visibility.Hidden;




            }
            if (ComboBoxNeeds.SelectedIndex == 2)
            {
                TextBoxTypeObject.Text = "2";
                MessageBox.Show("Выбраны аппартаменты");

                TextBoxMaxRooms.Visibility = Visibility.Visible;
                TextBoxMinRooms.Visibility = Visibility.Visible;
                TextBoxMaxFloor.Visibility = Visibility.Visible;
                TextBoxMinFloor.Visibility = Visibility.Visible;
                LabelMaxFloor.Visibility = Visibility.Visible;
                LabelMinFloor.Visibility = Visibility.Visible;
                LabelMaxRooms.Visibility = Visibility.Visible;
                LabelMinRooms.Visibility = Visibility.Visible;
            }
        }
        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            //Метод для проверки вводимых значений
            int result;
            if (!(int.TryParse(e.Text, out result) || e.Text == ","))
            {
                e.Handled = true;
            }
        }

        private void TextBoxID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Вызов метода
            CheckIsNumeric(e);
        }
        private void FillComboBoxAgent()
        {
            //Вывод данных в combobox из БД
            using (AgencyContainer db = new AgencyContainer())
            {


                var combo = (from c in db.Agent
                             join p in db.Person
                             on c.id equals p.id
                             select new
                             {
                                 AgentName = p.id + " " + p.FirstName + " " + p.LastName + "" + p.MiddleName
                             }).ToList();
                for (int i = 0; i < combo.Count; i++)
                {
                    ComboBoxAgent.Items.Add(combo[i].AgentName);
                }

            }
        }
        private void FillComboBoxClient()
        {
            //Вывод данных в combobox из БД
            using (AgencyContainer db = new AgencyContainer())
            {


                var combo = (from c in db.Client
                             join p in db.Person
                             on c.id equals p.id
                             select new
                             {
                                 ClientName =
                             p.id + " " +
                             p.FirstName + " " +
                             p.LastName + " " +
                             p.MiddleName
                             }).ToList();
                for (int i = 0; i < combo.Count; i++)
                {
                    ComboBoxClient.Items.Add(combo[i].ClientName);
                }

            }
        }

        private void ComboBoxClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = ComboBoxClient.SelectedItem.ToString();
            string[] subs = s.Split(' ');
            TextBoxClient.Text = subs[0];
        }

        private void ComboBoxAgent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string s = ComboBoxAgent.SelectedItem.ToString();
            string[] subs = s.Split(' ');
            TextBoxAgent.Text = subs[0];
        }

        private void TextBoxMinPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void TextBoxMaxPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void TextBoxMinArea_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void TextBoxMaxArea_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void TextBoxMinRooms_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void TextBoxMaxRooms_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void TextBoxMinFloor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void TextBoxMaxFloor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }
    }
}
//{

//    //using (AgencyContainer db = new AgencyContainer()) // подключаем БД Агенство
//    //{
//    //    if (index == true)
//    //    {
//    //        Demand ob = new Demand(); // подключение таблицы Person                   
//    //        ob.id = int.Parse(TBid.Text); // заполняем id                   
//    //        ob.idClient = int.Parse(TBClient.Text);  // заполняем фамилию
//    //        ob.idAgent = int.Parse(TBAgent.Text); // заполняем имя
//    //        ob.idTypeDem = int.Parse(TBType.Text); // заполняем отчество
//    //        ob.Address = int.Parse(TBAdres.Text); // заполняем коэффициент
//    //        ob.MinPrice = int.Parse(TBMinPrice.Text);
//    //        ob.MinPrice = int.Parse(TBMaxPrice.Text);
//    //        db.Demand.Add(ob); // заполняем таблицу

//    //        db.SaveChanges();  // сохраняем в БД
//    //        MessageBox.Show("Объект добавлен");  // вывод сообщения

//    //    }
//    //    if (index == false)
//    //    {
//    //        Demand ob = db.Demand.Find(int.Parse(TBid.Text)); // поиск по id
//    //        ob.idClient = int.Parse(TBClient.Text);  //  фамилию
//    //        ob.idAgent = int.Parse(TBAgent.Text); // заполняем имя
//    //        ob.idTypeDem = int.Parse(TBType.Text); // заполняем отчество
//    //        ob.Address = int.Parse(TBAdres.Text); // заполняем коэффициент
//    //        ob.MinPrice = int.Parse(TBMinPrice.Text);
//    //        ob.MinPrice = int.Parse(TBMaxPrice.Text);
//    //        db.SaveChanges(); // сохраняем в БД
//    //        MessageBox.Show("Объект изменен"); // вывод сообщения
//    //    }
//    //}
//    //WinPotrebnosti main = new WinPotrebnosti(); // объявляем экземпляр
//    //main.Show(); // показываем экземпляр
//    //this.Hide();  // скрываем текущее
//}


