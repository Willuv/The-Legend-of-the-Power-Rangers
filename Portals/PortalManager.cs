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
        private OrangePortal orangePortal;
        private List<ICollision> loadedObjects;
        
        public PortalManager()
        {
            NeedsRoomAssignment = -1;
            //portalWidth = 36;
            //portalHeight = 80;
            portalWidth = 50;
            portalHeight = 50;
            PortalDelegator.OnBluePortalCreated += HandleBluePortalCreation;
            PortalDelegator.OnOrangePortalCreated += HandleOrangePortalCreation;
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
                CollisionHitbox = new Rectangle((int)location.X, (int)location.Y, portalWidth, portalHeight),
                TeleportPosition = location
            };
            switch (projectileDirection)
            {
                case CollisionDirection.Left:
                    bluePortal.LinkDirection = LinkDirection.Left;
                    break;
                case CollisionDirection.Top:
                    bluePortal.LinkDirection = LinkDirection.Up;
                    break;
                case CollisionDirection.Right:
                    bluePortal.LinkDirection = LinkDirection.Right;
                    break;
                case CollisionDirection.Bottom:
                    bluePortal.LinkDirection = LinkDirection.Down;
                    break;
            }

            NeedsRoomAssignment = 0;
            DelegateManager.RaiseObjectCreated(bluePortal);
        }

        private void HandleOrangePortalCreation(Vector2 location, CollisionDirection projectileDirection)
        {
            if (orangePortal == null)
            {
                AddOrangePortal(location, projectileDirection);
            }
            else
            {
                DelegateManager.RaiseObjectRemoved(orangePortal);
                AddOrangePortal(location, projectileDirection);
            }
        }

        private void AddOrangePortal(Vector2 location, CollisionDirection projectileDirection)
        {
            orangePortal = new OrangePortal()
            {
                CollisionHitbox = new Rectangle((int)location.X, (int)location.Y, portalWidth, portalHeight),
                TeleportPosition = location
            };
            switch (projectileDirection)
            {
                case CollisionDirection.Left:
                    orangePortal.LinkDirection = LinkDirection.Left;
                    break;
                case CollisionDirection.Top:
                    orangePortal.LinkDirection = LinkDirection.Up;
                    break;
                case CollisionDirection.Right:
                    orangePortal.LinkDirection = LinkDirection.Right;
                    break;
                case CollisionDirection.Bottom:
                    orangePortal.LinkDirection = LinkDirection.Down;
                    break;
            }

            NeedsRoomAssignment = 1;
            DelegateManager.RaiseObjectCreated(orangePortal);
        }

        public void Update(GameTime gameTime, int currentRoom, List<ICollision> loadedObjects)
        {
            this.loadedObjects = loadedObjects;

            if (NeedsRoomAssignment == 0) //blue needs
            {
                bluePortal.PortalRoom = currentRoom;
            } else if (NeedsRoomAssignment == 1) //orange
            {
                orangePortal.PortalRoom = currentRoom;
                loadedObjects.Add(orangePortal);
            }
            NeedsRoomAssignment = -1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bluePortal?.Draw(spriteBatch);
            orangePortal?.Draw(spriteBatch);
        }
    }
}
