using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame
{
    public class TextBox
    {
        public double timer;
        public double timerBlink;
        public string value = "";
        //public Vector2 position;
        public Rectangle area;
        public bool focused = false;
        int maxCharacters = 0;

        public TextBox(int x, int y, int width, int height)
        {
            this.area.X = x;
            this.area.Y = y;
            this.area.Width = width;
            this.area.Height = height;

            maxCharacters = width / 13;
        }

        public void Update(GameTime gameTime)
        {
            if (focused)
            {
            KeyboardState lastKeyboardState;
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] keys = keyboardState.GetPressedKeys();
            Keys[] lastKeys = keyboardState.GetPressedKeys();
            Keys keyPressed = Keys.None;
            bool keyWasPressed = false;
            bool capsOn = false;

                foreach (Keys currentKey in keys)
                {
                    if (currentKey != Keys.None)
                    {
                        if (currentKey == Keys.LeftShift || currentKey == Keys.RightShift)
                        {
                            capsOn = true;
                        }
                        else
                        {
                            if (lastKeys.Contains(currentKey))
                            {
                                if ((gameTime.TotalGameTime.TotalMilliseconds - timer > 90))
                                {
                                    keyPressed = currentKey;
                                    keyWasPressed = true;
                                }
                            }
                            else if (!lastKeys.Contains(currentKey))
                            {
                                keyPressed = currentKey;
                                keyWasPressed = true;
                            }
                        }
                    }
                }

                if (keyWasPressed)
                {
                    HandleKey(gameTime, keyPressed, capsOn);
                }

                lastKeyboardState = keyboardState;
                lastKeys = keys;
            }
        }

        private void HandleKey(GameTime gameTime, Keys currentKey, bool capsOn)
        {
            string keyString = currentKey.ToString();

            if ((currentKey == Keys.Back || currentKey == Keys.Delete))
            {
                if (value.Length > 0)
                {
                    value = value.Remove(value.Length - 1);
                }
            }
            else
            {
                if (value.Length < maxCharacters)
                {
                    switch (currentKey)
                    {
                        case Keys.Enter:
                            break;
                        case Keys.Space:
                            value += " ";
                            break;
                        case Keys.OemComma:
                            value += ",";
                            break;
                        case Keys.OemQuotes:
                            value += "'";
                            break;
                        case Keys.OemQuestion:
                            value += "/";
                            break;
                        case Keys.D0:
                            value += "0";
                            break;
                        case Keys.D1:
                            value += "1";
                            break;
                        case Keys.D2:
                            value += "2";
                            break;
                        case Keys.D3:
                            value += "3";
                            break;
                        case Keys.D4:
                            value += "4";
                            break;
                        case Keys.D5:
                            value += "5";
                            break;
                        case Keys.D6:
                            value += "6";
                            break;
                        case Keys.D7:
                            value += "7";
                            break;
                        case Keys.D8:
                            value += "8";
                            break;
                        case Keys.D9:
                            value += "9";
                            break;
                        case Keys.OemSemicolon:
                            value += ";";
                            break;
                        case Keys.OemPeriod:
                            value += ".";
                            break;
                        default:
                            if (capsOn)
                            {
                                value += keyString.ToUpper();
                            }
                            else
                            {
                                value += keyString.ToLower();
                            }
                            break;
                    }
                }
            }
            

            //if ((currentKey == Keys.Back || currentKey == Keys.Delete))
            //{
            //    if (value.Length > 0)
            //    {
            //        value = value.Remove(value.Length - 1);
            //    }
            //}
            //else if (currentKey == Keys.Enter)
            //    return;
            //else if (currentKey == Keys.Space)
            //    value += " ";
            //else if (value.Length < maxCharacters)
            //    if (capsOn)
            //    {
            //        value += keyString.ToUpper();
            //    }
            //    else
            //    {
            //        value += keyString.ToLower();
            //    }

            timer = gameTime.TotalGameTime.TotalMilliseconds;

        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Dictionary<string, Texture2D> textureTable, GameTime gameTime)
        {
            int segments = (area.Width / 10) - 1;

            spriteBatch.Draw(textureTable["textboxLeft"], ToVector2(area.Location));
            for(int i = 1; i < segments; i++)
            {
                spriteBatch.Draw(textureTable["textboxMid"], new Vector2(area.X + (i * 10), area.Y));
            }
            spriteBatch.Draw(textureTable["textboxRight"], new Vector2(area.X + area.Width - 10, area.Y));


            Vector2 positionText;
            positionText.X = area.X + 10;
            positionText.Y = area.Y + 10;

            spriteBatch.DrawString(font, value, positionText, Color.Black);

            if (focused && value.Length != maxCharacters && gameTime.TotalGameTime.TotalMilliseconds - timerBlink < 500)
            {
                spriteBatch.Draw(textureTable["cursorBlink"], new Vector2(positionText.X + (value.Length * 11), positionText.Y + 18));
            }
            else if (gameTime.TotalGameTime.TotalMilliseconds - timerBlink > 1000)
            {
                timerBlink = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public Vector2 ToVector2(Point point)
        {
            return new Vector2(point.X, point.Y);
        }

    }
}
