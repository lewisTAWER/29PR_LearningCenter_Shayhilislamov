using LearningCenter_Shayhilislamov.Context;
using LearningCenter_Shayhilislamov.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LearningCenter_Shayhilislamov.ViewModel
{
    public class VMStudentsAdd : INotifyPropertyChanged
    {
        public Students item { get; set; }
        public StudentsContext context { get; set; }
        public ObservableCollection<Courses> courses { get; set; }

        public VMStudentsAdd(Students Item, StudentsContext Context)
        {
            item = Item;
            context = Context;
            courses = new VMCourses().Items;
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
