using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace Курсовой_проект_Бикжанов.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main_Page.xaml
    /// </summary>
    public partial class Main_Page : Page
    {
        readonly int IDpers;
        readonly int ID_toipc = 1;
        public Main_Page(int IDPerson)
        {
            IDpers = IDPerson;
            InitializeComponent();
            MyFrame.Navigate(new Lesson_Page(IDpers));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login_Page());
        }

        private void Lesson_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new Lesson_Page(IDpers));
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new Test_Page(MyFrame, IDpers, ID_toipc));
        }

        private void Person_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new Person_Page(IDpers));
        }
    }       
}
