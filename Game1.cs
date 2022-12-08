using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace flygplan
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        private Texture2D playerTexture;
        private Texture2D regMineTexture;
        private Texture2D advancedMineTexture;
        private Vector2 startPos;
        private Vector2 velocityPlayer;
        private Texture2D _backgroundTexture;
        private bool isPlaying;

        //Hearts
        private List<MovingObject> mines;
        List<Heart> hearts = new List<Heart>();
        Random random = new Random();

        private int regularMineTimer;
        private int advancedMineTimer;
        private int gameScreenWidth;

        private Random rnd;

        private Player player;

        public int spawn { get; private set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            startPos = new Vector2(400, 400);
            velocityPlayer = Vector2.Zero;

            mines = new List<MovingObject>();

            regularMineTimer = 60;
            advancedMineTimer = 360;
            rnd = new Random();

            gameScreenWidth = Window.ClientBounds.Width;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerTexture = Content.Load<Texture2D>("fantasticPlane");
            advancedMineTexture = Content.Load<Texture2D>("advancedMine");
            regMineTexture = Content.Load<Texture2D>("regularMine");
            _backgroundTexture = Content.Load<Texture2D>("bakgrund-sol");

            player = new Player(playerTexture, startPos);
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Enter) && !isPlaying)
            {
                isPlaying = true;
            }

            if (!isPlaying)
            {
                return;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
                
            velocityPlayer = Vector2.Zero;

            if (state.IsKeyDown(Keys.W) && player.Position.Y > 0)
            {
                velocityPlayer += new Vector2(0, -2);
            }

            if (state.IsKeyDown(Keys.A) && player.Position.X > 0) 
            {
            
                velocityPlayer += new Vector2(-2, 0);
            }

            if (state.IsKeyDown(Keys.S) && player.Position.Y < Window.ClientBounds.Height - 32)
            {
                velocityPlayer += new Vector2(0, 2);

            }

            if (state.IsKeyDown(Keys.D) && player.Position.X < Window.ClientBounds.Width - 32 )
            {
                velocityPlayer += new Vector2(2, 0);
            }

            player.Update(velocityPlayer);

            regularMineTimer--;
            advancedMineTimer--;

            if (regularMineTimer == 0)
            {
                regularMineTimer = 60;

                int startPosX = rnd.Next(gameScreenWidth);

                mines.Add(new RegularMine(regMineTexture, new Vector2(startPosX, 0)));
            }

            if (advancedMineTimer == 0)
            {
                advancedMineTimer = 360;

                int startPosX = rnd.Next(gameScreenWidth);

                mines.Add(new AdvancedMine(advancedMineTexture, new Vector2(startPosX, 0)));
            }


            foreach (MovingObject mine in mines)
            {
                if (mine is RegularMine)
                {
                    mine.Update(new Vector2(0, 2));
                }

                if (mine is AdvancedMine)
                {
                    if (mine.getPosX() > player.getPosX())
                    {
                        mine.Update(new Vector2(-1.5f, 2));
                    }
                    else if (mine.getPosX() < player.getPosX())
                    {
                        mine.Update(new Vector2(-1.5f, 2));
                    }

                    else
                    {
                        mine.Update(new Vector2(0, 2));
                    }
                }
            }





            base.Update(gameTime);
        }

       

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            player.Draw(spriteBatch);



            foreach(MovingObject mine in mines)
            {
                mine.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}