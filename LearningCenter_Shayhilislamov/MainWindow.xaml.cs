using System.Windows;

namespace LearningCenter_Shayhilislamov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow? init;

        public View.Courses.Main MainCourses = new();
        public View.Students.Main MainStudents = new();
        public MainWindow()
        {
            InitializeComponent();

            init = this;
            frame.Navigate(MainCourses);
        }

        private void OpenCourses(object sender, RoutedEventArgs e) =>
            frame.Navigate(MainCourses);

        private void OpenStudents(object sender, RoutedEventArgs e) =>
            frame.Navigate(MainStudents);
    }
}