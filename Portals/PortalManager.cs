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
        private OrangePortal orangePortal;
        
        public PortalManager()
        {
            NeedsRoomAssignment = -1;
            PortalDelegator.OnBluePortalCreated += HandleBluePortalCreation;
            PortalDelegator.OnOrangePortalCreated += HandleOrangePortalCreation;
        }

        private void HandleBluePortalCreation(Rectangle rectangle, CollisionDirection projectileDirection)
        {
            if (bluePortal == null)
            {
                AddBluePortal(rectangle, projectileDirection);
                DelegateManager.RaiseObjectCreated(bluePortal);
            } else if (orangePortal != null && !rectangle.Intersects(orangePortal.CollisionHitbox)) //don't want portals to overlap
            {
                DelegateManager.RaiseObjectRemoved(bluePortal);
                AddBluePortal(rectangle, projectileDirection);
                DelegateManager.RaiseObjectCreated(bluePortal);
            }
        }

        private void AddBluePortal(Rectangle rectangle, CollisionDirection projectileDirection)
        {
            bluePortal = new BluePortal()
            {
                CollisionHitbox = rectangle,
                TeleportPosition = new Vector2(rectangle.X, rectangle.Y)
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
        }

        private void HandleOrangePortalCreation(Rectangle rectangle, CollisionDirection projectileDirection)
        {
            if (orangePortal == null)
            {
                AddOrangePortal(rectangle, projectileDirection);
                DelegateManager.RaiseObjectCreated(orangePortal);
            }
            else if (bluePortal != null && !rectangle.Intersects(bluePortal.CollisionHitbox))
            {
                DelegateManager.RaiseObjectRemoved(orangePortal);
                AddOrangePortal(rectangle, projectileDirection);
                DelegateManager.RaiseObjectCreated(orangePortal);
            }
        }

        private void AddOrangePortal(Rectangle rectangle, CollisionDirection projectileDirection)
        {
            orangePortal = new OrangePortal()
            {
                CollisionHitbox = rectangle,
                TeleportPosition = new Vector2(rectangle.X, rectangle.Y)
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
        }

        public void Update(GameTime gameTime, int currentRoom)
        {
            if (NeedsRoomAssignment == 0) //blue needs
            {
                bluePortal.PortalRoom = currentRoom;
            } else if (NeedsRoomAssignment == 1) //orange
            {
                orangePortal.PortalRoom = currentRoom;
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
