using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace ToDoMod
{
    class ModConfig
    {
        public string OpenListKey { get; set; } = Keys.F2.ToString();

        //public string TaskListPath { get; set; } = ".\\Resources\\TaskList.txt";

        public StringCollection SavedTasks { get; set; } = new StringCollection();

    }
}
