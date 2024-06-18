using JewelJam.Engine;
using Point = Microsoft.Xna.Framework.Point;
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
            gameWorld.AddChild(backgroundTexture);
            JewelGrid gridOfJewels = new JewelGrid();
            gameWorld.AddChild(gridOfJewels);
            gameWorldSize = new Point(backgroundTexture.Width, backgroundTexture.Height);
            IsFullScreen = false;
        }

        #endregion
    }
}
