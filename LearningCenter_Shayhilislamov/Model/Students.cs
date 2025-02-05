using LearningCenter_Shayhilislamov.Classes;
using LearningCenter_Shayhilislamov.Context;
using LearningCenter_Shayhilislamov.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LearningCenter_Shayhilislamov.Model
{
    public class Students : INotifyPropertyChanged
    {
        private Courses? _coursesField;

        public Courses? Courses
        {
            get => _coursesField;
            set
            {
                _coursesField = value;
                // Указываем корректное имя свойства для уведомления
                OnPropertyChanged(nameof(Courses));
                OnPropertyChanged(nameof(CoursesTitle));
            }
        }

        public string CoursesTitle => Courses?.Title ?? "Не выбран";

        // Остальные свойства остаются без изменений
        private int _coursesId;
        public int CoursesId
        {
            get => _coursesId;
            set
            {
                _coursesId = value;
                OnPropertyChanged(nameof(CoursesId));
            }
        }

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id)); // Было OnPropertyChanged("_id")
            }
        }

        private string? _name;
        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name)); // Было OnPropertyChanged("_name")
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public RelayCommand OnEdit
        {
            get
            {
                return new RelayCommand(studentsItem =>
                {
                    if (studentsItem is Students studentsObject)
                    {
                        using StudentsContext studentsContext = new();
                        VMStudentsAdd newModel = new(studentsObject, studentsContext);
                        MainWindow.init?.frame.Navigate(new View.Students.Add(newModel));
                    }
                });
            }
        }
        public RelayCommand OnDelete
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (obj is Students studentsItem)
                    {
                        using StudentsContext studentsContext = new();
                        studentsContext.Delete(this);
                        MainWindow.init.MainStudents = new View.Students.Main();
                        MainWindow.init.frame.Navigate(MainWindow.init.MainStudents);
                    }
                });
            }
        }


    }
}
