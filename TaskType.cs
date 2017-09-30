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
    class TaskType : IClickableMenu
    {
        protected TextBox textBox;
        public ClickableComponent textBoxCC;
        private TextBoxEvent e;

        public ClickableTextureComponent doneTypingButton;

        public bool Selected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public TaskType()
        {
            /*this.xPositionOnScreen = Game1.viewport.Width / 2 - (800 + IClickableMenu.borderWidth * 2) / 2;
            this.yPositionOnScreen = Game1.viewport.Height / 2 - (800 + IClickableMenu.borderWidth * 2) / 2;
            this.width = 800 + IClickableMenu.borderWidth * 2;
            this.height = 200 + IClickableMenu.borderWidth * 2;*/

            this.textBox = new TextBox((Texture2D)null, (Texture2D)null, Game1.dialogueFont, Game1.textColor);
            this.textBox.X = Game1.viewport.Width / 2 - (800 + IClickableMenu.borderWidth * 2) / 2 + Game1.tileSize;
            this.textBox.Y = Game1.viewport.Height / 2 + 200;
            this.textBox.Width = 700 + IClickableMenu.borderWidth * 2;
            this.textBox.Height = Game1.tileSize * 3;
            this.e = new TextBoxEvent(this.textBoxEnter);
            this.textBox.OnEnterPressed += this.e;
            Game1.keyboardDispatcher.Subscriber = (IKeyboardSubscriber)this.textBox;
            this.textBox.Text = "";
            this.textBox.Selected = true;
            this.textBox.limitWidth = false;
            ClickableTextureComponent textureComponent1 = new ClickableTextureComponent(new Rectangle(this.textBox.X + this.textBox.Width + Game1.tileSize + Game1.tileSize * 3 / 4 - Game1.pixelZoom * 2, Game1.viewport.Height / 2 + Game1.pixelZoom, Game1.tileSize, Game1.tileSize), Game1.mouseCursors, new Rectangle(381, 361, 10, 10), (float)Game1.pixelZoom, false);
            
            this.doneTypingButton = new ClickableTextureComponent(new Rectangle(this.textBox.X + this.textBox.Width + Game1.tileSize / 2 + Game1.pixelZoom, Game1.viewport.Height / 2 - Game1.pixelZoom * 2 + 200, Game1.tileSize, Game1.tileSize), Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 46, -1, -1), 1f, false);
            this.textBoxCC = new ClickableComponent(new Rectangle(this.textBox.X, this.textBox.Y, 192, 48), "")
            {
                myID = 104,
                rightNeighborID = 102
            };
        }

        public override void receiveKeyPress(Keys key)
        {
            if (this.textBox.Selected || Game1.options.doesInputListContain(Game1.options.menuButton, key))
                return;
            base.receiveKeyPress(key);
        }

        private void textBoxEnter(TextBox sender)
        {
            throw new NotImplementedException();
        }

        public override void receiveRightClick(int x, int y, bool playSound = true)
        {
            throw new NotImplementedException();
        }

        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            base.receiveLeftClick(x, y, playSound);
            
        }

        public override void draw(SpriteBatch batch)
        {
            base.draw(batch);
            this.textBox.Draw(batch);
            this.doneTypingButton.draw(batch);
        }
    }


        
    
    
}
