using System;
using System.Collections.Generic;
using System.Text;
using Adress43_Kurlishuk.Classes;
using Adress43_Kurlishuk.Models;
using System.Collections.ObjectModel;
using Adress43_Kurlishuk.Context;
using System.Linq;
using System.Security.Policy;
using System.Windows.Controls;

namespace Adress43_Kurlishuk.ViewModels
{
    public class VM_Tasks : Notification
    {
        public VM_Groups Vm_Groups { get; set; }
        public TasksContext tasksContext = new TasksContext();
        public ObservableCollection<Tasks> Tasks {  get; set; }
        public VM_Tasks()
        {
            UpdateGroup();
            Tasks = new ObservableCollection<Tasks>(tasksContext.Tasks.OrderBy(x => x.Name));
        }

        public void UpdateGroup()
        {
            Vm_Groups = new VM_Groups();
        }

        public RealyCommand OnAddTask
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    Tasks NewTask = new Tasks()
                    {
                        DateExecute = DateTime.Now
                    };
                    Tasks.Add(NewTask);
                    tasksContext.Tasks.Add(NewTask);
                    tasksContext.SaveChanges();
                }); 
            }
        }
    }
}
