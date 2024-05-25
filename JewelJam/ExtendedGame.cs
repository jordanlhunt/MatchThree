﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Point = Microsoft.Xna.Framework.Point;
namespace JewelJam;
public class ExtendedGame : Game
{
    #region Member Variables
    // Standard Monogame objects for sprites and graphics
    protected GraphicsDeviceManager graphics;
    protected SpriteBatch spriteBatch;
    // An object for handling keyboard and mouse input
    protected InputHandler inputHandler;
    // The width and height of the game, in game unit
    protected Point gameWorldSize;
    // Width and height of the window, in pixels
    protected Point windowSize;
    // A matrix used for scaling the game world so that it fits inside the window
    protected Matrix spriteScaleMatrix;
    const int DEFAULT_WINDOW_HEIGHT = 768;
    const int DEFAULT_WINDOW_WIDTH = 1024;
    const int DEFAULT_GAMEWORLD_HEIGHT = 768;
    const int DEFAULT_GAMEWORLD_WIDTH = 1024;
    #endregion
    #region Properties
    // Using the static keyword this property will be accessible everywhere
    public static Random Random
    {
        get;
        private set;
    }
    public static ContentManager ContentManager
    {
        get;
        private set;
    }
    public bool IsFullScreen
    {
        get
        {
            return graphics.IsFullScreen;
        }
        protected set
        {
            ApplyResolutionSettings(value);
        }
    }
    #endregion
    #region Constructors
    protected ExtendedGame()
    {
        Content.RootDirectory = "Content";
        graphics = new GraphicsDeviceManager(this);
        inputHandler = new InputHandler();
        Random = new Random();
        // Default Window Size and GameWorldSize
        windowSize = new Point(DEFAULT_WINDOW_HEIGHT, DEFAULT_WINDOW_WIDTH);
        gameWorldSize = new Point(DEFAULT_GAMEWORLD_HEIGHT, DEFAULT_GAMEWORLD_WIDTH);
    }
    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        // Store a static reference to the Content Manager;
        ContentManager = Content;
        IsFullScreen = false;
    }
    protected override void Update(GameTime gameTime)
    {
        HandleInput();
    }
    protected virtual void HandleInput()
    {
        inputHandler.Update();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || inputHandler.IsKeyPressed(Keys.Escape))
        {
            Exit();
        }
        // Toggle fullscreen mode when player hits F5
        if (inputHandler.IsKeyPressed(Keys.F5))
        {
            IsFullScreen = !IsFullScreen;
        }



    }
    /// <summary>
    /// This method takes a position in a screen coordinates as a parameter and returns the matching position in gameWorld coordinates
    /// </summary>
    public Vector2 ScreenToGameWorld(Vector2 screenPosition)
    {
        Vector2 viewportTopLeft = new Vector2(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y);
        float screenToWorldScale = (float)gameWorldSize.X / (float)GraphicsDevice.Viewport.Width;
        return (screenPosition - viewportTopLeft) * screenToWorldScale;
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
