using LearningCenter_Shayhilislamov.Classes;
using LearningCenter_Shayhilislamov.Classes.Common;
using LearningCenter_Shayhilislamov.Model;
using LearningCenter_Shayhilislamov.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace LearningCenter_Shayhilislamov.Context
{
    public class StudentsContext : DbContext
    {
        public DbSet<Students> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseMySql(Config.ConnectionConfig, Config.Version);

        public StudentsContext() => Database.EnsureCreated();

        // Метод для получения всех студентов
        public static ObservableCollection<Students> AllStudents()
        {
            using StudentsContext context = new();
            return new ObservableCollection<Students>(context.Students);
        }

        // Метод для сохранения или обновления студента
        // Для студентов
        // Метод для сохранения или обновления студента
        public void Save(Students studentsItem, bool isNew)
        {
            using (StudentsContext context = new())
            {
                if (isNew)
                {
                    context.Students.Add(studentsItem);
                }
                else
                {
                    context.Students.Update(studentsItem);
                }
                context.SaveChanges();
            }

            // Обновление коллекции студентов
            VMStudents vmStudents = MainWindow.init.MainStudents.DataContext as VMStudents;
            if (vmStudents != null)
            {
                if (isNew)
                {
                    // Если это новый студент, добавляем его в коллекцию
                    vmStudents.Items.Add(studentsItem);
                }
                else
                {
                    // Если студент уже существует, обновляем его
                    var existingStudent = vmStudents.Items.FirstOrDefault(s => s.Id == studentsItem.Id);
                    if (existingStudent != null)
                    {
                        // Обновляем информацию о студенте в коллекции
                        existingStudent.Name = studentsItem.Name;
                        existingStudent.CoursesId = studentsItem.CoursesId;
                        existingStudent.Courses = studentsItem.Courses;
                    }
                }

                vmStudents.OnPropertyChanged(nameof(vmStudents.Items)); // Уведомление UI о изменении коллекции
            }

            // Переход на страницу студентов
            MainWindow.init?.frame.Navigate(new View.Students.Main());
        }




        // Метод для удаления студента
        // Метод для удаления студента
        public void Delete(Students studentsItem)
        {
            using (StudentsContext context = new())
            {
                context.Students.Remove(studentsItem);
                context.SaveChanges();
            }

            // Обновление коллекции после удаления студента
            VMStudents vmStudents = MainWindow.init.MainStudents.DataContext as VMStudents;
            if (vmStudents != null)
            {
                var studentToRemove = vmStudents.Items.FirstOrDefault(x => x.Id == studentsItem.Id);
                if (studentToRemove != null)
                {
                    vmStudents.Items.Remove(studentToRemove); // Удаление студента из коллекции
                    vmStudents.OnPropertyChanged(nameof(vmStudents.Items)); // Уведомление об изменении коллекции
                }
            }

            // Переход на страницу студентов
            MainWindow.init?.frame.Navigate(new View.Students.Main());
        }



        // Команда для сохранения студента
        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    if (obj is Students studentsItem)
                    {
                        Save(studentsItem, studentsItem.Id == 0); // Сохраняем новый или измененный студент
                    }
                });
            }
        }
    }
}
