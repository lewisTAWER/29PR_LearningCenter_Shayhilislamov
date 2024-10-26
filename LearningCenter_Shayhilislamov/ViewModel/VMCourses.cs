using LearningCenter_Shayhilislamov.Context;
using LearningCenter_Shayhilislamov.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LearningCenter_Shayhilislamov.ViewModel
{
    public class VMCourses : INotifyPropertyChanged
    {
        public ObservableCollection<Courses> Items { get; set; }

        public Classes.RelayCommand NewItem
        {
            get
            {
                return new Classes.RelayCommand(obj =>
                {
                    Courses _courses = new()
                    {
                        Id = 0,
                        Title = "",
                        Date = DateTime.Now,
                        Time = TimeSpan.Zero,
                    };
                    VMCoursesAdd newModell = new(_courses, new CoursesContext());
                    MainWindow.init?.frame.Navigate(new View.Courses.Add(newModell));
                });
            }
        }

        public VMCourses() => Items = Context.CoursesContext.AllCourses();

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
