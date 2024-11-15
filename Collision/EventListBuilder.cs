using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Legend_of_the_Power_Rangers.Portals;
using Microsoft.Xna.Framework;

namespace Legend_of_the_Power_Rangers
{
    internal class EventListBuilder
    {
        public static Dictionary<(int, int, CollisionDirection), IEvent> BuildList()
        {
            Dictionary<(int, int, CollisionDirection), IEvent> list = new();

            List<ICollision> link = new() { LinkManager.GetLink() };
            List<ICollision> wall = new() { new Wall(0, 0, 0, false) };
            Rectangle r = new(); //instead of declaring new rectangle for everything that needs one

            List<ICollision> enemies = new() {
                new BatKeese(), new BlueCentaur(), new BlueGorya(), new BlueKnight(), new BlueOcto(null),
                new DarkMoblin(), new DragonBoss(null, null), new GelBigGray(), new GelBigGreen(),
                new GelSmallBlack(), new GelSmallTeal(), new RedCentaur(), new RedGorya(), new RedKnight(),
                new RedMoblin(), new RedOcto(null), new Skeleton(), new WallMaster()
            };

            List<ICollision> unmovableBlocks = new() {
                new BlockBlueFloor(), new BlockBlueGap(), new BlockBombedWall(), new BlockDiamond(),
                new BlockKeyHole(), new BlockOpenDoor(), new BlockStatue1(), new BlockStatue2(), 
                new BlockWall(), new BlockWhiteBrick()
            };
            List<ICollision> otherBlocks = new()
            {
                new BlockPush(), new BlockStairs()
            };
            List<ICollision> allCollidableBlocks = new();
            allCollidableBlocks.AddRange(unmovableBlocks);
            allCollidableBlocks.AddRange(otherBlocks);

            List<IEvent> linkMovementEvents = new() {
                new MoveLinkLeft(), new MoveLinkUp(), new MoveLinkRight(), new MoveLinkDown(),
            };
            List<IEvent> enemyMovementEvents = new() {
                new MoveEnemyLeft(), new MoveEnemyUp(), new MoveEnemyRight(), new MoveEnemyDown()
            };
            List<IEvent> movementEvents = new();
            movementEvents.AddRange(linkMovementEvents);
            movementEvents.AddRange(enemyMovementEvents);

            List<ICollision> pickupableItems = new()
            {
                new ItemBomb(), new ItemBow(), new ItemClock(), new ItemCompass(), new ItemFairy(),
                new ItemHeart(), new ItemHeartContainer(), new ItemKey(), new ItemMap(), new ItemRupee(),
                new ItemTriforce(), new ItemWoodBoomerang()
            };
            List<ICollision> linkProjectiles = new()
            {
                new ArrowSprite(null, r, 0), new BombSprite(null, r, 0), new BoomerangSprite(null, r, 0),
                new CandleSprite(null, r, 0), new SwordSprite(null, r, 0)
            };
            List<ICollision> enemyProjectiles = new()
            {
                new DragonProjectile(null, r) //add octo
            };
            List<ICollision> allProjectiles = new();
            allProjectiles.AddRange(linkProjectiles);
            allProjectiles.AddRange(enemyProjectiles);
            List<IEvent> linkEncountersEnemyEvents = new()
            {
                new LinkVSEnemyLeft(), new LinkVSEnemyTop(),
                new LinkVSEnemyRight(), new LinkVSEnemyBottom()
            };
            List<ICollision> doors = new()
            {
                new diamondDoor(null, 0, 0 , 0), new holeDoor(null, 0, 0, 0), new keyDoor(null, 0, 0, 0),
                new openDoor(null, 0, 0, 0), new wallDoor(null, 0, 0, 0)
            };

            //custom events
            List<IEvent> pushableBlockEvents = new()
            {
                new LinkVSPushBlockLeft(), new LinkVSPushBlockTop(),
                new LinkVSPushBlockRight(), new LinkVSPushBlockBottom()
            };

            //portals
            List<ICollision> portalProjectiles = new()
            {
                new BluePortalProjectileSprite(null, r, 0)
            };
            List<ICollision> portals = new()
            {
                new BluePortal()
            };


            //link vs unmovableblocks
            AddDirectionalEvents(list, link, unmovableBlocks, linkMovementEvents);
            //link vs invisible blocks (bc enemies can ignore them in secret room)
            AddDirectionalEvents(list, link, new List<ICollision>() { new InvisibleBlock() }, linkMovementEvents);
            //enemies vs all blocks
            AddDirectionalEvents(list, enemies, allCollidableBlocks, enemyMovementEvents);
            //link vs pushable blocks
            AddDirectionalEvents(list, link, new List<ICollision>() { new BlockPush() }, pushableBlockEvents);
            //link vs stairs
            AddDirectionalEvents(list, link, new List<ICollision>() { new BlockStairs() }, new List<IEvent>()
            {
                new StairsEvent(), new MoveLinkUp(), new MoveLinkRight(), new MoveLinkDown()
            });
            //link vs teleporter
            AddNonDirectionalEvents(list, link, new List<ICollision>() { new InvisibleTeleportBlock() }, new LinkVSTeleporter());
            //link vs fire
            AddNonDirectionalEvents(list, link, new List<ICollision>() { new BlockFire() }, new HurtLink());

            //projectiles against blocks
            allCollidableBlocks.Remove(new BlockBlueGap()); //projectiles go over the water
            allProjectiles.Remove(new BombSprite(null, r, 0)); //bomb is its own case
            AddNonDirectionalEvents(list, allProjectiles, allCollidableBlocks, new ProjectileVanish());
            allCollidableBlocks.Add(new BlockBlueGap());
            allProjectiles.Add(new BombSprite(null, r, 0));

            //link picking up items
            AddNonDirectionalEvents(list, link, pickupableItems, new PickUpItem());

            //link running into enemies
            AddDirectionalEvents(list, link, enemies, linkEncountersEnemyEvents);
            //link hurt by enemy projectiles
            AddNonDirectionalEvents(list, link, enemyProjectiles, new HurtLink());
            //link projectiles hurting enemies
            AddNonDirectionalEvents(list, linkProjectiles, enemies, new HurtEnemy());

            //link vs doors
            AddNonDirectionalEvents(list, link, doors, new LinkVSAnyDoor());
            //enemies vs doors
            AddDirectionalEvents(list, enemies, doors, enemyMovementEvents);
            //projectiles vs doors

            //link vs walls
            AddDirectionalEvents(list, link, wall, linkMovementEvents);
            //enemies vs walls
            AddDirectionalEvents(list, enemies, wall, enemyMovementEvents);
            //projectiles vs walls
            //AddNonDirectionalEvents(list, allProjectiles, new List<ICollision>() { new Wall(0, 0, 0) }, new ProjectileVanish());

            //special events
            //bomb vs bombable door
            AddNonDirectionalEvents(list, new List<ICollision>() { new BombSprite(null, r, 0) },
                new List<ICollision>() { new holeDoor(null, 0, 0, 0) }, new BombVSBombableDoor());

            //portal projectiles vs blocks
            allCollidableBlocks.RemoveAt(1); //remove blue gap
            AddNonDirectionalEvents(list, portalProjectiles, allCollidableBlocks, new SpawnPortal());
            allCollidableBlocks.Add(new BlockBlueGap());

            //portal projectiles vs walls
            AddNonDirectionalEvents(list, portalProjectiles, wall, new SpawnPortal());

            //link walking into portals


            return list;
        }

