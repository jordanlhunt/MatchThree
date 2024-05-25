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
        private Texture2D backgroundTexture;
        private Texture2D cursorTexture;
        private Rectangle backgroundRectangle;
        Texture2D[] jewelsArray;
        const int DEFAULT_WINDOW_WIDTH = 1024;
        const int DEFAULT_WINDOW_HEIGHT = 768;
        const int GRID_WIDTH = 5;
        const int GRID_HEIGHT = 10;
        const int CELL_SIZE = 85;
        static Vector2 GRID_OFFSET = new Vector2(85, 150);
        static Random randomNumberGenerator;
        int[,] gridOfJewels;
        #endregion
        #region Public Methods
        public JewelJam()
        {
            IsMouseVisible = false;
            gridOfJewels = new int[GRID_WIDTH, GRID_HEIGHT];
            jewelsArray = new Texture2D[3];
            PopulateGridOfJewels();
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTexture = Content.Load<Texture2D>("spr_background");
            cursorTexture = Content.Load<Texture2D>("pickaxe");
            jewelsArray[0] = Content.Load<Texture2D>("spr_single_jewel0");
            jewelsArray[1] = Content.Load<Texture2D>("spr_single_jewel1");
            jewelsArray[2] = Content.Load<Texture2D>("spr_single_jewel2");
            windowSize = new Point(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT);
            gameWorldSize = new Point(backgroundTexture.Width, backgroundTexture.Height);
            backgroundRectangle = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);
            IsFullScreen = false;
        }
        protected override void Update(GameTime gameTime)
        {
            if (inputHandler.IsKeyPressed(Keys.Space))
            {
                MoveGridOfJewelsRowsDown();
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, spriteScaleMatrix);
            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
            DrawGridOfJewels();
            spriteBatch.Draw(cursorTexture, ScreenToGameWorld(inputHandler.MousePosition), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
        #region Private Methods

        /// <summary>
        /// This method populates the gridOfJewels with the values 0, 1, or 2
        /// </summary>
        private void PopulateGridOfJewels()
        {
            for (int x = 0; x < GRID_WIDTH; x++)
            {
                for (int y = 0; y < GRID_HEIGHT; y++)
                {
                    gridOfJewels[x, y] = Random.Next(3);
                }
            }
        }
        private void DrawGridOfJewels()
        {
            for (int x = 0; x < GRID_WIDTH; x++)
            {
                for (int y = 0; y < GRID_HEIGHT; y++)
                {
                    Vector2 spritePosition = GRID_OFFSET + new Vector2(x, y) * CELL_SIZE;
                    int jewelIndex = gridOfJewels[x, y];
                    spriteBatch.Draw(jewelsArray[jewelIndex], spritePosition, Color.White);
                }
            }
        }
        /// <summary>
        /// Moves all jewels one row down, and then refills the top row of the grid with new random jewels.
        /// </summary>
        private void MoveGridOfJewelsRowsDown()
        {
            for (int y = GRID_HEIGHT - 1; y > 0; y--)
            {
                for (int x = 0; x < GRID_WIDTH; x++)
                {
                    gridOfJewels[x, y] = gridOfJewels[x, y - 1];
                }
            }
            // Populate the top row with random jewels
            for (int x = 0; x < GRID_WIDTH; x++)
            {
                gridOfJewels[x, 0] = Random.Next(3);
            }
        }
        #endregion
    }
}
