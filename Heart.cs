using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flygplan
{
    class Heart
    {
        public Texture2D Texture;
        public Vector2 position;
        public Vector2 velocity;

        public bool isVisible = true;

        Random random = new Random();
        int randX, randY;

        public Heart(Texture2D newTexture, Vector2 newPosition)
        {
            Texture = newTexture;
            position = newPosition;

            randY = random.Next(-4, 4);
            randX = random.Next(-4, -1);

            velocity = new Vector2(randX, randY);   
        }

        public void Update(GraphicsDevice graphics)
        {
            position += velocity;

            if (position.Y <= 0 || position.Y >= graphics.Viewport.Height - Texture.Height)
                velocity.Y = -velocity.Y;

            if (position.X < 0 - Texture.Width)
                isVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, Color.WhiteSmoke);
        }

    }
}
