using LearningCenter_Shayhilislamov.Classes.Common;
using LearningCenter_Shayhilislamov.Classes;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using LearningCenter_Shayhilislamov.Model;

namespace LearningCenter_Shayhilislamov.Context
{
    public class StudentsContext : DbContext
    {
        public DbSet<Students> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseMySql(Config.ConnectionConfig, Config.Version);

        public StudentsContext() => Database.EnsureCreated();
        public static ObservableCollection<Students> AllStudents()
        {
            using StudentsContext context = new();
            return new ObservableCollection<Students>([.. context.Students]);
        }

        public void Save(Students studentsItem, bool isNew)
        {
            using StudentsContext context = new();
            if (isNew)
            {
                context.Students.Add(studentsItem);
            }
            else
            {
                studentsItem.Courses = null;
                context.Students.Update(studentsItem);
            }
            context.SaveChanges();
        }

        public void Delete(Students studentsItem)
        {
            Students.Remove(studentsItem);
            SaveChanges();
        }

        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (obj is Students studentsItem)
                    {
                        Save(studentsItem, studentsItem.Id == 0);
                        MainWindow.init?.frame.Navigate(new View.Students.Main());
                    }
                });
            }
        }
    }
}
