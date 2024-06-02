using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JewelJam;


public class JewelGrid : GameObject
{
    #region Member Variables
    Jewel[,] gridOfJewels;

    const int GRID_WIDTH = 5;
    const int GRID_HEIGHT = 10;
    const int CELL_SIZE = 85;
    static Vector2 GRID_OFFSET = new Vector2(85, 150);
    #endregion

    #region Properties
    #endregion

    #region Constructors
    public JewelGrid()
    {
        Position = GRID_OFFSET;
        Reset();
    }
    #endregion

    #region Public Methods
    public override void Reset()
    {
        gridOfJewels = new Jewel[GRID_WIDTH, GRID_HEIGHT];
        for (int x = 0; x < GRID_WIDTH; x++)
        {
            for (int y = 0; y < GRID_HEIGHT; y++)
            {
                gridOfJewels[x, y] = new Jewel(ExtendedGame.Random.Next(3))
                {
                    // Set the position of each jewel
                    Position = Position + new Vector2(x * CELL_SIZE, y * CELL_SIZE)
                };
            }
        }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        foreach (Jewel jewel in gridOfJewels)
        {
            jewel.Draw(gameTime, spriteBatch);
        }
    }
    #endregion

    #region Private Methods
    #endregion


}
