using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flygplan
{
    class MovingObject
    {
        public Vector2 Position { get; set; }
        private Texture2D Texture { get; set; }

        public MovingObject(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
        }
        public void Update(Vector2 velocity)
        {
            Position += velocity;
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.BurlyWood);
        }

        public float getPosX()
        {
            return Position.X;
        }

    }

    class Player : MovingObject 
    {
        public Player(Texture2D texture, Vector2 position) : base(position, texture)
        {
            
        }
    }

    class AdvancedMine : MovingObject
    {
        public AdvancedMine(Texture2D texture, Vector2 position) : base(position, texture)
        {

        }
    }

    class RegularMine : MovingObject
    {
        public RegularMine(Texture2D texture, Vector2 position) : base(position, texture)
        {

        }
    }

    
}

