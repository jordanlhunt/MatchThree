using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace JewelJam;
public class GameObject
{
    #region Members Variables
    protected Vector2 velocity;
    #endregion
    #region Properties
    public Vector2 Position
    {
        get;
        set;
    }
    public bool IsVisible
    {
        get;
        set;
    }
    public GameObject Parent
    {
        get;
        set;
    }
    #endregion
    #region Constructor
    public GameObject()
    {
        Position = Vector2.Zero;
        velocity = Vector2.Zero;
        IsVisible = true;
    }
    #endregion
    #region Public Methods
    public virtual void HandleInput(InputHandler inputHandler)
    {
    }

    public virtual void Update(GameTime gameTime)
    {
        Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
    }

    public virtual void Reset()
    {
        velocity = Vector2.Zero;
    }
    #endregion
}
