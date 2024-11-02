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
            Rectangle r = new(); //instead of declaring new rectangle for everything that needs one

            List<ICollision> enemies = new() {
                new BatKeese(), new BlueCentaur(), new BlueGorya(), new BlueKnight(), new BlueOcto(null), 
                new DarkMoblin(), new DragonBoss(null, null), 
                new GelBigGray(), new GelBigGreen(), new GelSmallBlack(), new GelSmallTeal(), 
                new RedCentaur(), new RedGorya(), new RedKnight(), new RedMoblin(), new RedOcto(null), 
                new Skeleton(), new WallMaster()
            };

            List<ICollision> unmovableBlocks = new() {
                new BlockBlueFloor(), new BlockBlueGap(), new BlockBombedWall(), new BlockDiamond(), 
                new BlockKeyHole(), new BlockLadder(), new BlockOpenDoor(), new BlockStairs(), 
                new BlockStatue1(), new BlockStatue2(), new BlockWall(), new BlockWhiteBrick()
            };
            List<ICollision> otherBlocks = new()
            {
                new BlockPush() //will add stairs and other odd cases
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


            //link vs unmovableblocks
            AddDirectionalEvents(list, new List<ICollision>() { link }, unmovableBlocks, linkMovementEvents);
            //enemies vs all blocks
            AddDirectionalEvents(list, enemies, allCollidableBlocks, enemyMovementEvents);
            //AddUniqueBlockEvents(list, link, enemies, uniqueBlocksAndEvents);
            //uniqueBlocksAndEvents can be a dictionary?

            //projectiles against blocks
            allCollidableBlocks.Remove(new BlockBlueGap()); //projectiles go over the water
            allProjectiles.Remove(new BombSprite(null, r, 0)); //bomb is its own case
            AddNonDirectionalEvents(list, allProjectiles, allCollidableBlocks, new ProjectileVanish());
            allCollidableBlocks.Add(new BlockBlueGap());
            allProjectiles.Add(new BombSprite(null, r, 0));

            //link picking up items
            AddNonDirectionalEvents(list, new List<ICollision>() { link }, pickupableItems, new PickUpItem());

            //link running into enemies
            AddDirectionalEvents(list, new List<ICollision>() { link }, enemies, linkEncountersEnemyEvents);
            //link hurt by enemy projectiles
            AddNonDirectionalEvents(list, new List<ICollision>() { link }, enemyProjectiles, new HurtLink());
            //link projectiles hurting enemies
            AddNonDirectionalEvents(list, linkProjectiles, enemies, new HurtEnemy());

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
