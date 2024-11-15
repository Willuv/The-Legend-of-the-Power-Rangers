using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend_of_the_Power_Rangers.Portals
{
    public class BluePortal : IPortal
    {
        private Rectangle sourceRectangle = new(0, 0, 73, 160);
        private Rectangle destinationRectangle = new(450, 340, 48, 48);
        public Rectangle CollisionHitbox
        {
            get { return destinationRectangle; }
            set { destinationRectangle = value; }
        }
        public ObjectType ObjectType { get { return ObjectType.Portal; } }
        public PortalType PortalType { get { return PortalType.Blue; } }
        public int PortalRoom { get; set; }
        public Vector2 TeleportPosition { get; set; }

        public BluePortal()
        {
            PortalRoom = 1; //default
            TeleportPosition = Vector2.Zero; //default
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PortalSpriteFactory.Instance.GetPortalSpritesheet(), destinationRectangle, sourceRectangle, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);
        }
    }
}
