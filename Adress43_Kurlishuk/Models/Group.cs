using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using Adress43_Kurlishuk.Classes;
using Schema = System.ComponentModel.DataAnnotations.Schema;

namespace Adress43_Kurlishuk.Models
{
    public class Group : Notification
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
                    "Не корректный воод значения."); // выводим сообщение
                else
                {
                    name = value;
                    OnProperyChanged("Name");
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
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_groups.groupsContext.SaveChanges();
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
                    if (MessageBox.Show("Вы уверены что хотите удалить группу?",
                    "Предупреждение", MessageBoxButton.YesNo)
                    ==
                    MessageBoxResult.Yes)
                    {
                        // Удаляем модель из коллекции
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_groups.Groups.Remove(this);
                        // Удаляем модель из контекста данных
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_groups.groupsContext.Remove(this);
                        // Вызываем сохранение данных в контексте TaskContext
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_groups.groupsContext.SaveChanges();
                        /// <summary> Команда выполнения

                    }
                });
            }
        }
    }
}
