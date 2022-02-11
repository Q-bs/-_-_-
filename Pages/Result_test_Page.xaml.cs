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
    /// Логика взаимодействия для Result_test_Page.xaml
    /// </summary>
    public partial class Result_test_Page : Page
    {
        int id_topic, id_pers, id_quest;
        Frame MyFrame;
        bool test = true;    
        public Result_test_Page(Frame frame, int ID_Topic, int ID_Quest, int ID_Person, bool Test)
        {
            InitializeComponent();
            id_pers = ID_Person;
            id_topic = ID_Topic;
            id_quest = ID_Quest;
            MyFrame = frame;
            test = Test;
            if (!test)
            {
                kek.Text = "Сочуствую, но это не правильно(";
                kek.FontSize = 50;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new Test_Page(MyFrame, id_pers, id_topic));
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(ConString))
            {
                cn.Open();
                SqlCommand sqlCommand = new SqlCommand("use [Курсовой_Бикжанов] Delete from dbo.Course_result where ID_person = " + id_pers.ToString() + " and ID_qests = " + id_quest.ToString() + " ", cn);
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                sqlData.Read();
            }           
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            id_topic++;
            MyFrame.Navigate(new Test_Page(MyFrame, id_pers, id_topic));
        }
    }
}
