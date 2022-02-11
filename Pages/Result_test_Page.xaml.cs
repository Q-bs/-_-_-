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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Курсовой_проект_Бикжанов.Pages
{
    /// <summary>
    /// Логика взаимодействия для Result_test_Page.xaml
    /// </summary>
    public partial class Result_test_Page : Page
    {
        int id_topic, id_pers;
        Frame MyFrame;
        bool test = true;
        public Result_test_Page(Frame frame, int ID_Topic, int ID_Person, bool Test)
        {
            InitializeComponent();
            id_pers = ID_Person;
            id_topic = ID_Topic;
            MyFrame = frame;
            test = Test;
            if (!test)
            {
                kek.Text = "Ебать ты тупой";
            }
        }
    }
}
