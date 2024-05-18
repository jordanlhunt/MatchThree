using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace JewelJam
{
    public class JewelJam : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D backgroundTexture;
        private Rectangle backgroundRectangle;
        Point gameWorldSize, windowSize;
        const int DEFAULT_WINDOW_WIDTH = 1024;
        const int DEFAULT_WINDOW_HEIGHT = 768;

        private Matrix spriteScaleMatrix;
        public JewelJam()
        {
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
            ApplyResolutionSettings();
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
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

        protected void ApplyResolutionSettings()
        {
            graphics.PreferredBackBufferWidth = windowSize.X;
            graphics.PreferredBackBufferHeight = windowSize.Y;

            graphics.ApplyChanges();
            spriteScaleMatrix = Matrix.CreateScale((float)windowSize.X / gameWorldSize.X, (float)windowSize.Y / gameWorldSize.Y, 1);
        }
    }
}
