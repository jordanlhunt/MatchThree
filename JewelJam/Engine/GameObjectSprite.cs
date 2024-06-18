using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace JewelJam.Engine;
public class GameObjectSprite : GameObject
{
    #region Members Variables
    const float DRAW_SCALE = 1.0f;
    protected Texture2D sprite;
    protected Vector2 origin;
    #endregion
    #region Properties
    public int Width
    {
        get
        {
            return sprite.Width;
        }
    }
    public int Height
    {
        get
        {
            return sprite.Height;
        }
    }
    /// <summary>
    /// Gets a Rectangle that describes this sprite's current Bounding Box.
    /// Useful for collision detection
    /// </summary>
    public Rectangle BoundingBox
    {
        get
        {
            Rectangle spriteBounds = sprite.Bounds;
            // Add the sprites position to it as an offset
            spriteBounds.Offset(LocalPosition - origin);
            return spriteBounds;
        }
    }
    #endregion
    #region Constructor
    public GameObjectSprite(string spriteName)
    {
        sprite = ExtendedGame.ContentManager.Load<Texture2D>(spriteName);
        origin = Vector2.Zero;
    }
    #endregion
    #region Public Methods
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (IsVisible == true)
        {
            spriteBatch.Draw(sprite, GlobalPosition, null, Color.White, 0, origin, DRAW_SCALE, SpriteEffects.None, 0);
        }
    }
    #endregion
}
