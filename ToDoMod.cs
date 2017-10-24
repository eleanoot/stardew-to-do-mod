using System;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

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
        private ToDoList toDoList;

        private ModData Data;

        /********
         ** Public methods
         ********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        public override void Entry(IModHelper helper)
        {
            this.Config = helper.ReadConfig<ModConfig>();
            ControlEvents.KeyPressed += this.ControlEvents_KeyPress;

        }

        /********
         ** Private methods
         ********/
        /// <summary>Open the to do list.</summary>
        private void OpenMenus()
        {
            //this.Monitor.Log($"Inside opening menu");

            // read file
            var model = this.Helper.ReadJsonFile<ModData>($"data/{Constants.SaveFolderName}.json") ?? new ModData();

            // write file (if needed)
            this.Helper.WriteJsonFile($"data/{Constants.SaveFolderName}.json", model);

            if (Game1.activeClickableMenu != null)
                Game1.exitActiveMenu();
            Game1.activeClickableMenu = new ToDoList(0, this.Config, this.SaveConfig, model);
        }

        /// <summary>Update the mod's config.json file from the current <see cref="Config"/>.</summary>
        private void SaveConfig()
        {
            this.Helper.WriteConfig(this.Config);
        }

        private void ControlEvents_KeyPress(object sender, EventArgsKeyPressed e)
        {
            if ((Context.IsWorldReady) && (Context.IsPlayerFree))
            {
                if (e.KeyPressed.ToString() == this.Config.OpenListKey)
                {
                    this.OpenMenus();
                }
            }

        }

    }
}