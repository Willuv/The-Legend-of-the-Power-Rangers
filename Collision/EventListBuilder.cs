using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    internal class EventListBuilder
    {
        public static Dictionary<(int, int, CollisionDirection), IEvent> BuildList()
        {
            Dictionary<(int, int, CollisionDirection), IEvent> list = new();

            Link link = LinkManager.GetLink();

            List<ICollision> enemies = new() {
                new BatKeese(), new BlueCentaur(), new BlueGorya(), new BlueKnight(), new BlueOcto(null), 
                new DarkMoblin(), new DragonBoss(null, null), new DragonProjectile(null, new Rectangle()),
                new GelBigGray(), new GelBigGreen(), new GelSmallBlack(), new GelSmallTeal(), 
                new RedCentaur(), new RedGorya(), new RedKnight(), new RedMoblin(), new RedOcto(null), 
                new Skeleton(), new WallMaster()
            };
            List<ICollision> unmoveableBlocks = new() {
                new BlockBlueFloor(), new BlockBlueGap(), new BlockBombedWall(), new BlockDiamond(), 
                new BlockKeyHole(), new BlockLadder(), new BlockOpenDoor(), new BlockStairs(), 
                new BlockStatue1(), new BlockStatue2(), new BlockWall(), new BlockWhiteBrick()
            };
            List<IEvent> movementEvents = new() {
                new MoveLinkLeft(), new MoveLinkUp(), new MoveLinkRight(), new MoveLinkDown(),
                new MoveEnemyLeft(), new MoveEnemyUp(), new MoveEnemyRight(), new MoveEnemyDown()
            };
            List<ICollision> pickupableItems = new()
            {
                new ItemBomb(), new ItemBow(), new ItemClock(), new ItemCompass(), new ItemFairy(),
                new ItemHeart(), new ItemHeartContainer(), new ItemKey(), new ItemMap(), new ItemRupee(),
                new ItemTriforce(), new ItemWoodBoomerang()
            };
            List<IEvent> enemyDamageLinkEvents = new()
            {
                new MoveLinkLeftAndGetHurt(), new MoveLinkUpAndGetHurt(),
                new MoveLinkRightAndGetHurt(), new MoveLinkDownAndGetHurt()
            };
            
            
            //link and enemies vs blocks
            AddEntitiesAgainstUnmovableBlocksEvents(list, link, enemies, unmoveableBlocks, movementEvents);
            //AddUniqueBlockEvents(list, link, enemies, uniqueBlocksAndEvents);
            //uniqueBlocksAndEvents can be a dictionary?

            //link vs items
            AddLinkPickupItemEvents(list, link, pickupableItems, new PickUpItem());

            //link vs enemies
            AddEnemyDamagingLinkEvents(list, link, enemies, enemyDamageLinkEvents);
            //AddLinkDamagingEnemyEvents(list, link, linkItems, enemies, linkDamageEnemyEvents);

            //linkItems vs enemies

            return list;
        }

        private static void AddEntitiesAgainstUnmovableBlocksEvents(Dictionary<(int, int, CollisionDirection), IEvent> eventList, Link link, List<ICollision> enemies, List<ICollision> blocks, List<IEvent> events)
        {
            foreach (ICollision block in blocks)
            {
                eventList.Add(KeyGenerator.Generate(link, block, CollisionDirection.Left), events[0]);
                eventList.Add(KeyGenerator.Generate(link, block, CollisionDirection.Top), events[1]);
                eventList.Add(KeyGenerator.Generate(link, block, CollisionDirection.Right), events[2]);
                eventList.Add(KeyGenerator.Generate(link, block, CollisionDirection.Bottom), events[3]);

                foreach (ICollision enemy in enemies)
                {
                    eventList.Add(KeyGenerator.Generate(enemy, block, CollisionDirection.Left), events[4]);
                    eventList.Add(KeyGenerator.Generate(enemy, block, CollisionDirection.Top), events[5]);
                    eventList.Add(KeyGenerator.Generate(enemy, block, CollisionDirection.Right), events[6]);
                    eventList.Add(KeyGenerator.Generate(enemy, block, CollisionDirection.Bottom), events[7]);
                }
            } 
        }

        private static void AddLinkPickupItemEvents(Dictionary<(int, int, CollisionDirection), IEvent> eventList, Link link, List<ICollision> items, IEvent pickUpItem)
        {
            foreach (ICollision item in items)
            {
                eventList.Add(KeyGenerator.Generate(link, item, CollisionDirection.Left), pickUpItem);
                eventList.Add(KeyGenerator.Generate(link, item, CollisionDirection.Top), pickUpItem);
                eventList.Add(KeyGenerator.Generate(link, item, CollisionDirection.Bottom), pickUpItem);
                eventList.Add(KeyGenerator.Generate(link, item, CollisionDirection.Right), pickUpItem);
            }
        }

        private static void AddEnemyDamagingLinkEvents(Dictionary<(int, int, CollisionDirection), IEvent> eventList, Link link, List<ICollision> enemies, List<IEvent> damageLink)
        {
            foreach (ICollision enemy in enemies) {
                eventList.Add(KeyGenerator.Generate(link, enemy, CollisionDirection.Left), damageLink[0]);
                eventList.Add(KeyGenerator.Generate(link, enemy, CollisionDirection.Top), damageLink[1]);
                eventList.Add(KeyGenerator.Generate(link, enemy, CollisionDirection.Right), damageLink[2]);
                eventList.Add(KeyGenerator.Generate(link, enemy, CollisionDirection.Bottom), damageLink[3]);
            }
        }
    }
}
