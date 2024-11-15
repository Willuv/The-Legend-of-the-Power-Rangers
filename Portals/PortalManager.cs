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
        private int portalWidth;
        private int portalHeight;
        private BluePortal bluePortal;
        private List<ICollision> loadedObjects;
        
        public PortalManager()
        {
            NeedsRoomAssignment = -1;
            //portalWidth = 36;
            //portalHeight = 80;
            portalWidth = 50;
            portalHeight = 50;
            PortalDelegator.OnBluePortalCreated += HandleBluePortalCreation;
            //PortalDelegator.OnOrangePortalCreated += HandleOrangePortalCreation;
        }

        private void HandleBluePortalCreation(Vector2 location, CollisionDirection projectileDirection)
        {
            if (bluePortal == null)
            {
                AddBluePortal(location, projectileDirection);
            } else
            {
                DelegateManager.RaiseObjectRemoved(bluePortal);
                AddBluePortal(location, projectileDirection);
            }
        }

        private void AddBluePortal(Vector2 location, CollisionDirection projectileDirection)
        {
            bluePortal = new BluePortal()
            {
                CollisionHitbox = new Rectangle((int)location.X, (int)location.Y, portalWidth, portalHeight)
            };
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

        public void Update(GameTime gameTime, int currentRoom, List<ICollision> loadedObjects)
        {
            this.loadedObjects = loadedObjects;

            if (NeedsRoomAssignment == 0) //blue needs
            {
                bluePortal.PortalRoom = currentRoom;
            } else if (NeedsRoomAssignment == 1) //orange
            {
                //orangePortal.PortalRoom = currentRoom;
                //loadedObjects.Add(orangePortal);
            }
            NeedsRoomAssignment = -1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bluePortal?.Draw(spriteBatch);
            //orange next
        }
    }
}
