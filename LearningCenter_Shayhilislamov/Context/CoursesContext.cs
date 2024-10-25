using LearningCenter_Shayhilislamov.Classes.Common;
using LearningCenter_Shayhilislamov.Classes;
using LearningCenter_Shayhilislamov.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace LearningCenter_Shayhilislamov.Context
{
    public class CoursesContext : DbContext
    {
        public DbSet<Courses> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseMySql(Config.ConnectionConfig, Config.Version);

        public CoursesContext() => Database.EnsureCreated();

        public static ObservableCollection<Courses> AllCourses()
        {
            using CoursesContext context = new();
            return new ObservableCollection<Courses>(context.Courses);
        }

        public void Save(Courses coursesItem, bool isNew)
        {
            using CoursesContext context = new();
            if (isNew)
            {
                context.Courses.Add(coursesItem);
            }
            else
            {
                context.Courses.Update(coursesItem);
            }
            context.SaveChanges();
        }

        public void Delete(Courses coursesItem)
        {
            using CoursesContext context = new();
            context.Courses.Remove(coursesItem);
            context.SaveChanges();
        }

        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (obj is Courses coursesItem)
                    {
                        Save(coursesItem, coursesItem.Id == 0);
                        MainWindow.init?.frame.Navigate(new View.Courses.Main());
                    }
                });
            }
        }
    }
}
