using LearningCenter_Shayhilislamov.Context;
using LearningCenter_Shayhilislamov.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LearningCenter_Shayhilislamov.ViewModel
{
    public class VMStudentsAdd : INotifyPropertyChanged
    {
        private Courses _selectedCourse;
        public Courses SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                item.CoursesId = value?.Id ?? 0;
                item.Courses = value; // Непосредственная привязка объекта
                OnPropertyChanged();
            }
        }
        public Students item { get; set; }
        public StudentsContext context { get; set; }
        public ObservableCollection<Courses> courses { get; set; }

        public VMStudentsAdd(Students Item, StudentsContext Context)
        {
            item = Item;
            context = Context;

            // Загружаем курсы из существующего контекста
            using (var coursesContext = new CoursesContext())
            {
                courses = new ObservableCollection<Courses>(coursesContext.Courses.ToList());
            }

            // Устанавливаем выбранный курс
            SelectedCourse = courses.FirstOrDefault(c => c.Id == item.CoursesId);
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
