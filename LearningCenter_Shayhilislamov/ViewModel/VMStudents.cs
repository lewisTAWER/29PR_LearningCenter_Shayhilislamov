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

        // Конструктор, который заполняет коллекцию студентов
        public VMStudents()
        {
            using (CoursesContext coursesContext = new CoursesContext())
            {
                // Загружаем студентов и их курсы
                Items = new ObservableCollection<Students>(
                    StudentsContext.AllStudents()
                        .Select(x =>
                        {
                            // Получаем курс для студента по ID
                            Courses? selectedCourse = coursesContext.Courses.FirstOrDefault(c => c.Id == x.CoursesId);
                            x.Courses = selectedCourse; // Назначаем курс студенту
                            return x;
                        }));
            }
        }

        // Метод для добавления нового студента в коллекцию и обновления привязки
        public void AddNewStudent(Students newStudent)
        {
            // Загружаем курс для нового студента, если нужно
            using (CoursesContext coursesContext = new CoursesContext())
            {
                newStudent.Courses = coursesContext.Courses.FirstOrDefault(c => c.Id == newStudent.CoursesId);
            }

            Items.Add(newStudent); // Добавляем нового студента в коллекцию
            OnPropertyChanged(nameof(Items)); // Уведомляем UI об изменении коллекции
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
