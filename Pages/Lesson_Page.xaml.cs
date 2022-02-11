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
    /// Логика взаимодействия для Lesson_Page.xaml
    /// </summary>
    public partial class Lesson_Page : Page
    {
        int id_topic = 1;
        int id_per;
        public Lesson_Page(int IDperson)
        {
            id_per = IDperson;
            InitializeComponent();
            List_topic(id_topic);
        }
       
        private void List_topic(int id_topic)
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(ConString))
            {
                cn.Open();
                SqlCommand sqlCommand = new SqlCommand("use [Курсовой_Бикжанов] select * from Table_topic where ID_topic = '" + id_topic + "'", cn);
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                if (sqlData.Read())
                {
                    Oglav.Text = sqlData.GetString(1);
                    infoText.Text = sqlData.GetString(2);
                }
            }
        }
        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            id_topic = 1;
            List_topic(id_topic);
        }

        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            id_topic = 2;
            List_topic(id_topic);
        }

        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            id_topic = 3;
            List_topic(id_topic);
        }

        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            id_topic = 4;
            List_topic(id_topic);
        }

        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            id_topic = 5;
            List_topic(id_topic);
        }
    }
}
