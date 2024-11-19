using Legend_of_the_Power_Rangers.LevelCreation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            PortalDelegator.OnPortalEntered += HandlePortalEntered;
        }

        private void HandleBluePortalCreation(Rectangle rectangle, CollisionDirection projectileDirection)
        {
            bool notCollidingWithOrange = orangePortal != null && !rectangle.Intersects(orangePortal.CollisionHitbox);
            if (bluePortal == null)
            {
                AddBluePortal(rectangle, projectileDirection);
                DelegateManager.RaiseObjectCreated(bluePortal);
            } else if (notCollidingWithOrange || orangePortal == null) //don't want portals to overlap
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
            bool notCollidingWithBlue = bluePortal != null && !rectangle.Intersects(bluePortal.CollisionHitbox);
            if (orangePortal == null)
            {
                AddOrangePortal(rectangle, projectileDirection);
                DelegateManager.RaiseObjectCreated(orangePortal);
            }
            else if (notCollidingWithBlue || bluePortal == null)
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

        private void HandlePortalEntered(IPortal portal)
        {
            Link link = LinkManager.GetLink();
            LinkStateMachine linkStateMachine = link.GetStateMachine();

            if (portal is BluePortal && orangePortal != null)
            {
                link.destinationRectangle.X = (int)orangePortal.TeleportPosition.X;
                link.destinationRectangle.Y = (int)orangePortal.TeleportPosition.Y;
                linkStateMachine.ChangeDirection(orangePortal.LinkDirection);
                DelegateManager.RaiseChangeToSpecificRoom(orangePortal.PortalRoom);
            } else if ((portal is OrangePortal && bluePortal != null))
            {
                link.destinationRectangle.X = (int)bluePortal.TeleportPosition.X;
                link.destinationRectangle.Y = (int)bluePortal.TeleportPosition.Y;
                linkStateMachine.ChangeDirection(bluePortal.LinkDirection);
                DelegateManager.RaiseChangeToSpecificRoom(bluePortal.PortalRoom);
            }
        }

        public void Update(List<ICollision> loadedObjects, int currentRoom)
        {
            if (bluePortal != null && !loadedObjects.Contains(bluePortal))
            {
                loadedObjects.Add(bluePortal);
            }
            if (orangePortal != null && !loadedObjects.Contains(orangePortal))
            {
                loadedObjects.Add(orangePortal);
            }

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
