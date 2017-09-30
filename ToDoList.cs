using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;


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

        private readonly ClickableTextureComponent UpArrow;
        private readonly ClickableTextureComponent DownArrow;
        private readonly ClickableTextureComponent Scrollbar;
        
        private Rectangle ScrollbarRunner;
        private bool CanClose;
        

        private readonly ClickableComponent Title;

        

        private TaskType taskType;

        




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
            this.UpArrow = new ClickableTextureComponent("up-arrow", new Rectangle(this.xPositionOnScreen + width + Game1.tileSize / 4, this.yPositionOnScreen + Game1.tileSize, 11 * Game1.pixelZoom, 12 * Game1.pixelZoom), "", "", Game1.mouseCursors, new Rectangle(421, 459, 11, 12), Game1.pixelZoom);
            this.DownArrow = new ClickableTextureComponent("down-arrow", new Rectangle(this.xPositionOnScreen + width + Game1.tileSize / 4, this.yPositionOnScreen + height - Game1.tileSize, 11 * Game1.pixelZoom, 12 * Game1.pixelZoom), "", "", Game1.mouseCursors, new Rectangle(421, 472, 11, 12), Game1.pixelZoom);
            this.Scrollbar = new ClickableTextureComponent("scrollbar", new Rectangle(this.UpArrow.bounds.X + Game1.pixelZoom * 3, this.UpArrow.bounds.Y + this.UpArrow.bounds.Height + Game1.pixelZoom, 6 * Game1.pixelZoom, 10 * Game1.pixelZoom), "", "", Game1.mouseCursors, new Rectangle(435, 463, 6, 10), Game1.pixelZoom);
            this.ScrollbarRunner = new Rectangle(this.Scrollbar.bounds.X, this.UpArrow.bounds.Y + this.UpArrow.bounds.Height + Game1.pixelZoom, this.Scrollbar.bounds.Width, height - Game1.tileSize * 2 - this.UpArrow.bounds.Height - Game1.pixelZoom * 2);

            this.Title = new ClickableComponent(new Rectangle(this.xPositionOnScreen + width / 2, this.yPositionOnScreen, Game1.tileSize * 4, Game1.tileSize), "To Do List");
            

            taskType = new TaskType();



        }

        public override void receiveRightClick(int x, int y, bool playSound = true)
        {

        }

        public override void receiveKeyPress(Keys key)
        {
            if ((Game1.options.menuButton.Contains(new InputButton(key)) || key == Keys.N) && this.readyToClose() && this.CanClose)
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
            this.UpArrow.draw(batch);
            this.DownArrow.draw(batch);

            IClickableMenu.drawTextureBox(batch, Game1.mouseCursors, new Rectangle(403, 383, 6, 6), this.ScrollbarRunner.X, this.ScrollbarRunner.Y, this.ScrollbarRunner.Width, this.ScrollbarRunner.Height, Color.White, Game1.pixelZoom, false);
            this.Scrollbar.draw(batch);
            this.taskType.draw(batch);
            
            this.drawMouse(batch);
            


        }
    }
}
