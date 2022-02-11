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
    /// Логика взаимодействия для Test_Page.xaml
    /// </summary>
    public partial class Test_Page : Page
    {
        Frame MyFrame;
        int id_topic, id_pers, id_quests;
        int Cours_res = 0;
        string truetext, textSaved;
        public Test_Page(Frame frame, int IDPerson, int ID_topic)
        {
            id_topic = ID_topic;          
            MyFrame = frame;
            id_pers = IDPerson;                
            InitializeComponent();
            List(id_pers);         
            if (infoText.Text == null)
            {
                Restart_Btn.Visibility = Visibility.Visible;
            }
        }
        private void List(int id_Person)
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(ConString))
            {
                cn.Open();
                SqlCommand sqlCommand = new SqlCommand("use [Курсовой_Бикжанов] select TOP(1) dbo.Quests.ID_qests, dbo.Quests.Text_quests, dbo.Table_topic.Name_topic from dbo.Table_topic inner join dbo.Quests ON dbo.Table_topic.ID_topic = dbo.Quests.ID_topic full join dbo.Course_result ON dbo.Course_result.ID_qests = dbo.Quests.ID_qests where ((ID_person = " + id_Person.ToString() + " and dbo.Course_result.Course_result = " + Cours_res.ToString() + " ) OR dbo.Course_result.Course_result is NULL) AND dbo.Quests.ID_topic = " + id_topic.ToString() + " group by dbo.Quests.ID_qests,dbo.Quests.Text_quests, dbo.Table_topic.Name_topic ORDER BY NEWID()", cn);
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                if (sqlData.Read())
                {
                    id_quests = sqlData.GetInt32(0);                    
                    infoText.Text = sqlData.GetString(1);
                    Oglav.Text = sqlData.GetString(2);
                    AnswerOptions(id_quests);
                }
            }          
        }       

        private void Test_1_Click(object sender, RoutedEventArgs e)
        {
            id_topic = 1;
            List(id_pers);          
        }
        private void Test_2_Click(object sender, RoutedEventArgs e)
        {
            id_topic = 2;
            List(id_pers);
        }
        private void Test_3_Click(object sender, RoutedEventArgs e)
        {
            id_topic = 3;
            List(id_pers);
        }
        private void Test_5_Click(object sender, RoutedEventArgs e)
        {
            id_topic = 5;
            List(id_pers);
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            if (textSaved.CompareTo(truetext) == 0)
            {
                Save_Result(id_quests, id_pers);
                MyFrame.Navigate(new Result_test_Page(MyFrame, id_topic, id_quests, id_pers, true));
            }
            else { MyFrame.Navigate(new Result_test_Page(MyFrame, id_topic, id_quests, id_pers, false)); }
        }

        private void AnswerOptions(int id_quests)
        {
            string[] falseAn = FalseAnswerOptions(id_quests, 0);
            if (falseAn != null)
            {
                string[] trueAn = FalseAnswerOptions(id_quests, 1);
                if (trueAn != null)
                {
                    int j = 4;
                    if ((trueAn.Length + falseAn.Length) <= 4)
                        j = trueAn.Length + falseAn.Length;
                    switch (j)
                    {
                        case 4:
                            Btn_1.Visibility = Visibility.Visible;
                            goto case 3;
                        case 3:
                            Btn_2.Visibility = Visibility.Visible;
                            goto case 2;
                        case 2:
                            Btn_3.Visibility = Visibility.Visible;
                            goto case 1;
                        case 1:
                            Btn_4.Visibility = Visibility.Visible;
                            break;
                    }
                    int[] test = null;
                    int[] random = null;
                    Random(ref test, 0, j);
                    Random(ref random, 0, trueAn.Length - 1);
                    truetext = trueAn[random[random.Length - 1]];
                    if (test[test.Length - 1] == 0)
                        Txt_1.Text = truetext;
                    else if (test[test.Length - 1] == 1)
                        Txt_2.Text = truetext;
                    else if (test[test.Length - 1] == 2)
                        Txt_3.Text = truetext;
                    else if (j >= 4)
                        Txt_4.Text = truetext;
                    random = null;
                    for (int i = 0; i < (j - 1); i++)
                    {
                        Random(ref test, 0, j);
                        Random(ref random, 0, falseAn.Length);
                        if (test[test.Length - 1] == 0)
                            Txt_1.Text = falseAn[random[random.Length - 1]];
                        else if (test[test.Length - 1] == 1)
                            Txt_2.Text = falseAn[random[random.Length - 1]];
                        else if (test[test.Length - 1] == 2)
                            Txt_3.Text = falseAn[random[random.Length - 1]];
                        else if (j >= 4)
                            Txt_4.Text = falseAn[random[random.Length - 1]];
                    }
                }
            }
        }

        private void Btn_1_Click(object sender, RoutedEventArgs e)
        {
            textSaved = Txt_1.Text;           
        }
        private void Btn_2_Click(object sender, RoutedEventArgs e)
        {
            textSaved = Txt_2.Text;
        }
        private void Btn_3_Click(object sender, RoutedEventArgs e)
        {
            textSaved = Txt_3.Text;
        }
        private void Btn_4_Click(object sender, RoutedEventArgs e)
        {
            textSaved = Txt_4.Text;
        }

        private void Random(ref int[] mass, int start, int end)
        {
            int[] massive;
            Random random = new Random();
            if (mass != null)
            {
                massive = new int[mass.Length + 1];
                mass.CopyTo(massive, 0);
                do { massive[mass.Length] = random.Next(start, end); }
                while (mass.Contains(massive[mass.Length]));
            }
            else
            {
                massive = new int[1];
                massive[0] = random.Next(start, end);
            }
            mass = new int[massive.Length];
            massive.CopyTo(mass, 0);
        }

        private string[] FalseAnswerOptions(int id_quests, int test)
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(ConString))
            {
                string[] answer;
                cn.Open();
                SqlCommand sqlCommand = new SqlCommand("use [Курсовой_Бикжанов] select count(dbo.Answer_options.ID_answer) from dbo.Answer_options inner join dbo.Quests ON dbo.Quests.ID_qests=dbo.Answer_options.ID_qests where dbo.Answer_options.True_answer = " + test.ToString() + " AND dbo.Answer_options.ID_qests = " + id_quests.ToString(), cn);
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                if (sqlData.Read())
                {
                    answer = new string[sqlData.GetInt32(0)];
                    cn.Close();
                    cn.Open();
                    sqlCommand = new SqlCommand("use [Курсовой_Бикжанов] select dbo.Answer_options.answer_Text from dbo.Answer_options inner join dbo.Quests ON dbo.Quests.ID_qests=dbo.Answer_options.ID_qests where dbo.Answer_options.True_answer = " + test.ToString() + " AND dbo.Answer_options.ID_qests = " + id_quests.ToString(), cn);
                    sqlData = sqlCommand.ExecuteReader();
                    int i = 0;
                    while (sqlData.Read())
                    {
                        answer[i++] = sqlData.GetString(0);
                    }
                    return answer;
                }
                return null;
            }
        }
        private void Save_Result(int id_quests, int id_person)
        {
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(ConString))
            {
                cn.Open();
                SqlCommand sqlCommand = new SqlCommand("use [Курсовой_Бикжанов] insert into dbo.Course_result(ID_person, ID_qests, Course_result) values (" + id_person.ToString() + "," + id_quests.ToString() + ",1)", cn);
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                sqlData.Read();
            }
        }
    }
}
