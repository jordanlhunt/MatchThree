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
    public class JewelJam : Game
    {
        #region Member Variables
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D backgroundTexture;
        private Texture2D cursorTexture;
        private Rectangle backgroundRectangle;
        Point gameWorldSize, windowSize;
        Texture2D[] jewelsArray;
        const int DEFAULT_WINDOW_WIDTH = 1024;
        const int DEFAULT_WINDOW_HEIGHT = 768;
        const int GRID_WIDTH = 5;
        const int GRID_HEIGHT = 10;
        const int CELL_SIZE = 85;
        static Vector2 GRID_OFFSET = new Vector2(85, 150);
        private Matrix spriteScaleMatrix;
        private InputHandler inputHandler;
        static Random randomNumberGenerator;
        int[,] gridOfJewels;
        #endregion
        #region Properties
        bool IsFullScreen
        {
            get
            {
                return graphics.IsFullScreen;
            }
            set
            {
                ApplyResolutionSettings(value);
            }
        }
        #endregion
        #region Public Methods
        public JewelJam()
        {
            inputHandler = new InputHandler();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            gridOfJewels = new int[GRID_WIDTH, GRID_HEIGHT];
            randomNumberGenerator = new Random();
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
            inputHandler.Update();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (inputHandler.IsKeyPressed(Keys.F5))
            {
                IsFullScreen = !IsFullScreen;
            }
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
        /// Scales the window to the desired size, and calculates how the game world should be scaled to fit inside that window.
        /// </summary>
        private void ApplyResolutionSettings(bool isFullScreen)
        {
            graphics.IsFullScreen = isFullScreen;
            Point screenSize;
            if (isFullScreen)
            {
                screenSize = new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            }
            else
            {
                screenSize = windowSize;
            }
            graphics.PreferredBackBufferWidth = screenSize.X;
            graphics.PreferredBackBufferHeight = screenSize.Y;
            graphics.ApplyChanges();
            GraphicsDevice.Viewport = CalculateViewport(screenSize);
            spriteScaleMatrix = Matrix.CreateScale((float)GraphicsDevice.Viewport.Width / gameWorldSize.X, (float)GraphicsDevice.Viewport.Height / gameWorldSize.Y, 1);
        }
        private Viewport CalculateViewport(Point windowSize)
        {
            Viewport viewport = new Viewport();
            float gameAspectRatio = (float)gameWorldSize.X / (float)gameWorldSize.Y;
            float windowAspectRatio = (float)windowSize.X / (float)windowSize.Y;
            // If the window is wide use the full window height
            if (windowAspectRatio > gameAspectRatio)
            {
                viewport.Width = (int)(windowSize.Y * gameAspectRatio);
                viewport.Height = windowSize.Y;
            }
            // If the window is high, use the full window width
            else
            {
                viewport.Width = windowSize.X;
                viewport.Height = (int)(windowSize.X / gameAspectRatio);
            }
            viewport.X = (windowSize.X - viewport.Width) / 2;
            viewport.Y = (windowSize.Y - viewport.Height) / 2;
            return viewport;
        }
        /// <summary>
        /// This method takes a position in a screen coordinates as a parameter and returns the matching position in gameWorld coordinates
        /// </summary>
        private Vector2 ScreenToGameWorld(Vector2 screenPosition)
        {
            Vector2 viewportTopLeft = new Vector2(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y);
            float screenToWorldScale = (float)gameWorldSize.X / (float)GraphicsDevice.Viewport.Width;
            return (screenPosition - viewportTopLeft) * screenToWorldScale;
        }
        /// <summary>
        /// This method populates the gridOfJewels with the values 0, 1, or 2
        /// </summary>
        private void PopulateGridOfJewels()
        {
            for (int x = 0; x < GRID_WIDTH; x++)
            {
                for (int y = 0; y < GRID_HEIGHT; y++)
                {
                    gridOfJewels[x, y] = randomNumberGenerator.Next(3);
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
                gridOfJewels[x, 0] = randomNumberGenerator.Next(3);
            }
        }
        #endregion
    }
}
