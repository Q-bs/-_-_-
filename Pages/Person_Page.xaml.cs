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
    /// Логика взаимодействия для Person_Page.xaml
    /// </summary>
    public partial class Person_Page : Page
    {
        int id_pers;
        public Person_Page(int ID_Person)
        {
            id_pers = ID_Person;
            InitializeComponent();
            Сounting_Progress();
        }        

        private string Сounting_Progress()
        {
            int procentAll = 0;         
            string ConString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(ConString))
            {
                cn.Open();
                SqlCommand sqlCommand = new SqlCommand("use [Курсовой_Бикжанов] select COUNT(dbo.Quests.ID_qests) from dbo.Quests inner join dbo.Course_result ON dbo.Course_result.ID_qests = dbo.Quests.ID_qests where (dbo.Course_result.Course_result = 1) AND (dbo.Course_result.ID_person = " + id_pers.ToString() + ")", cn);
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                if (sqlData.Read()) 
                { 
                    if (!sqlData.IsDBNull(0)) 
                    { 
                        procentAll = sqlData.GetInt32(0); 
                    } 
                }
                cn.Close();
                cn.Open();
                sqlCommand = new SqlCommand("use [Курсовой_Бикжанов] select COUNT(dbo.Quests.ID_qests) from dbo.Quests", cn);
                sqlData = sqlCommand.ExecuteReader();
                if (sqlData.Read())
                {
                    if (!sqlData.IsDBNull(0))
                    {                       
                    procentAll = 100 / sqlData.GetInt32(0) * procentAll;                       
                    }
                    Res.Text = procentAll.ToString() + "%";
                }
            }
            return Res.Text;
        }
    }
}
