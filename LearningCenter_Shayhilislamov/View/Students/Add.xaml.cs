using System.Windows.Controls;

namespace LearningCenter_Shayhilislamov.View.Students
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Add(object? Context)
        {
            InitializeComponent();
            DataContext = Context;
        }
    }
}
