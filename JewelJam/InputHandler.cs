using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace JewelJam
{
    class InputHandler
    {
        #region Fields
        private KeyboardState currentKeyBoardState;
        private KeyboardState previousKeyBoardState;
        private MouseState currentMouseState;
        private MouseState previousMouseState;
        #endregion
        #region Public Methods
        public void Update()
        {
            previousMouseState = currentMouseState;
            previousKeyBoardState = currentKeyBoardState;
            currentMouseState = Mouse.GetState();
            currentKeyBoardState = Keyboard.GetState();
        }
        public bool IsKeyPressed(Keys someKey)
        {
            return currentKeyBoardState.IsKeyDown(someKey) && previousKeyBoardState.IsKeyUp(someKey);
        }
        public bool IsMouseLeftButtonPressed()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
        }
        #endregion
        #region Properties
        public Vector2 MousePosition
        {
            get
            {
                return new Vector2(currentMouseState.X, currentMouseState.Y);
            }
        }
        #endregion
    }
}