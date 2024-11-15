using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Legend_of_the_Power_Rangers.LinkStateMachine;

namespace Legend_of_the_Power_Rangers.Portals
{
    public class PortalManager
    {
        private int NeedsRoomAssignment;
        private BluePortal bluePortal;
        
        public PortalManager()
        {
            NeedsRoomAssignment = -1;
            PortalDelegator.OnBluePortalCreated += HandleBluePortalCreation;
            //PortalDelegator.OnOrangePortalCreated += HandleOrangePortalCreation;
        }

        private void HandleBluePortalCreation(Vector2 location, CollisionDirection projectileDirection)
        {
            if (bluePortal == null)
            {
                bluePortal = new BluePortal();
                switch (projectileDirection)
                {
                    case CollisionDirection.Left:
                        break;
                    case CollisionDirection.Top:
                        break;
                    case CollisionDirection.Right:
                        break;
                    case CollisionDirection.Bottom:
                        break;
                }

                NeedsRoomAssignment = 0;
                DelegateManager.RaiseObjectCreated(bluePortal);
            }
        }

        public void Update(GameTime gameTime, int currentRoom, List<ICollision> loadedObjects)
        {
            if (NeedsRoomAssignment == 0) //blue needs
            {
                int index = loadedObjects.FindIndex(obj => obj is BluePortal);
                loadedObjects.RemoveAt(index);

                bluePortal.PortalRoom = currentRoom;

                loadedObjects.Add(bluePortal);
            } else if (NeedsRoomAssignment == 1) //orange
            {
                //orangePortal.PortalRoom = currentRoom;
                //loadedObjects.Add(orangePortal);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (bluePortal != null) bluePortal.Draw(spriteBatch);
            //orange next
        }
    }
}
