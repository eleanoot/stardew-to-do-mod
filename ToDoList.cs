using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;
using StardewValley.BellsAndWhistles;


namespace ToDoMod
{
    class ToDoList : IClickableMenu
    {
        /*********
        ** Properties
        *********/
        /// <summary>The mod settings.</summary>
        private readonly ModConfig Config;
        /// <summary>Saving the mod settings.</summary>
        private readonly Action SaveConfig;

        private bool CanClose;
        private readonly ClickableComponent Title;

        private TaskType taskType;

        private int TaskPage = -1;
        public const int tasksPerPage = 6;
        private List<List<String>> taskPages;
        private List<String> loadedTaskNames;
        private List<ClickableComponent> taskPageButtons;
        private int currentPage;
        public const int region_forwardButton = 101;
        public const int region_backButton = 102;
        public ClickableTextureComponent forwardButton;
        public ClickableTextureComponent backButton;

        /*********
        ** Public methods
        *********/

        public ToDoList(int currentIndex, ModConfig config, Action saveConfig)
            : base(Game1.viewport.Width / 2 - (800 + IClickableMenu.borderWidth * 2) / 2,
                   Game1.viewport.Height / 2 - (600 + IClickableMenu.borderWidth * 2) / 2,
                   800 + IClickableMenu.borderWidth * 2,
                   450 + IClickableMenu.borderWidth * 2,
                   true)
        {
            this.Config = config;
            this.SaveConfig = saveConfig;
            
            this.Title = new ClickableComponent(new Rectangle(this.xPositionOnScreen + width / 2, this.yPositionOnScreen, Game1.tileSize * 4, Game1.tileSize), "To Do List");

            this.upperRightCloseButton = new ClickableTextureComponent(new Rectangle(this.xPositionOnScreen + this.width - 5 * Game1.pixelZoom, this.yPositionOnScreen - 2 * Game1.pixelZoom, 12 * Game1.pixelZoom, 12 * Game1.pixelZoom), Game1.mouseCursors, new Rectangle(337, 494, 12, 12), (float)Game1.pixelZoom, false);

            taskType = new TaskType();
            //Game1.activeClickableMenu = taskType;

            this.loadedTaskNames = new List<String>();
            this.taskPageButtons = new List<ClickableComponent>();
            for (int index = 0; index < 6; ++index)
            {
                List<ClickableComponent> taskPageButtons = this.taskPageButtons;
                ClickableComponent clickableComponent = new ClickableComponent(new Rectangle(this.xPositionOnScreen + Game1.tileSize / 4, this.yPositionOnScreen + Game1.tileSize / 4 + index * ((this.height - Game1.tileSize / 2) / 6), this.width - Game1.tileSize / 2, (this.height - Game1.tileSize / 2) / 6 + Game1.pixelZoom), string.Concat((object)index));
                clickableComponent.myID = index;
                clickableComponent.downNeighborID = -7777;
                int num1 = index > 0 ? index - 1 : -1;
                clickableComponent.upNeighborID = num1;
                int num2 = -7777;
                clickableComponent.rightNeighborID = num2;
                int num3 = -7777;
                clickableComponent.leftNeighborID = num3;
                int num4 = 1;
                clickableComponent.fullyImmutable = num4 != 0;
                taskPageButtons.Add(clickableComponent);
            }

            ClickableTextureComponent textureComponent1 = new ClickableTextureComponent(new Rectangle(this.xPositionOnScreen - Game1.tileSize, this.yPositionOnScreen + Game1.pixelZoom * 2, 12 * Game1.pixelZoom, 11 * Game1.pixelZoom), Game1.mouseCursors, new Rectangle(352, 495, 12, 11), (float)Game1.pixelZoom, false);
            int num5 = 102;
            textureComponent1.myID = num5;
            int num6 = -7777;
            textureComponent1.rightNeighborID = num6;
            this.backButton = textureComponent1;
            ClickableTextureComponent textureComponent2 = new ClickableTextureComponent(new Rectangle(this.xPositionOnScreen + this.width + Game1.tileSize - 12 * Game1.pixelZoom, this.yPositionOnScreen + this.height - 12 * Game1.pixelZoom, 12 * Game1.pixelZoom, 11 * Game1.pixelZoom), Game1.mouseCursors, new Rectangle(365, 495, 12, 11), (float)Game1.pixelZoom, false);
            int num7 = 101;
            textureComponent2.myID = num7;
            this.forwardButton = textureComponent2;
            ClickableTextureComponent textureComponent3 = new ClickableTextureComponent(new Rectangle(this.xPositionOnScreen + this.width / 2 - Game1.pixelZoom * 20, this.yPositionOnScreen + this.height - Game1.tileSize / 2 - 24 * Game1.pixelZoom, 24 * Game1.pixelZoom, 24 * Game1.pixelZoom), Game1.mouseCursors, new Rectangle(293, 360, 24, 24), (float)Game1.pixelZoom, true);
            



            loadTaskList();
            pageTasks();

        }

