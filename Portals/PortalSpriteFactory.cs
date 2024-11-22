using Microsoft.Xna.Framework.Graphics;
using System;

namespace Legend_of_the_Power_Rangers.Portals
{
    public class PortalSpriteFactory
    {
        private static PortalSpriteFactory instance = new();
        private Texture2D portalSpritesheet;

        public static PortalSpriteFactory Instance
        {
            get { return instance; }
        }

        private PortalSpriteFactory() { }

        public void SetPortalSpritesheet(Texture2D spritesheet)
        {
            portalSpritesheet = spritesheet;
        }

        public Texture2D GetPortalSpritesheet() => portalSpritesheet;
    }
}
