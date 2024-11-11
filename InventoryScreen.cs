using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Legend_of_the_Power_Rangers
{
    public class InventoryScreen
    {
        private Texture2D blackTexture;
        private Rectangle heartCover;
        private Rectangle keyCover;
        private Rectangle bombCover;
        private Rectangle BCover;
        private Rectangle rupeeXCover;
        private Rectangle rupeeNumberCover;
        private Texture2D InventoryTexture;
        private Rectangle InventorySourceRectangle = new Rectangle(0, 0, 255, 265);
        private Rectangle InventoryDestinationRectangle;
        private SpriteBatch InventorySpriteBatch;
        private readonly Rectangle bombCountPosition;
        private readonly Rectangle keyCountPosition;
        private readonly Rectangle rupeeCountPosition;
        private Link link;

        private const int HeartSize = 8;
        private const int ScaledHeartSize = 32;
        private const int HeartsPerRow = 8;


        private readonly Rectangle fullHeartSource = new Rectangle(176, 251, HeartSize, HeartSize);
        private readonly Rectangle halfHeartSource = new Rectangle(176, 242, HeartSize, HeartSize);
        private readonly Rectangle emptyHeartSource = new Rectangle(176, 233, HeartSize, HeartSize);

        public InventoryScreen(GraphicsDevice graphicsDevice, Texture2D hudTexture, Rectangle destinationRectangle)
        {
            this.InventoryTexture = hudTexture;
            this.InventorySpriteBatch = new SpriteBatch(graphicsDevice);
            this.InventoryDestinationRectangle = destinationRectangle;
            this.link = LinkManager.GetLink();

            blackTexture = new Texture2D(graphicsDevice, 1, 1);
            blackTexture.SetData(new[] { Color.Black });

            heartCover = new Rectangle(InventoryDestinationRectangle.X + 704, InventoryDestinationRectangle.Y + 800, 260, 68);
            BCover = new Rectangle(InventoryDestinationRectangle.X + 512, InventoryDestinationRectangle.Y + 768, 32, 64);
            rupeeXCover = new Rectangle(InventoryDestinationRectangle.X + 384, InventoryDestinationRectangle.Y + 738, 32, 32);

            rupeeCountPosition = new Rectangle(InventoryDestinationRectangle.X + 416, InventoryDestinationRectangle.Y + 738, 64, 32);
            bombCountPosition = new Rectangle(InventoryDestinationRectangle.X + 416, InventoryDestinationRectangle.Y + 832, 64, 32);
            keyCountPosition = new Rectangle(InventoryDestinationRectangle.X + 416, InventoryDestinationRectangle.Y + 800, 64, 32);
        }

        private readonly Dictionary<int, Rectangle> digitSourceRectangles = new Dictionary<int, Rectangle>
        {
            { 0, new Rectangle(96, 242, 8, 8) },
            { 1, new Rectangle(96, 251, 8, 8) },
            { 2, new Rectangle(96, 260, 8, 8) },
            { 3, new Rectangle(96, 269, 8, 8) },
            { 4, new Rectangle(96, 278, 8, 8) },
            { 5, new Rectangle(96, 287, 8, 8) },
            { 6, new Rectangle(96, 296, 8, 8) },
            { 7, new Rectangle(96, 305, 8, 8) },
            { 8, new Rectangle(96, 314, 8, 8) },
            { 9, new Rectangle(96, 323, 8, 8) }
        };

        private void DrawCovers()
        {
            Rectangle xSourceRectangle = new Rectangle(96, 233, 8, 8);

            InventorySpriteBatch.Draw(blackTexture, heartCover, Color.Black);
            InventorySpriteBatch.Draw(blackTexture, BCover, Color.Black);
            InventorySpriteBatch.Draw(blackTexture, bombCountPosition, Color.Black);
            InventorySpriteBatch.Draw(blackTexture, keyCountPosition, Color.Black);
            InventorySpriteBatch.Draw(InventoryTexture, rupeeXCover, xSourceRectangle, Color.White);
            InventorySpriteBatch.Draw(blackTexture, rupeeCountPosition, Color.Black);


        }

        private void DrawItemCount(int count, Rectangle position)
        {
            string countString = count.ToString();
            for (int i = 0; i < countString.Length; i++)
            {
                int digit = int.Parse(countString[i].ToString());
                Rectangle sourceRectangle = digitSourceRectangles[digit];

                Rectangle destinationRectangle = new Rectangle(position.X + i * 32, position.Y, 32, 32);

                InventorySpriteBatch.Draw(InventoryTexture, destinationRectangle, sourceRectangle, Color.White);
            }
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
                    InventoryDestinationRectangle.X + 704 + col * ScaledHeartSize,
                    InventoryDestinationRectangle.Y + 832 - row * ScaledHeartSize,
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

                InventorySpriteBatch.Draw(InventoryTexture, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        public void Draw()
        {
            InventorySpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            InventorySpriteBatch.Draw(InventoryTexture, InventoryDestinationRectangle, InventorySourceRectangle, Color.White);
            DrawCovers();
            DrawItemCount(LinkManager.GetLinkInventory().GetItemCount(ItemType.Rupee), rupeeCountPosition);
            DrawItemCount(LinkManager.GetLinkInventory().GetItemCount(ItemType.Bomb), bombCountPosition);
            DrawItemCount(LinkManager.GetLinkInventory().GetItemCount(ItemType.Key), keyCountPosition);
            DrawHearts();
            InventorySpriteBatch.End();
        }
    }
}