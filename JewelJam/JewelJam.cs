using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
namespace JewelJam
{
    public class JewelJam : Game
    {
        #region Fields
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D backgroundTexture;
        private Rectangle backgroundRectangle;
        Point gameWorldSize, windowSize;
        const int DEFAULT_WINDOW_WIDTH = 1024;
        const int DEFAULT_WINDOW_HEIGHT = 768;
        private Matrix spriteScaleMatrix;
        private InputHandler inputHandler;
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
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTexture = Content.Load<Texture2D>("spr_background");
            windowSize = new Point(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT);
            gameWorldSize = new Point(backgroundTexture.Width, backgroundTexture.Height);
            backgroundRectangle = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);
            IsFullScreen = false;
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            inputHandler.Update();
            if (inputHandler.IsKeyPressed(Keys.F5))
            {
                IsFullScreen = !IsFullScreen;
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, spriteScaleMatrix);
            spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
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
        #endregion
    }
}
