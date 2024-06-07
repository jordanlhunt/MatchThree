using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JewelJam;

public class GameObjectList : GameObject
{
    #region Member Variables
    List<GameObject> children;
    #endregion
    #region Properties
    #endregion
    #region Constructor
    public GameObject()
    {
        children = new List<GameObject>();
    }
    #endregion
    #region Public Methods
    public void AddChild(GameObject newChild)
    {
        newChild.Parent = this;
        children.Add(newChild);
    }
    public override void HandleInput(InputHandler inputHandler)
    {
        foreach (GameObject child in children)
        {
            child.HandleInput(inputHandler);
        }
    }
    public override void Update(GameTime gameTime)
    {
        foreach (GameObject child in children)
        {
            child.Update(gameTime);
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (GameObject child in children)
        {
            child.Draw(gameTime, spriteBatch);
        }
    }
    public override void Reset()
    {
        foreach (GameObject child in children)
        {
            child.Reset();
        }
    }
    #endregion
    #region Private Methods
    #endregion
}
