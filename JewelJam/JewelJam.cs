using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
namespace JewelJam
{
    public class JewelJam : ExtendedGame
    {
        #region Member Variables
        #endregion
        #region Constructor
        public JewelJam()
        {
            IsMouseVisible = false;
        }
        #endregion
        #region Public Methods
        protected override void LoadContent()
        {
            base.LoadContent();
            GameObjectSprite backgroundTexture = new GameObjectSprite("spr_background");
            gameWorld.Add(backgroundTexture);
            JewelGrid gridOfJewels = new JewelGrid();
            gameWorld.Add(gridOfJewels);
            gameWorldSize = new Point(backgroundTexture.Width, backgroundTexture.Height);
            IsFullScreen = false;
        }

        #endregion
    }
}
