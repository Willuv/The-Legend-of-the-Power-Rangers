using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Legend_of_the_Power_Rangers
{
    public class HUD
    {
        private Texture2D hudTexture;
        private Texture2D blackTexture;
        private Texture2D whiteTexture;
        private Texture2D redTexture;
        private Rectangle hudSourceRectangle = new Rectangle(0, 183, 255, 48);
        private Rectangle hudDestinationRectangle;
        private SpriteBatch hudSpriteBatch;
        private int currentRoom;
        private Link link;

        private Rectangle heartCover;
        private Rectangle keyCover;
        private Rectangle bombCover;
        private Rectangle BCover;
        private Rectangle rupeeXCover;
        private Rectangle rupeeNumberCover;
        private Rectangle fullMapCover;
        private Rectangle linkPosition;
        private Rectangle bossRoom;
        private List<Rectangle> mapLayoutCover;
        private List<Rectangle> rooms;

        private readonly Rectangle bombCountPosition;
        private readonly Rectangle keyCountPosition;
        private readonly Rectangle rupeeCountPosition;

        private const int HeartSize = 8;
        private const int ScaledHeartSize = 32;
        private const int HeartsPerRow = 8;


        private readonly Rectangle fullHeartSource = new Rectangle(176, 251, HeartSize, HeartSize);
        private readonly Rectangle halfHeartSource = new Rectangle(176, 242, HeartSize, HeartSize);
        private readonly Rectangle emptyHeartSource = new Rectangle(176, 233, HeartSize, HeartSize);


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

        public HUD(GraphicsDevice graphicsDevice, Texture2D hudTexture, Rectangle destinationRectangle, int currentRoom)
        {
            this.hudTexture = hudTexture;
            this.hudSpriteBatch = new SpriteBatch(graphicsDevice);
            this.hudDestinationRectangle = destinationRectangle;
            this.link = LinkManager.GetLink();
            this.currentRoom = currentRoom;

            blackTexture = new Texture2D(graphicsDevice, 1, 1);
            blackTexture.SetData(new[] { Color.Black });

            whiteTexture = new Texture2D(graphicsDevice, 1, 1);
            whiteTexture.SetData(new[] { Color.White });

            redTexture = new Texture2D(graphicsDevice, 1, 1);
            redTexture.SetData(new[] { Color.Red });

            heartCover = new Rectangle(hudDestinationRectangle.X + 704, hudDestinationRectangle.Y + 100, 260, 68);
            BCover = new Rectangle(hudDestinationRectangle.X + 512, hudDestinationRectangle.Y + 68, 32, 64);
            rupeeXCover = new Rectangle(hudDestinationRectangle.X + 384, hudDestinationRectangle.Y + 36, 32, 32);
            fullMapCover = new Rectangle(hudDestinationRectangle.X + 64, hudDestinationRectangle.Y + 36, 252, 124);
            bossRoom = new Rectangle(hudDestinationRectangle.X + 262, hudDestinationRectangle.Y + 84, 16, 12);

            rupeeCountPosition = new Rectangle(hudDestinationRectangle.X + 416, hudDestinationRectangle.Y + 36, 64, 32);
            bombCountPosition = new Rectangle(hudDestinationRectangle.X + 416, hudDestinationRectangle.Y + 132, 64, 32);
            keyCountPosition = new Rectangle(hudDestinationRectangle.X + 416, hudDestinationRectangle.Y + 100, 64, 32);

            mapLayoutCover = new List<Rectangle>();
            rooms = new List<Rectangle>();

            int startX = hudDestinationRectangle.X + 64;
            int startY = hudDestinationRectangle.Y + 36;
            int width = 28;
            int height = 12;

            int totalRows = 8;
            List<int[]> skippedRows = new List<int[]>
            {
                new int[] { },         
                new int[] { 4 },      
                new int[] { 2, 4, 5, 7},   
                new int[] { 2, 3, 4, 5, 6, 7 }, 
                new int[] { 4, 5, 7 },       
                new int[] { 3, 4 },    
                new int[] { 3 },        
                new int[] { }         
            };

            for (int col = 0; col < skippedRows.Count; col++)
            {
                HashSet<int> skips = new HashSet<int>(skippedRows[col]);

                for (int row = 0; row < totalRows; row++)
                {
                    int x = startX + col * 32;
                    int y = startY + row * 16;
                    Rectangle rect = new Rectangle(x, y, width, height);

                    if (skips.Contains(row))
                    {
                        rooms.Add(rect);
                        continue;
                    }

                    mapLayoutCover.Add(rect);
                }
            }
        }


        private void DrawCovers()
        {
            Rectangle xSourceRectangle = new Rectangle(96, 233, 8, 8);

            hudSpriteBatch.Draw(blackTexture, heartCover, Color.Black);
            hudSpriteBatch.Draw(blackTexture, BCover, Color.Black);
            hudSpriteBatch.Draw(blackTexture, bombCountPosition, Color.Black);
            hudSpriteBatch.Draw(blackTexture, keyCountPosition, Color.Black);
            hudSpriteBatch.Draw(hudTexture, rupeeXCover, xSourceRectangle, Color.White);
            hudSpriteBatch.Draw(blackTexture, rupeeCountPosition, Color.Black);


        }

        private void DrawItemCount(int count, Rectangle position)
        {
            string countString = count.ToString();
            for (int i = 0; i < countString.Length; i++)
            {
                int digit = int.Parse(countString[i].ToString());
                Rectangle sourceRectangle = digitSourceRectangles[digit];

                Rectangle destinationRectangle = new Rectangle(position.X + i * 32, position.Y, 32, 32);

                hudSpriteBatch.Draw(hudTexture, destinationRectangle, sourceRectangle, Color.White);
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


        private void DrawMap()
        {
            if (LinkManager.GetLinkInventory().HasItem(ItemType.Map))
            {
                foreach (var roomRectangle in mapLayoutCover)
                {
                    hudSpriteBatch.Draw(blackTexture, roomRectangle, Color.Black);
                }
            }
            else
            {
                hudSpriteBatch.Draw(blackTexture, fullMapCover, Color.Black);
            }

            if (LinkManager.GetLinkInventory().HasItem(ItemType.Compass))
            {
                hudSpriteBatch.Draw(redTexture, bossRoom, Color.Red);
            }

        }

        public void Draw()
        {
            hudSpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            hudSpriteBatch.Draw(hudTexture, hudDestinationRectangle, hudSourceRectangle, Color.White);
            DrawCovers();
            DrawMap();
            DrawItemCount(LinkManager.GetLinkInventory().GetItemCount(ItemType.Rupee), rupeeCountPosition);
            DrawItemCount(LinkManager.GetLinkInventory().GetItemCount(ItemType.Bomb), bombCountPosition);
            DrawItemCount(LinkManager.GetLinkInventory().GetItemCount(ItemType.Key), keyCountPosition);
            DrawHearts();
            hudSpriteBatch.Draw(whiteTexture, linkPosition, Color.White);



            hudSpriteBatch.End();
        }

        public void Update(int currentRoom)
        {
            switch (currentRoom)
            {
                case 1:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 166, hudDestinationRectangle.Y + 148, 16, 12);
                    break;
                case 2:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 134, hudDestinationRectangle.Y + 148, 16, 12);
                    break;
                case 3:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 198, hudDestinationRectangle.Y + 148, 16, 12);
                    break;
                case 4:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 166, hudDestinationRectangle.Y + 132, 16, 12);
                    break;
                case 5:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 166, hudDestinationRectangle.Y + 116, 16, 12);
                    break;
                case 6:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 134, hudDestinationRectangle.Y + 116, 16, 12);
                    break;
                case 7:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 198, hudDestinationRectangle.Y + 116, 16, 12);
                    break;
                case 8:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 166, hudDestinationRectangle.Y + 100, 16, 12);
                    break;
                case 9:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 134, hudDestinationRectangle.Y + 100, 16, 12);
                    break;
                case 10:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 102, hudDestinationRectangle.Y + 100, 16, 12);
                    break;
                case 11:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 198, hudDestinationRectangle.Y + 100, 16, 12);
                    break;
                case 12:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 230, hudDestinationRectangle.Y + 100, 16, 12);
                    break;
                case 13:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 166, hudDestinationRectangle.Y + 84, 16, 12);
                    break;
                case 14:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 230, hudDestinationRectangle.Y + 84, 16, 12);
                    break;
                case 15:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 252, hudDestinationRectangle.Y + 84, 16, 12);
                    break;
                case 16:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 166, hudDestinationRectangle.Y + 68, 16, 12);
                    break;
                case 17:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 134, hudDestinationRectangle.Y + 68, 16, 12);
                    break;
                case 18:
                    linkPosition = new Rectangle(hudDestinationRectangle.X + 102, hudDestinationRectangle.Y + 68, 16, 12);
                    break;
            }
        }
    }
}
