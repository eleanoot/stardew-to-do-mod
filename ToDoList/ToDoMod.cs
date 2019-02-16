using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace ToDoMod
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        /********
        ** Properties
        ********/
        /// <summary>
        /// The mod settings.
        /// </summary>
        private ModConfig Config;

        /// <summary>
        /// The saved task data.
        /// </summary>
        private ModData Data;


        /********
        ** Public methods
        ********/
        /// <summary>
        /// The mod entry point, called after the mod is first loaded.
        /// </summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.Config = helper.ReadConfig<ModConfig>();

            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;
        }


        /********
        ** Private methods
        ********/
        /// <summary>
        /// Update the save file's saved task list.
        /// </summary>
        private void SaveData()
        {
            this.Helper.Data.WriteJsonFile($"data/{Constants.SaveFolderName}.json", this.Data);
        }

        /// <summary>
        /// Raised after the player presses a button on the keyboard, controller, or mouse.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            /* Only open if the save file is loaded and the player can move. */
            if ((Context.IsWorldReady) && (Context.IsPlayerFree))
            {
                /* Check if the key pressed is the one bound in the config file. */
                if (e.Button == this.Config.OpenListKey)
                {
                    /* If so open the to do list. */
                    this.OpenList();
                }
            }
        }

        /// <summary>Raised after the player loads a save slot and the world is initialised.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        public void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            /* If the user wants the list to load at game start, do that */
            if (this.Config.OpenAtStartup)
            {
                this.OpenList();
            }
        }

        /// <summary>
        /// Open the to do list.
        /// </summary>
        private void OpenList()
        {
            /* Read in the specific saved task list for the opened save file */
            /* Or create one if it doesn't exist yet. */
            this.Data = this.Helper.Data.ReadJsonFile<ModData>($"data/{Constants.SaveFolderName}.json") ?? new ModData();

            if (Game1.activeClickableMenu != null)
                Game1.exitActiveMenu();

            /* Open a to do list. */
            Game1.activeClickableMenu = new ToDoList(0, this.Config, this.Data, this.SaveData);
        }

    }
}