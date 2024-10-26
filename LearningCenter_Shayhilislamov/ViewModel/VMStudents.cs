using LearningCenter_Shayhilislamov.Context;
using LearningCenter_Shayhilislamov.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LearningCenter_Shayhilislamov.ViewModel
{
    public class VMStudents : INotifyPropertyChanged
    {


        public ObservableCollection<Students> Items { get; set; }

        public Classes.RelayCommand NewItem
        {
            get
            {
                return new Classes.RelayCommand(obj =>
                {
                    Students _students = new()
                    {
                        Id = 0,
                        Name = "",
                        CoursesId = 0,
                    };
                    VMStudentsAdd newModell = new(_students, new StudentsContext());
                    MainWindow.init?.frame.Navigate(new View.Students.Add(newModell));
                });
            }
        }

        public VMStudents()
        {
            using CoursesContext coursesContext = new CoursesContext();
            Items = new ObservableCollection<Students>(
                StudentsContext.AllStudents()
                    .Select(x => {
                        Courses? SelectedCourses = coursesContext.Courses.FirstOrDefault(_courses => _courses.Id == x.CoursesId);
                        x.Courses = SelectedCourses;
                        return x;
                    })
            );
        }


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