        private void pageTasks()
        {
            this.taskPages = new List<List<String>>();
            for (int index = loadedTaskNames.Count - 1; index >= 0; --index)
            {
                int num = loadedTaskNames.Count - 1 - index;
                if (this.taskPages.Count <= num / 6)
                    this.taskPages.Add(new List<String>());
                this.taskPages[num / 6].Add(loadedTaskNames[index]);
            }
            this.currentPage = Math.Min(Math.Max(this.currentPage, 0), this.taskPages.Count - 1);
            this.TaskPage = -1;
        }

        private void loadTaskList()
        {
            if (!File.Exists("C:\\Users\\grego\\source\\repos\\ToDoMod\\ToDoMod\\TaskList.txt"))
            {
                using (FileStream fs = File.Create("C:\\Users\\grego\\source\\repos\\ToDoMod\\ToDoMod\\TaskList.txt"))
                {

                }
            }
            using (StreamReader sr = File.OpenText("C:\\Users\\grego\\source\\repos\\ToDoMod\\ToDoMod\\TaskList.txt"))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    loadedTaskNames.Add(s);
                }
            }
        }

        public override void receiveRightClick(int x, int y, bool playSound = true)
        {

        }

        public override void receiveKeyPress(Keys key)
        {
            if (Game1.options.doesInputListContain(Game1.options.menuButton, key))
            {
                return;
            }
            if ((Game1.options.menuButton.Contains(new InputButton(key)) || key == Keys.F2) && this.readyToClose() && this.CanClose)
            {
                Game1.exitActiveMenu();
                return;
            }

            this.CanClose = true;
        }







        public override void draw(SpriteBatch batch)
        {
            Game1.drawDialogueBox(this.xPositionOnScreen, this.yPositionOnScreen, this.width, this.height, false, true);
            batch.End();

            batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
            

            
            this.taskType.draw(batch);

            

            /* Task boxes */

            if (this.TaskPage == -1)
            {
                for (int index = 0; index < this.taskPageButtons.Count; ++index)
                {
                    if (this.taskPages.Count<List<String>>() > 0 && this.taskPages[this.currentPage].Count<String>() > index)
                    {
                        IClickableMenu.drawTextureBox(batch, Game1.mouseCursors, new Rectangle(384, 396, 15, 15), this.taskPageButtons[index].bounds.X, this.taskPageButtons[index].bounds.Y, this.taskPageButtons[index].bounds.Width, this.taskPageButtons[index].bounds.Height, this.taskPageButtons[index].containsPoint(Game1.getOldMouseX(), Game1.getOldMouseY()) ? Color.Wheat : Color.White, (float)Game1.pixelZoom, false);
                        Utility.drawWithShadow(batch, Game1.mouseCursors, new Vector2((float)(this.taskPageButtons[index].bounds.X + Game1.tileSize / 2), (float)(this.taskPageButtons[index].bounds.Y + Game1.pixelZoom * 7)), new Rectangle(410, 501, 9, 9), Color.White, 0.0f, Vector2.Zero, (float)Game1.pixelZoom, false, 0.99f, -1, -1, 0.35f);
                        SpriteText.drawString(batch, this.taskPages[this.currentPage][index], this.taskPageButtons[index].bounds.X + Game1.tileSize * 2 + Game1.pixelZoom, this.taskPageButtons[index].bounds.Y + Game1.pixelZoom * 5, 999999, -1, 999999, 1f, 0.88f, false, -1, "", -1);
                        
                    }
                }
            }

            if (this.currentPage < this.taskPages.Count - 1 && this.TaskPage == -1)
                this.forwardButton.draw(batch);
            if (this.currentPage > 0 || this.TaskPage != -1)
                this.backButton.draw(batch);
            base.draw(batch);

            Game1.mouseCursorTransparency = 1f;
            this.drawMouse(batch);


        }

        public void Entry(IModHelper helper)
        {
            throw new NotImplementedException();
        }
    }
}