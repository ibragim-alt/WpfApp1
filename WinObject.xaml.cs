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
    /// Логика взаимодействия для WinObject.xaml
    /// </summary>
    public partial class WinObject : Window
    {
        public bool index;
        public WinObject()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            using (AgencyContainer db = new AgencyContainer()) // подключаем БД Агенство
            {
                if (index == true)
                {
                    Objects ob = new Objects(); // подключение таблицы Person                   
                    ob.ObjectID = int.Parse(TBID.Text); // заполняем id                   
                    ob.AddressCity = TBCity.Text;  // заполняем фамилию
                    ob.AdressStreet = TBStreet.Text; // заполняем имя
                    ob.AdressHouse = TBHouse.Text; // заполняем отчество
                    ob.AddressNumber = TBNum.Text; // заполняем коэффициент
                    ob.CoordinateLatitude = double.Parse(TBLat.Text);
                    ob.CoordinateLongitude = double.Parse(TBLog.Text);
                    db.Objects.Add(ob); // заполняем таблицу
                    
                    db.SaveChanges();  // сохраняем в БД
                    MessageBox.Show("Объект добавлен");  // вывод сообщения

                }
                if (index == false)
                {
                    Objects ob = db.Objects.Find(int.Parse(TBID.Text)); // поиск по id
                    ob.AddressCity = TBCity.Text;  //  фамилию
                    ob.AdressStreet = TBStreet.Text; // заполняем имя
                    ob.AdressHouse = TBHouse.Text; // заполняем отчество
                    ob.AddressNumber = TBNum.Text; // заполняем коэффициент
                    ob.CoordinateLatitude = double.Parse(TBLat.Text);
                    ob.CoordinateLongitude = double.Parse(TBLog.Text);
                    db.SaveChanges(); // сохраняем в БД
                    MessageBox.Show("Объект изменен"); // вывод сообщения
                }
            }
            ObjectHouse main = new ObjectHouse(); // объявляем экземпляр
            main.Show(); // показываем экземпляр
            this.Hide();  // скрываем текущее
        }

        private void SliderDol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider; // объявляем слайдер
            TBLat.Text = slider.Value.ToString(); // записываем в текстбокс
            double thisValue = slider.Value; // переводим в дробные
            thisValue = Math.Round(thisValue, 5); // округляем до 5 чесел после цифр
            slider.Value = thisValue; 
        }

        private void SliderLon_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            TBLog.Text = slider.Value.ToString();
            double thisValue = slider.Value;
            thisValue = Math.Round(thisValue,5);
            slider.Value = thisValue;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            ObjectHouse objectHouse = new ObjectHouse(); // объявляем экземпляр
            objectHouse.Show(); // показываем экземпляр
            this.Close();  // скрываем текущее
        }

        private void TBLat_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, 0)) // если введен символ то он закрыт
            {
                e.Handled = false;

            }
            else
            {
                e.Handled = true;
            }
        }

        private void TBLog_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, 0)) // если введен символ то он закрыт
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