        private static void AddDirectionalEvents(Dictionary<(int, int, CollisionDirection), IEvent> eventList, List<ICollision> collidables1, List<ICollision> collidables2, List<IEvent> events) {
            foreach (ICollision obj1 in collidables1)
            {
                foreach (ICollision obj2 in collidables2)
                {
                    eventList.Add(KeyGenerator.Generate(obj1, obj2, CollisionDirection.Left), events[0]);
                    eventList.Add(KeyGenerator.Generate(obj1, obj2, CollisionDirection.Top), events[1]);
                    eventList.Add(KeyGenerator.Generate(obj1, obj2, CollisionDirection.Right), events[2]);
                    eventList.Add(KeyGenerator.Generate(obj1, obj2, CollisionDirection.Bottom), events[3]);
                }
            }
        }

        private static void AddNonDirectionalEvents(Dictionary<(int, int, CollisionDirection), IEvent> eventList, List<ICollision> collidables1, List<ICollision> collidables2, IEvent oneEvent)
        {
            foreach (ICollision obj1 in collidables1)
            {
                foreach (ICollision obj2 in collidables2)
                {
                    eventList.Add(KeyGenerator.Generate(obj1, obj2, CollisionDirection.Left), oneEvent);
                    eventList.Add(KeyGenerator.Generate(obj1, obj2, CollisionDirection.Top), oneEvent);
                    eventList.Add(KeyGenerator.Generate(obj1, obj2, CollisionDirection.Right), oneEvent);
                    eventList.Add(KeyGenerator.Generate(obj1, obj2, CollisionDirection.Bottom), oneEvent);
                }
            }
        }

        private static void AddUniqueEvents(Dictionary<(int, int, CollisionDirection), IEvent> eventList, List<ICollision> collidables1, List<ICollision> collidables2, List<IEvent> events)
        {
            //figure out soon
        }
    }
}
