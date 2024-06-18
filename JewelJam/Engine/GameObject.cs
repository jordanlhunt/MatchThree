using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace JewelJam.Engine;
public class GameObject
{
    #region Members Variables
    protected Vector2 velocity;
    #endregion
    #region Properties
    public Vector2 LocalPosition
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

    public Vector2 GlobalPosition
    {
        get
        {
            if (Parent == null)
            {
                return LocalPosition;
            }
            else
            {
                return LocalPosition + Parent.GlobalPosition;
            }
        }
    }
    #endregion
    #region Constructor
    public GameObject()
    {
        LocalPosition = Vector2.Zero;
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
        LocalPosition += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
