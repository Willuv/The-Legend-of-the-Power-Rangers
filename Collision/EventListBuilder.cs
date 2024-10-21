using Legend_of_the_Power_Rangers.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    internal class EventListBuilder
    {
        public static Dictionary<string, IEvent> BuildList()
        {
            Dictionary<string, IEvent> list = new();

            //as ugly as this is, the alternative is creating a new instance for every list.add call
            Link link = new Link();
            BlockBlueFloor blockBlueFloor = new();
            BlockBlueGap blockBlueGap = new();
            BlockBlueSand blockBlueSand = new();
            BlockBombedWall blockBombedWall = new();
            BlockDiamond blockDiamond = new();
            BlockFire blockFire = new();
            BlockKeyHole blockKeyHole = new();
            BlockLadder blockLadder = new();
            BlockOpenDoor blockOpenDoor = new();
            BlockPush blockPush = new();
            BlockSquare blockSquare = new();
            BlockStairs blockStairs = new();
            BlockStatue1 blockStatue1 = new();
            BlockStatue2 blockStatue2 = new();
            ItemBlueCandle itemBlueCandle = new();
            ItemBluePotion itemBluePotion = new();
            ItemBomb itemBomb = new();
            ItemBow itemBow = new();
            ItemClock itemClock = new();
            ItemCompass itemCompass = new();
            ItemFairy itemFairy = new();
            ItemHeart itemHeart = new();
            ItemHeartContainer itemHeartContainer = new();
            ItemKey itemKey = new();
            ItemMap itemMap = new();
            ItemRupee itemRupee = new();
            ItemTriforce itemTriforce = new();
            ItemWoodBoomerang itemWoodBoomerang = new();
            BatKeese batKeese = new();
            BlueCentaur blueCentaur = new();
            BlueGorya blueGorya = new();
            BlueKnight blueKnight = new();
            BlueOcto blueOcto = new();
            DarkMoblin darkMoblin = new();
            DragonBoss dragonBoss = new(null, null);
            DragonProjectile dragonProjectile = new(null, new Microsoft.Xna.Framework.Rectangle());
            GelBigGray gelBigGray = new();
            GelBigGreen gelBigGreen = new();
            GelSmallBlack gelSmallBlack = new();
            GelSmallTeal gelSmallTeal = new();
            RedCentaur redCentaur = new();
            RedGorya redGorya = new();
            RedKnight redKnight = new();
            RedMoblin redMoblin = new();
            RedOcto redOcto = new();
            Skeleton skeleton = new();
            WallMaster wallMaster = new();
            MoveLinkDown moveLinkDown = new();
            MoveLinkLeft moveLinkLeft = new();
            MoveLinkRight moveLinkRight = new();
            MoveLinkUp moveLinkUp = new();
            MoveLinkLeftAndGetHurt moveLinkLeftAndGetHurt = new();
            PickUpItem pickUpItem = new();
            PickUpItemAndHeal pickUpItemAndHeal = new();
            PickUpItemAndIncreaseMaxHealth pickUpItemAndIncreaseMaxHealth = new();
            HurtLink hurtLink = new();

            //link vs blocks
            list.Add(KeyGenerator.Generate(link, blockBlueFloor, CollisionDirection.Left), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockBlueFloor, CollisionDirection.Top), moveLinkUp);
            list.Add(KeyGenerator.Generate(link, blockBlueFloor, CollisionDirection.Right), moveLinkRight);
            list.Add(KeyGenerator.Generate(link, blockBlueFloor, CollisionDirection.Bottom), moveLinkDown);
            //eventList.Add(KeyGenerator.Generate(new Link(), new BlockPush(), CollisionDirection.Left), new SlowLinkAndPushBlockRight());

            //link vs items
            list.Add(KeyGenerator.Generate(link, itemBomb, CollisionDirection.Left), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemBomb, CollisionDirection.Top), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemBomb, CollisionDirection.Right), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemBomb, CollisionDirection.Bottom), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemBow, CollisionDirection.Left), pickUpItem); //we'll see if this causes issues firing
            list.Add(KeyGenerator.Generate(link, itemBow, CollisionDirection.Top), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemBow, CollisionDirection.Right), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemBow, CollisionDirection.Bottom), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemClock, CollisionDirection.Left), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemClock, CollisionDirection.Top), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemClock, CollisionDirection.Right), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemClock, CollisionDirection.Bottom), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemCompass, CollisionDirection.Left), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemCompass, CollisionDirection.Top), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemCompass, CollisionDirection.Right), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemCompass, CollisionDirection.Bottom), pickUpItem);
            //fairy maybe added as pickupable but hard to say right now
            list.Add(KeyGenerator.Generate(link, itemHeart, CollisionDirection.Left), pickUpItemAndHeal);
            list.Add(KeyGenerator.Generate(link, itemHeart, CollisionDirection.Top), pickUpItemAndHeal);
            list.Add(KeyGenerator.Generate(link, itemHeart, CollisionDirection.Right), pickUpItemAndHeal);
            list.Add(KeyGenerator.Generate(link, itemHeart, CollisionDirection.Bottom), pickUpItemAndHeal);
            list.Add(KeyGenerator.Generate(link, itemHeartContainer, CollisionDirection.Left), pickUpItemAndIncreaseMaxHealth);
            list.Add(KeyGenerator.Generate(link, itemHeartContainer, CollisionDirection.Top), pickUpItemAndIncreaseMaxHealth);
            list.Add(KeyGenerator.Generate(link, itemHeartContainer, CollisionDirection.Right), pickUpItemAndIncreaseMaxHealth);
            list.Add(KeyGenerator.Generate(link, itemHeartContainer, CollisionDirection.Bottom), pickUpItemAndIncreaseMaxHealth);
            list.Add(KeyGenerator.Generate(link, itemKey, CollisionDirection.Left), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemKey, CollisionDirection.Top), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemKey, CollisionDirection.Right), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemKey, CollisionDirection.Bottom), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemMap, CollisionDirection.Bottom), pickUpItem);

            //link vs enemies
            list.Add(KeyGenerator.Generate(link, dragonBoss, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redOcto, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            //will need more obviously

            return list;
        }
    }
}
