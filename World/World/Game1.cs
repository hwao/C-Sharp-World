using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace World
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        /// <summary>
        /// Sta³a okreœlaj¹ca czy kod jest stabilny (produkcyjny)
        /// czy te¿ jest to wersja developerska - posiada mniej fixów w kodzie, ulatwia znajdowanie b³êdów
        /// </summary>
        public const bool STABLE = false;
        bool debugViewport = false;

        public SpriteFont _spr_font;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Element.DisplayFps DisplayFps;
        Viewport[] Viewport;
        Player Player;

        WorldMap WorldMap;
        Texture2D pixel;

        KeyboardState LastkeyState;

        World.Map.Field.iField OldField;

        /// <summary>
        /// Start gry
        /// </summary>
        public Game1()
        {
            this.IsFixedTimeStep = false;
            this.IsMouseVisible = true;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1820;
            graphics.PreferredBackBufferHeight = 1100;
            if (this.debugViewport != true)
            {
                graphics.PreferredBackBufferWidth /= 2;
                graphics.PreferredBackBufferHeight /= 2;
            }
            graphics.IsFullScreen = false;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.ApplyChanges();

            //this.ViewportOld = new Rectangle(150, 150, 800/2, 400/2);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.DisplayFps = new Element.DisplayFps();

            int size = 100;
            this.WorldMap = new WorldMap(size, size, this);

            this.Viewport = new Viewport[1];

            Rectangle Screen = new Rectangle(0, 0, 400, 300);
            Screen.Width = graphics.PreferredBackBufferWidth;
            Screen.Height = graphics.PreferredBackBufferHeight;
            if (this.debugViewport == true)
            {
                Screen.X = 200;
                Screen.Y = 200;
                Screen.Width = 400;
                Screen.Height = 300;
            }
            this.Viewport[0] = new Viewport(this.WorldMap, Screen, new Vector2(100, 50), new Vector2(0, 0));
            this.LastkeyState = Keyboard.GetState();
            this.OldField = this.WorldMap.getField(0, 0);

            this.Player = new Player(new Vector2(100, 100), 10);

            base.Initialize();


            this.UpdateTitle();
        }

        public void UpdateTitle()
        {
            Window.Title = string.Format("Mapa: {0} x {1} Okno: {2} x {3} Zoom: {4}", this.WorldMap.getWidth(), this.WorldMap.getHeight(), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, Viewport[0].Zoom);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White }); // so that we can draw whatever color we want on top of it

            // TODO: use this.Content to load your game content here
            this.WorldMap.LoadContent(this.Content);

            this.Player.LoadContent(this.Content);

            _spr_font = Content.Load<SpriteFont>("SpriteFont1");
            this.DisplayFps.LoadContent(_spr_font);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            this.DisplayFps.Update(gameTime);

            MouseState mouseState = Mouse.GetState();
            World.Map.Field.iField Field = this.Viewport[0].getField(mouseState.X, mouseState.Y);
            if (Field != OldField)
            {
                Field.alpha = 0.5f;
                OldField.alpha = 1.0f;
                OldField = Field;
            }

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.A))
                this.Viewport[0].Camera.X -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (keyState.IsKeyDown(Keys.D))
                this.Viewport[0].Camera.X += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (keyState.IsKeyDown(Keys.W))
                this.Viewport[0].Camera.Y -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (keyState.IsKeyDown(Keys.S))
                this.Viewport[0].Camera.Y += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            /*if (keyState.IsKeyDown(Keys.T) && !this.LastkeyState.IsKeyDown(Keys.T))
                this.Viewport[0].Zoom *= 0.5f;
            if (keyState.IsKeyDown(Keys.G) && !this.LastkeyState.IsKeyDown(Keys.G))
                this.Viewport[0].Zoom /= 0.5f;
            */

            if (keyState.IsKeyDown(Keys.T) && !this.LastkeyState.IsKeyDown(Keys.T))
                this.Viewport[0].Zoom *= 2.5f;
            if (keyState.IsKeyDown(Keys.G) && !this.LastkeyState.IsKeyDown(Keys.G))
                this.Viewport[0].Zoom /= 2.5f;


            if (keyState.IsKeyDown(Keys.Left))
                this.Player.Position.X -= 2;
            if (keyState.IsKeyDown(Keys.Right))
                this.Player.Position.X += 2;
            if (keyState.IsKeyDown(Keys.Up))
                this.Player.Position.Y -= 2;
            if (keyState.IsKeyDown(Keys.Down))
                this.Player.Position.Y += 2;


            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyState.IsKeyDown(Keys.Q) || keyState.IsKeyDown(Keys.Escape))
                this.Exit();

            this.LastkeyState = keyState;

            // TODO: Add your update logic here

            this.UpdateTitle();

            base.Update(gameTime);
        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //this.WorldMap.Draw(spriteBatch, v2Camera, ViewportOld );



            foreach (Viewport View in this.Viewport)
            {
                View.Draw(spriteBatch);
                if (this.debugViewport == true)
                    DrawBorder(View.Screen, 2, Color.Red);
            }


            this.Player.Draw(spriteBatch, this.Viewport[0].Camera, this.Viewport[0].Screen, 1.0f);
            //DrawBorder(this.Player.getRectangle( this.Viewport[0].Camera, this.Viewport[0].Screen ), 1, Color.Red);

            this.DisplayFps.Draw(gameTime, spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawRectangle(Rectangle coords, Color color)
        {
            var rect = new Texture2D(GraphicsDevice, 1, 1);
            rect.SetData(new[] { color });
            spriteBatch.Draw(rect, coords, color);
        }

        /// <summary>
        /// Will draw a border (hollow rectangle) of the given 'thicknessOfBorder' (in pixels)
        /// of the specified color.
        ///
        /// By Sean Colombo, from http://bluelinegamestudios.com/blog
        /// </summary>
        /// <param name="rectangleToDraw"></param>
        /// <param name="thicknessOfBorder"></param>
        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);
        }
    }
}
