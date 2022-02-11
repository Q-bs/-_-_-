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
    /// Логика взаимодействия для Login_Page.xaml
    /// </summary>
    public partial class Login_Page : Page
    {
        private int IDPerson;
        public Login_Page()
        {
            InitializeComponent();           
        }           
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = Login_Box.Text.Trim();
            string password = Password_Box.Password.Trim();

            if (Login_Box.Text.Length != 0)
            {
                if (Password_Box.Password.Length != 0)
                {
                    if (Password_Box.Password.Length > 0 && Login_Box.Text.Length > 0)
                    {
                        if (Input_Person(login, password, ref IDPerson))
                        {
                            NavigationService.Navigate(new Main_Page(IDPerson));
                        }
                        else { MessageBox.Show("У вас нет доступа"); }
                    }
                    else { MessageBox.Show("Введите логин или пароль"); }
                }
                else { MessageBox.Show("Введите пароль"); }
            }
            else { MessageBox.Show("Введите логин"); }
        }
        private bool Input_Person(string login, string password, ref int IDPerson)
        {
            bool test = true;
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                using (SqlConnection cn = new SqlConnection(ConString))
                {
                    cn.Open();
                    SqlCommand sqlCommand = new SqlCommand("use [Курсовой_Бикжанов] SELECT ID_person FROM dbo.Persons where Login_Pers = '" + login + "' and Password_Pers = '" + password + "'", cn);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    if (sqlData.Read())
                    {
                        IDPerson = sqlData.GetInt32(0);
                    }
                    else { test = false; }
                    sqlData.Close();
                }
            }
            catch { MessageBox.Show("Ошибка базы данных(3)"); test = false; }
            return test;
        }
        private void Regist_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Regist_Page());
        }
    }
}
