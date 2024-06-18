using System;
using JewelJam.Engine;
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
        LocalPosition = GRID_OFFSET;
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
                    LocalPosition = GetCellPosition(x, y),
                    Parent = this
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

    public override void HandleInput(InputHandler inputHandler)
    {
        if (inputHandler.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
        {
            MoveRowsDown();
        }
    }



    #endregion

    #region Private Methods
    Vector2 GetCellPosition(int x, int y)
    {
        return new Vector2(x * CELL_SIZE, y * CELL_SIZE);
    }

    /// <summary>
    /// Moves all jewels one row down, and then refills the top row of the grid with new random jewels.
    /// </summary>
    void MoveRowsDown()
    {
        // shift all rows down
        for (int y = GRID_HEIGHT - 1; y > 0; y--)
        {
            for (int x = 0; x < GRID_WIDTH; x++)
            {
                gridOfJewels[x, y] = gridOfJewels[x, y - 1];
                gridOfJewels[x, y].LocalPosition = GetCellPosition(x, y);
            }
        }

        // refill the top row
        for (int x = 0; x < GRID_WIDTH; x++)
        {
            gridOfJewels[x, 0] = new Jewel(ExtendedGame.Random.Next(3))
            {
                LocalPosition = GetCellPosition(x, 0),
                Parent = this
            };
        }
    }
    #endregion


}
