using LearningCenter_Shayhilislamov.Classes;
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

        // Команда для добавления нового курса
        public RelayCommand NewItem
        {
            get
            {
                return new RelayCommand(obj =>
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

        // Конструктор, который заполняет коллекцию курсами из базы данных
        public VMCourses()
        {
            // Загружаем курсы из контекста
            using (CoursesContext context = new CoursesContext())
            {
                Items = new ObservableCollection<Courses>(context.Courses.ToList());
            }
        }


        // Метод для добавления нового курса в коллекцию и обновления привязки
        public void AddNewCourse(Courses newCourse)
        {
            Items.Add(newCourse); // Добавляем новый курс в коллекцию
            OnPropertyChanged(nameof(Items)); // Уведомляем UI о изменении коллекции
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
