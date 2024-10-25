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

        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("_id");
            }
        }
        private string? _name;
        public string? Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("_name");
            }
        }


        private int _coursesId;
        public int CoursesId
        {
            get { return _coursesId; }
            set
            {
                _coursesId = value;
                OnPropertyChanged("_coursesId");
            }
        }

        private Courses? _courses;
        public Courses? Courses
        {
            get { return _courses; }
            set
            {
                _courses = value;
                OnPropertyChanged("_courses");
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
