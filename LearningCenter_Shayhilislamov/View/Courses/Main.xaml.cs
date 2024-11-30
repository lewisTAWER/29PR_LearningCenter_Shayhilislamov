using LearningCenter_Shayhilislamov.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace LearningCenter_Shayhilislamov.View.Courses
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            DataContext = new VMCourses();
        }
    }
}
