using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace ToDoMod
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        /********
         ** Properties
         ********/
        /// <summary>The mod settings.</summary>
        private ModConfig Config;

        /********
         ** Public methods
         ********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        public override void Entry(IModHelper helper)
        {
            ControlEvents.KeyPressed += this.ControlEvents_KeyPress;
        }

        /********
         ** Private methods
         ********/
        /// <summary>Open the to do list.</summary>
        private void OpenMenus()
        {
            this.Monitor.Log($"Inside opening menu");
            if (Game1.activeClickableMenu != null)
                Game1.exitActiveMenu();
            Game1.activeClickableMenu = new ToDoList(0, this.Config, this.SaveConfig);
        }

        /// <summary>Update the mod's config.json file from the current <see cref="Config"/>.</summary>
        private void SaveConfig()
        {
            this.Helper.WriteConfig(this.Config);
        }

        private void ControlEvents_KeyPress(object sender, EventArgsKeyPressed e)
        {

            if ((Context.IsWorldReady) && (e.KeyPressed == Keys.N))
            {
                this.OpenMenus();
            }
        }
    }
}