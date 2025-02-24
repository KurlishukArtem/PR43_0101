using Adress43_Kurlishuk;
using System;
using System.Collections.Generic;
using System.Text;
using Adress43_Kurlishuk.Classes;
using System.Windows;
using Adress43_Kurlishuk.View;


namespace Adress43_Kurlishuk.ViewModels
{
    public class VM_Pages : Notification
    {
        public VM_Tasks vm_tasks = new VM_Tasks();
        public VM_Groups vm_groups = new VM_Groups();
        public VM_Pages()
        {
            MainWindow.init.frame.Navigate(new View.Main(vm_tasks));
        }
        public RealyCommand OnClose
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    MainWindow.init.Close();
                });
            }
        }

        public RealyCommand OnTasks
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    MainWindow.init.frame.Navigate(new View.Main(vm_tasks));
                    vm_tasks.UpdateGroup();
                    Main.main.GroupGrid.Visibility = Visibility.Hidden;
                    Main.main.TaskGrid.Visibility = Visibility.Visible;
                });
            }
        }

        public RealyCommand OnGroups
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    MainWindow.init.frame.Navigate(new View.Main(vm_groups));
                    Main.main.TaskGrid.Visibility = Visibility.Hidden;
                    Main.main.GroupGrid.Visibility = Visibility.Visible;
                });
            }
        }
    }
}
