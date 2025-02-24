using Adress43_Kurlishuk.Classes;
using Adress43_Kurlishuk.Context;
using Adress43_Kurlishuk.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Adress43_Kurlishuk.ViewModels
{
    public class VM_Groups : Notification
    {
        public GroupContext groupsContext = new GroupContext();
        public ObservableCollection<Group> Groups { get; set; }
        public VM_Groups() =>
            Groups = new ObservableCollection<Group>(groupsContext.Groups.OrderBy(x => x.Name));
        public RealyCommand OnAddGroups
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    Group NewGroup = new Group();
                    Groups.Add(NewGroup);
                    groupsContext.Groups.Add(NewGroup);
                    groupsContext.SaveChanges();
                });
            }
        }
    }
}
