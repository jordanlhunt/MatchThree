using System;
using JewelJam.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace JewelJam;
public class Cursor : GameObjectSprite
{
    #region Member Variables
    #endregion
    #region Properties
    #endregion
    #region Constructor
    public Cursor(string spriteName) : base(spriteName)
    {
    }
    #endregion
    #region Public Method
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ExtendedGame extendedGame, InputHandler inputHandler)
    {
        spriteBatch.Draw(sprite, extendedGame.ScreenToGameWorld(inputHandler.MousePosition), Color.White);
    }
    #endregion
    #region Private Method
    #endregion
}
