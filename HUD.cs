using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Legend_of_the_Power_Rangers
{
    public class HUD
    {
        private Texture2D hudTexture;
        private Texture2D blackTexture;
        private Rectangle hudSourceRectangle = new Rectangle(0, 183, 255, 48);
        private Rectangle hudDestinationRectangle;
        private SpriteBatch hudSpriteBatch;
        private Link link;

        private Rectangle heartCover;

        private const int HeartSize = 8;
        private const int ScaledHeartSize = 32;
        private const int HeartsPerRow = 10;

        private readonly Rectangle fullHeartSource = new Rectangle(176, 251, HeartSize, HeartSize);
        private readonly Rectangle halfHeartSource = new Rectangle(176, 242, HeartSize, HeartSize);
        private readonly Rectangle emptyHeartSource = new Rectangle(176, 233, HeartSize, HeartSize);

        public HUD(GraphicsDevice graphicsDevice, Texture2D hudTexture, Rectangle destinationRectangle)
        {
            this.hudTexture = hudTexture;
            this.hudSpriteBatch = new SpriteBatch(graphicsDevice);
            this.hudDestinationRectangle = destinationRectangle;
            this.link = LinkManager.GetLink();

            blackTexture = new Texture2D(graphicsDevice, 1, 1);
            blackTexture.SetData(new[] { Color.Black });

            heartCover = new Rectangle(
                hudDestinationRectangle.X + 704,
                hudDestinationRectangle.Y + 100,
                260,
                68
            );
        }

        private void DrawCovers()
        {
            hudSpriteBatch.Draw(blackTexture, heartCover, Color.Black);
        }

        private void DrawHearts()
        {
            int fullHearts = link.GetCurrentHealth() / 2;
            bool hasHalfHeart = (link.GetCurrentHealth() % 2) == 1;
            int maxHearts = link.GetMaxHealth() / 2;

            for (int i = 0; i < maxHearts; i++)
            {
                int row = i / HeartsPerRow;
                int col = i % HeartsPerRow;

                Rectangle destinationRectangle = new Rectangle(
                    hudDestinationRectangle.X + 704 + col * ScaledHeartSize,
                    hudDestinationRectangle.Y + 132 - row * ScaledHeartSize,
                    ScaledHeartSize,
                    ScaledHeartSize
                );

                Rectangle sourceRectangle;

                if (i < fullHearts)
                {
                    sourceRectangle = fullHeartSource;
                }
                else if (i == fullHearts && hasHalfHeart)
                {
                    sourceRectangle = halfHeartSource;
                }
                else
                {
                    sourceRectangle = emptyHeartSource;
                }

                hudSpriteBatch.Draw(hudTexture, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        public void Draw()
        {
            hudSpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            hudSpriteBatch.Draw(hudTexture, hudDestinationRectangle, hudSourceRectangle, Color.White);
            DrawCovers();
            DrawHearts();

            hudSpriteBatch.End();
        }
    }
}
