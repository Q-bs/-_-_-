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
    /// Логика взаимодействия для Regist_Page.xaml
    /// </summary>
    public partial class Regist_Page : Page
    {
        public Regist_Page()
        {
            InitializeComponent();
        }
        private bool Regist()
        {
            bool test = true;
            try
            {
                string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                using (SqlConnection cn = new SqlConnection(ConString))
                {
                    cn.Open();
                    SqlCommand SqlCommand = new SqlCommand("exec Regist_Person @Login_Pers, @Password_Pers, @Name_Pers, @Surname_Pers", cn);
                    SqlCommand.Parameters.AddWithValue("@Name_Pers", Name_txtBox.Text);
                    SqlCommand.Parameters.AddWithValue("Surname_Pers", Surname_txtbox.Text);
                    SqlCommand.Parameters.AddWithValue("@Login_Pers", Login_txtBox.Text);
                    SqlCommand.Parameters.AddWithValue("@Password_Pers", Pass_txtbox.Text);
                    SqlDataReader sqlData = SqlCommand.ExecuteReader();
                    if (sqlData.Read()) { }
                }
            }
            catch { MessageBox.Show("Ошибка базы данных(4)"); test = false; }
            return test;
        }
        private void Registrat_Click(object sender, RoutedEventArgs e)
        {
            string name = Name_txtBox.Text.Trim();
            string surname = Surname_txtbox.Text.Trim();
            string login = Login_txtBox.Text.Trim();
            string password = Pass_txtbox.Text.Trim();
            string password2 = Pass2_txtbox.Text.Trim();

            if (name.Length != 0)
            {
                if (surname.Length != 0)
                {
                    if (login.Length != 0)
                    {
                        if (Pass_txtbox.Text.Length != 0)
                        {
                            if (Pass2_txtbox.Text.Length != 0)
                            {
                                if (Pass_txtbox.Text.Length > 0 && Login_txtBox.Text.Length > 0 && Pass2_txtbox.Text.Length > 0)
                                {
                                    if (password != password2)
                                    {
                                        MessageBox.Show("Пороли не совподают");
                                    }
                                    else if (Regist())
                                    {
                                        MessageBox.Show("Вы успешно зарегистрированы");
                                        NavigationService.Navigate(new Login_Page());
                                    }
                                    else { MessageBox.Show("Ошибка регистрации"); }
                                }
                                else { MessageBox.Show("Введите email и пороль"); }
                            }
                            else { MessageBox.Show("Введите повторно пароль"); }
                        }
                        else { MessageBox.Show("Введите пароль"); }
                    }
                    else { MessageBox.Show("Введите email"); }
                }
                else { MessageBox.Show("Введите Фамилию"); }
            }
            else { MessageBox.Show("Введите Имя"); }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login_Page());
        }
    }
}
