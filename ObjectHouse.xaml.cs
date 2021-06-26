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
    /// Логика взаимодействия для Objects.xaml
    /// </summary>
    public partial class ObjectHouse : Window
    {
        List<Objects> objects = new List<Objects>();
        public ObjectHouse()
        {
            InitializeComponent();
            FillTable();
            ApartmantsSort();
            HousesSort();
            LandsSort();
            CBFiltr.SelectedIndex = 0;
        }
        
        private void FillTable()
        {

            using (AgencyContainer db = new AgencyContainer()) // подключаем БД
            {
                var polis = (from i in db.Objects    
                             
                             
                             select new
                             {
                                 id = i.ObjectID,
                                 City = i.AddressCity,
                                 Street = i.AdressStreet,
                                 House = i.AdressHouse,
                                 Number = i.AddressNumber,
                                 Latitude = i.CoordinateLatitude,
                                 Longitude = i.CoordinateLongitude
                             }).ToList();
                

                DGObjects.ItemsSource = polis;   // записываем в дата грид
            }
        }

        private void ApartmantsSort()
        {
            using (AgencyContainer db = new AgencyContainer())
            {
                var polis = (from i in db.Objects
                             join v in db.Apartments
                             on i.ObjectID equals v.ObjectID


                             select new
                             {
                                 id = i.ObjectID,
                                 City = i.AddressCity,
                                 Street = i.AdressStreet,
                                 House = i.AdressHouse,
                                 Number = i.AddressNumber,
                                 Latitude = i.CoordinateLatitude,
                                 Longitude = i.CoordinateLongitude,
                                 Floor = v.Floor,
                                 Rooms = v.Rooms,
                                 Area = v.TotalArea
                             }).ToList();


                DGObjects.ItemsSource = polis;
            }
        }

        private void HousesSort()
        {
            using (AgencyContainer db = new AgencyContainer())
            {
                var polis = (from i in db.Objects
                             join v in db.Houses
                             on i.ObjectID equals v.ObjectID


                             select new
                             {
                                 id = i.ObjectID,
                                 City = i.AddressCity,
                                 Street = i.AdressStreet,
                                 House = i.AdressHouse,
                                 Number = i.AddressNumber,
                                 Latitude = i.CoordinateLatitude,
                                 Longitude = i.CoordinateLongitude,
                                 Floor = v.TotalFloors,
                                 Rooms = v.Rooms,
                                 Area = v.TotalArea
                             }).ToList();


                DGObjects.ItemsSource = polis;
            }
        }
        private void LandsSort()
        {
            using (AgencyContainer db = new AgencyContainer())
            {
                var polis = (from i in db.Objects
                             join v in db.Lands
                             on i.ObjectID equals v.ObjectsID

                             select new
                             {
                                 id = i.ObjectID,
                                 City = i.AddressCity,
                                 Street = i.AdressStreet,
                                 House = i.AdressHouse,
                                 Number = i.AddressNumber,
                                 Latitude = i.CoordinateLatitude,
                                 Longitude = i.CoordinateLongitude,
                                 Area = v.TotalArea
                                 
                             }).ToList();


                DGObjects.ItemsSource = polis;
            }
        }

       

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            WinObject winOb = new WinObject(); // подключаем экземпляр
            winOb.LabAdd.Content = "Добавить объект недвижимости"; //  изменяем Label
            winOb.index = true;
            winOb.Show(); // показываем экземпляр
            this.Hide(); // скрываем текущее
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            var index = DGObjects.SelectedItem; // выбираем строки в дата грид
            if (index != null)
            {
                int id = int.Parse((DGObjects.SelectedCells[0].Column.GetCellContent(index) as TextBlock).Text);
                {

                    WinObject winob = new WinObject(); // подключаем окно
                    winob.index= false;  // если фалсе то заполняются поля
                    winob.LabAdd.Content = "Изменение объекта";  // изменение заголовка
                    {
                        using (AgencyContainer db = new AgencyContainer()) // подключение к БД
                        {

                            Objects objects = db.Objects.Find(id); // выбор по ID

                            winob.TBID.Text = objects.ObjectID.ToString(); // заполняем ID
                            winob.TBCity.Text = objects.AddressCity.ToString(); // заполняем город
                            winob.TBHouse.Text = objects.AdressHouse.ToString(); // заполняем дом
                            winob.TBNum.Text = objects.AddressNumber.ToString(); // заполняем номер дома
                            winob.TBLat.Text = objects.CoordinateLatitude.ToString(); // заполняем координаты широты 
                            winob.TBLog.Text = objects.CoordinateLongitude.ToString(); // заполняем координаты долготы
                            winob.TBStreet.Text = objects.AdressStreet.ToString(); // заполняем улицу
                            winob.Show(); // показываем окно
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
            if (DGObjects.Items.Count > 0)
            {
                var index = DGObjects.SelectedItem;
                if (index != null)
                {
                    int id = int.Parse((DGObjects.SelectedCells[0].Column.GetCellContent(index) as TextBlock).Text); // поиск по ID

                    using (AgencyContainer db = new AgencyContainer()) // подключение к БД
                    {
                        
                        Objects objec = db.Objects.Find(id);  // поиск по ID
                        
                        db.Objects.Remove(objec);    // удаление записи
                        db.SaveChanges(); // сохраняет в БД
                    }
                    MessageBox.Show("Объект удален"); // вывод сообщения
                }

            }
        }

        private void CBFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DGObjects.SelectedItem = null;
            switch (CBFiltr.SelectedIndex) // переключает на другие объекты сортировки
            {
                case 0:
                    FillTable();
                    break;
                case 1:
                    ApartmantsSort();
                    break;
                case 2:
                    HousesSort();
                    break;
                case 3:
                    LandsSort();
                    break;
            }
        }

        private void CheckB_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(); // объявляем экземпляр
            mainWindow.Show(); // показываем экземпляр
            this.Close(); // закрываем экземпляр
        }
    }
}
