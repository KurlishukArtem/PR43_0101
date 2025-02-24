using System;
using System.Collections.Generic;
using System.Text;
using Adress43_Kurlishuk.Classes;
using System.Text.RegularExpressions;
using System.Windows;
using Schema = System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Input;
using Adress43_Kurlishuk;

namespace Adress43_Kurlishuk.Models
{
    public class Tasks : Notification
    {
        public int Id { get; set; }
        /// <summary> Наименование
        private string name;
        public string Name
        {
            get { return name; } // Аксессор чтения
            set // Аксессор записи
            {
                // проверяем входящие значение, на регулярное выражение
                Match match = Regex.Match(value, "^.{1,50}$");
                if (!match.Success) // если нет совпадения
                    MessageBox.Show("Наименование не должно быть пустым, и не более 50 символов.",
                    "Не корректный ввод значения."); // выводим сообщение
                else
                {
                    name = value;
                    OnProperyChanged("Name");
                }
            }
        }
        private Group priority;
        public Group Priority
        {
            get { return priority; } // Аксессор чтения
            set // Аксессор записи
            {
                priority = value; // запоминаем введёное значение
                OnProperyChanged("Priority"); // сообщаем об изменении свойства
            }
        }
        private DateTime dateExecute;
        public DateTime DateExecute
        {
            get { return dateExecute; } // Аксессор чтения
            set // Аксессор записи
            {
                // Проверяем что указаная дата меньше чем текущая
                if (value.Date < DateTime.Now.Date)
                    MessageBox.Show("Дата выполнения не может быть меньше текущей.", "Не корректный ввод значения.");
                else
                {
                     // выводим сообщение
                    dateExecute = value; // запоминаем введёное значение
                    OnProperyChanged("DateExecute"); // сообщаем об изменении свойства
                }
            }
        }
        private string comment;
        public string Comment
        {
            get { return comment; } // Аксессор чтения
            set // Аксессор записи
            {
                // проверяем входящие значение, на регулярное выражение
                Match match = Regex.Match(value, "^.{1,1000}$");
                if (!match.Success) // если нет совпадения
                    MessageBox.Show("Комментарий не должен быть пустым, и не более 1000 символов.",
                    "Не корректный воод значения."); // выводим сообщение
                else
                {
                    comment = value; // запоминаем введёное значение
                    OnProperyChanged("Comment"); // сообщаем об изменении свойства
                }
            }
        }

        /// <summary> Видимость элементов
        [Schema.NotMapped] // исключаем поле из добавления в таблицу базы данных
        private bool isEnable;
        /// <summary> Свойство для видимости элементов
        [Schema.NotMapped] // исключаем поле из добавления в таблицу базы данных
        public bool IsEnable
        {
            get { return isEnable; } // Аксессор чтения
            set // Аксессор записи
            {
                isEnable = value; // запоминаем введёное значение
                OnProperyChanged("IsEnable"); // сообщаем об изменении свойства
                OnProperyChanged("IsEnableText"); // сообщаем об изменении свойства
            }
        }
        [Schema.NotMapped] // исключаем поле из добавления в таблицу базы 
        public string IsEnableText
        {
            get // Аксессор чтения
            {
                if (IsEnable) return "Сохранить"; // Если изменение включено, возвращаем одно значение
                else return "Изменить"; // Иначе другое
            }
        }

        [Schema.NotMapped] // исключаем поле из добавления в таблицу базы 
        public RealyCommand OnEdit
        {
            get // Аксессор чтения
            {
                return new RealyCommand(obj =>
                { // Выполняем команду
                    IsEnable = !IsEnable; // Изменяем состояние изменения представления
                    if (!IsEnable) // Если состояние не активно
                                   // Вызываем сохранение данных в контексте TaskContext
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_tasks.tasksContext.SaveChanges();
                });
            }
        }
        [Schema.NotMapped] // исключаем поле из добавления в таблицу базы 
        public RealyCommand OnDelete
        {
            get // Аксессор чтения
            {
                return new RealyCommand(obj => // Выполняем команду
                {
                    // Уточняем о том что пользователь хочет удалить объект
                    if (MessageBox.Show("Вы уверены что хотите удалить задачу?",
                    "Предупреждение", MessageBoxButton.YesNo)
                    ==
                    MessageBoxResult.Yes)
                    {
                        // Удаляем модель из коллекции
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_tasks.Tasks.Remove(this);
                        // Удаляем модель из контекста данных
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_tasks.tasksContext.Remove(this);
                        // Вызываем сохранение данных в контексте TaskContext
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_tasks.tasksContext.SaveChanges();
                        /// <summary> Команда выполнения

                    }
                });
            }
        }
    }
}
