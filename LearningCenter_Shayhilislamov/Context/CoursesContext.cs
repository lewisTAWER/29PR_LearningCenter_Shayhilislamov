using LearningCenter_Shayhilislamov.Classes;
using LearningCenter_Shayhilislamov.Classes.Common;
using LearningCenter_Shayhilislamov.Model;
using LearningCenter_Shayhilislamov.ViewModel;
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

        // Метод для получения всех курсов из базы данных
        public static ObservableCollection<Courses> AllCourses()
        {
            using (CoursesContext context = new())
            {
                return new ObservableCollection<Courses>(context.Courses);
            }
        }

        // Метод для сохранения или обновления курса
        // Method for saving or updating a course
        public void Save(Courses coursesItem, bool isNew)
        {
            using (CoursesContext context = new())
            {
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

            // Refresh the courses list by clearing and reloading it
            VMCourses vmCourses = MainWindow.init.MainCourses.DataContext as VMCourses;
            if (vmCourses != null)
            {
                // Clear the existing items and reload from the database
                vmCourses.Items.Clear();
                using (CoursesContext context = new CoursesContext())
                {
                    var updatedCourses = new ObservableCollection<Courses>(context.Courses.ToList());
                    foreach (var course in updatedCourses)
                    {
                        vmCourses.Items.Add(course);
                    }
                }

                vmCourses.OnPropertyChanged(nameof(vmCourses.Items)); // Notify UI about the change
            }

            // Navigate back to the courses page
            MainWindow.init?.frame.Navigate(new View.Courses.Main());
        }


        // Метод для удаления курса
        public void Delete(Courses coursesItem)
        {
            using (CoursesContext context = new())
            {
                context.Courses.Remove(coursesItem);
                context.SaveChanges();
            }

            // Обновляем коллекцию после удаления
            VMCourses vmCourses = MainWindow.init.MainCourses.DataContext as VMCourses;
            if (vmCourses != null)
            {
                vmCourses.Items.Remove(coursesItem); // Убираем удаленный курс из коллекции
                vmCourses.OnPropertyChanged(nameof(vmCourses.Items)); // Уведомляем UI об изменении
            }

            // Переход на главную страницу с курсами
            MainWindow.init?.frame.Navigate(new View.Courses.Main());
        }

        // Команда для сохранения курса
        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (obj is Courses coursesItem)
                    {
                        Save(coursesItem, coursesItem.Id == 0); // Сохраняем новый или измененный курс
                    }
                });
            }
        }
    }
}
