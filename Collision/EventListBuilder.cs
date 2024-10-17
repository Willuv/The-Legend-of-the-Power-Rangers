using Legend_of_the_Power_Rangers.Collision.CollisionBasedEvents;
using Legend_of_the_Power_Rangers.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers.Collision
{
    internal class EventListBuilder
    {
        public static Dictionary<string, IEvent> BuildList()
        {
            Dictionary<string, IEvent> list = new();

            //as ugly as this is, the alternative is creating a new instance for every event addition
            Link link = new Link();
            BlockBlueFloor blockBlueFloor = new();
            BlockBlueGap blockBlueGap = new BlockBlueGap();
            BlockBlueSand blockBlueSand = new BlockBlueSand();
            BlockBombedWall blockBombedWall = new BlockBombedWall();
            BlockDiamond blockDiamond = new BlockDiamond();
            BlockFire blockFire = new BlockFire();
            BlockKeyHole blockKeyHole = new BlockKeyHole();
            BlockLadder blockLadder = new BlockLadder();
            BlockOpenDoor blockOpenDoor = new BlockOpenDoor();
            BlockPush blockPush = new BlockPush();
            BlockSquare blockSquare = new BlockSquare();
            BlockStairs blockStairs = new BlockStairs();
            BlockStatue1 blockStatue1 = new BlockStatue1();
            BlockStatue2 blockStatue2 = new BlockStatue2();
            ItemBlueCandle itemBlueCandle = new ItemBlueCandle();
            ItemBluePotion itemBluePotion = new ItemBluePotion();
            ItemBomb itemBomb = new ItemBomb();
            ItemBow itemBow = new ItemBow();
            ItemClock itemClock = new ItemClock();
            ItemCompass itemCompass = new ItemCompass();
            ItemFairy itemFairy = new ItemFairy();
            ItemHeart itemHeart = new ItemHeart();
            ItemHeartContainer itemHeartContainer = new ItemHeartContainer();
            ItemKey itemKey = new ItemKey();
            ItemMap itemMap = new ItemMap();
            ItemRupee itemRupee = new ItemRupee();
            ItemTriforce itemTriforce = new ItemTriforce();
            ItemWoodBoomerang itemWoodBoomerang = new ItemWoodBoomerang();
            BatKeese batKeese = new BatKeese();
            BlueCentaur blueCentaur = new BlueCentaur();
            BlueGorya blueGorya = new BlueGorya();
            BlueKnight blueKnight = new BlueKnight();
            BlueOcto blueOcto = new BlueOcto();
            DarkMoblin darkMoblin = new DarkMoblin();
            DragonBoss dragonBoss = new(null, null);
            DragonProjectile dragonProjectile = new(null, new Microsoft.Xna.Framework.Rectangle());
            GelBigGray gelBigGray = new GelBigGray();
            GelBigGreen gelBigGreen = new GelBigGreen();
            GelSmallBlack gelSmallBlack = new GelSmallBlack();
            GelSmallTeal gelSmallTeal = new GelSmallTeal();
            RedCentaur redCentaur = new RedCentaur();
            RedGorya redGorya = new RedGorya();
            RedKnight redKnight = new RedKnight();
            RedMoblin redMoblin = new RedMoblin();
            RedOcto redOcto = new RedOcto();
            Skeleton skeleton = new Skeleton();
            WallMaster wallMaster = new WallMaster();
            MoveLinkDown moveLinkDown = new MoveLinkDown();
            MoveLinkLeft moveLinkLeft = new MoveLinkLeft();
            MoveLinkRight moveLinkRight = new MoveLinkRight();
            MoveLinkUp moveLinkUp = new MoveLinkUp();
            MoveLinkLeftAndGetHurt moveLinkLeftAndGetHurt = new MoveLinkLeftAndGetHurt();

            //link vs blocks
            list.Add(KeyGenerator.Generate(link, blockBlueFloor, CollisionDirection.Left), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockBlueFloor, CollisionDirection.Top), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockBlueFloor, CollisionDirection.Right), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockBlueFloor, CollisionDirection.Bottom), moveLinkLeft);
            //eventList.Add(KeyGenerator.Generate(new Link(), new BlockPush(), CollisionDirection.Left), new SlowLinkAndPushBlockRight());

            //link vs items


            //link vs enemies
            list.Add(KeyGenerator.Generate(link, dragonBoss, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redOcto, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            //will need more obviously

            return list;
        }
    }
}
