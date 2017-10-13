using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewValley.BellsAndWhistles;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoMod
{
    class Task
    {
        public string taskName;

        public Task(String name)
        {
            this.taskName = name;
        }
    }
}
