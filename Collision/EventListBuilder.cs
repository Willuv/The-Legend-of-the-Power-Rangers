using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Legend_of_the_Power_Rangers
{
    internal class EventListBuilder
    {
        public static Dictionary<(int, int, CollisionDirection), IEvent> BuildList()
        {
            Dictionary<(int, int, CollisionDirection), IEvent> list = new();

            //as ugly as this is, the alternative is creating a new instance for every list.add call
            Link link = new();
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
            BlockWall blockWall = new();
            BlockWhiteBrick blockWhiteBrick = new();
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
            BlueOcto blueOcto = new(null);
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
            RedOcto redOcto = new(null);
            Skeleton skeleton = new();
            WallMaster wallMaster = new();
            MoveLinkDown moveLinkDown = new();
            MoveLinkLeft moveLinkLeft = new();
            MoveLinkRight moveLinkRight = new();
            MoveLinkUp moveLinkUp = new();
            MoveLinkLeftAndGetHurt moveLinkLeftAndGetHurt = new();
            MoveLinkUpAndGetHurt moveLinkUpAndGetHurt = new();
            MoveLinkRightAndGetHurt moveLinkRightAndGetHurt = new();
            MoveLinkDownAndGetHurt moveLinkDownAndGetHurt = new();
            MoveLinkLeftAndPushBlockRight moveLinkLeftAndPushBlockRight = new();
            PickUpItem pickUpItem = new();
            PickUpItemAndHeal pickUpItemAndHeal = new();
            PickUpItemAndIncreaseMaxHealth pickUpItemAndIncreaseMaxHealth = new();
            HurtLink hurtLink = new();
            PickUpMap pickUpMap = new();
            PickUpKey pickUpKey = new();
            PickUpTriforce pickUpTriforce = new();

            //link vs blocks
            list.Add(KeyGenerator.Generate(link, blockBlueFloor, CollisionDirection.Left), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockBlueFloor, CollisionDirection.Top), moveLinkUp);
            list.Add(KeyGenerator.Generate(link, blockBlueFloor, CollisionDirection.Right), moveLinkRight);
            list.Add(KeyGenerator.Generate(link, blockBlueFloor, CollisionDirection.Bottom), moveLinkDown);
            list.Add(KeyGenerator.Generate(link, blockBlueSand, CollisionDirection.Left), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockBlueSand, CollisionDirection.Top), moveLinkUp);
            list.Add(KeyGenerator.Generate(link, blockBlueSand, CollisionDirection.Right), moveLinkRight);
            list.Add(KeyGenerator.Generate(link, blockBlueSand, CollisionDirection.Bottom), moveLinkDown);
            list.Add(KeyGenerator.Generate(link, blockBombedWall, CollisionDirection.Left), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockBombedWall, CollisionDirection.Top), moveLinkUp);
            list.Add(KeyGenerator.Generate(link, blockBombedWall, CollisionDirection.Right), moveLinkRight);
            list.Add(KeyGenerator.Generate(link, blockBombedWall, CollisionDirection.Bottom), moveLinkDown);
            list.Add(KeyGenerator.Generate(link, blockDiamond, CollisionDirection.Left), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockDiamond, CollisionDirection.Top), moveLinkUp);
            list.Add(KeyGenerator.Generate(link, blockDiamond, CollisionDirection.Right), moveLinkRight);
            list.Add(KeyGenerator.Generate(link, blockDiamond, CollisionDirection.Bottom), moveLinkDown);
            list.Add(KeyGenerator.Generate(link, blockFire, CollisionDirection.Left), hurtLink);
            list.Add(KeyGenerator.Generate(link, blockFire, CollisionDirection.Top), hurtLink);
            list.Add(KeyGenerator.Generate(link, blockFire, CollisionDirection.Right), hurtLink);
            list.Add(KeyGenerator.Generate(link, blockFire, CollisionDirection.Bottom), hurtLink);
            list.Add(KeyGenerator.Generate(link, blockKeyHole, CollisionDirection.Left), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockKeyHole, CollisionDirection.Top), moveLinkUp);
            list.Add(KeyGenerator.Generate(link, blockKeyHole, CollisionDirection.Right), moveLinkRight);
            list.Add(KeyGenerator.Generate(link, blockKeyHole, CollisionDirection.Bottom), moveLinkDown);
            //can walk through open doors for now
            list.Add(KeyGenerator.Generate(link, blockPush, CollisionDirection.Left), moveLinkLeftAndPushBlockRight);
            //list.Add(KeyGenerator.Generate(link, blockPush, CollisionDirection.Top), moveLinkLeftAndPushBlockRight);
            //list.Add(KeyGenerator.Generate(link, blockPush, CollisionDirection.Right), moveLinkLeftAndPushBlockRight);
            //list.Add(KeyGenerator.Generate(link, blockPush, CollisionDirection.Bottom), moveLinkLeftAndPushBlockRight);
            //push will be added once we know exactly how it will work
            list.Add(KeyGenerator.Generate(link, blockSquare, CollisionDirection.Left), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockSquare, CollisionDirection.Top), moveLinkUp);
            list.Add(KeyGenerator.Generate(link, blockSquare, CollisionDirection.Right), moveLinkRight);
            list.Add(KeyGenerator.Generate(link, blockSquare, CollisionDirection.Bottom), moveLinkDown);
            list.Add(KeyGenerator.Generate(link, blockStairs, CollisionDirection.Left), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockStairs, CollisionDirection.Top), moveLinkUp);
            list.Add(KeyGenerator.Generate(link, blockStairs, CollisionDirection.Right), moveLinkRight);
            list.Add(KeyGenerator.Generate(link, blockStairs, CollisionDirection.Bottom), moveLinkDown);
            list.Add(KeyGenerator.Generate(link, blockStatue1, CollisionDirection.Left), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockStatue1, CollisionDirection.Top), moveLinkUp);
            list.Add(KeyGenerator.Generate(link, blockStatue1, CollisionDirection.Right), moveLinkRight);
            list.Add(KeyGenerator.Generate(link, blockStatue1, CollisionDirection.Bottom), moveLinkDown);
            list.Add(KeyGenerator.Generate(link, blockStatue2, CollisionDirection.Left), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockStatue2, CollisionDirection.Top), moveLinkUp);
            list.Add(KeyGenerator.Generate(link, blockStatue2, CollisionDirection.Right), moveLinkRight);
            list.Add(KeyGenerator.Generate(link, blockStatue2, CollisionDirection.Bottom), moveLinkDown);
            list.Add(KeyGenerator.Generate(link, blockWall, CollisionDirection.Left), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockWall, CollisionDirection.Top), moveLinkUp);
            list.Add(KeyGenerator.Generate(link, blockWall, CollisionDirection.Right), moveLinkRight);
            list.Add(KeyGenerator.Generate(link, blockWall, CollisionDirection.Bottom), moveLinkDown);
            list.Add(KeyGenerator.Generate(link, blockWhiteBrick, CollisionDirection.Left), moveLinkLeft);
            list.Add(KeyGenerator.Generate(link, blockWhiteBrick, CollisionDirection.Top), moveLinkUp);
            list.Add(KeyGenerator.Generate(link, blockWhiteBrick, CollisionDirection.Right), moveLinkRight);
            list.Add(KeyGenerator.Generate(link, blockWhiteBrick, CollisionDirection.Bottom), moveLinkDown);

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
            //fairy maybe added as pickupable but idk what it does yet
            list.Add(KeyGenerator.Generate(link, itemHeart, CollisionDirection.Left), pickUpItemAndHeal);
            list.Add(KeyGenerator.Generate(link, itemHeart, CollisionDirection.Top), pickUpItemAndHeal);
            list.Add(KeyGenerator.Generate(link, itemHeart, CollisionDirection.Right), pickUpItemAndHeal);
            list.Add(KeyGenerator.Generate(link, itemHeart, CollisionDirection.Bottom), pickUpItemAndHeal);
            list.Add(KeyGenerator.Generate(link, itemHeartContainer, CollisionDirection.Left), pickUpItemAndIncreaseMaxHealth);
            list.Add(KeyGenerator.Generate(link, itemHeartContainer, CollisionDirection.Top), pickUpItemAndIncreaseMaxHealth);
            list.Add(KeyGenerator.Generate(link, itemHeartContainer, CollisionDirection.Right), pickUpItemAndIncreaseMaxHealth);
            list.Add(KeyGenerator.Generate(link, itemHeartContainer, CollisionDirection.Bottom), pickUpItemAndIncreaseMaxHealth);
            list.Add(KeyGenerator.Generate(link, itemKey, CollisionDirection.Left), pickUpKey);
            list.Add(KeyGenerator.Generate(link, itemKey, CollisionDirection.Top), pickUpKey);
            list.Add(KeyGenerator.Generate(link, itemKey, CollisionDirection.Right), pickUpKey);
            list.Add(KeyGenerator.Generate(link, itemKey, CollisionDirection.Bottom), pickUpKey);
            list.Add(KeyGenerator.Generate(link, itemMap, CollisionDirection.Left), pickUpMap);
            list.Add(KeyGenerator.Generate(link, itemMap, CollisionDirection.Top), pickUpMap);
            list.Add(KeyGenerator.Generate(link, itemMap, CollisionDirection.Right), pickUpMap);
            list.Add(KeyGenerator.Generate(link, itemMap, CollisionDirection.Bottom), pickUpMap);
            list.Add(KeyGenerator.Generate(link, itemRupee, CollisionDirection.Left), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemRupee, CollisionDirection.Top), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemRupee, CollisionDirection.Right), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemRupee, CollisionDirection.Bottom), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemTriforce, CollisionDirection.Left), pickUpTriforce);
            list.Add(KeyGenerator.Generate(link, itemTriforce, CollisionDirection.Top), pickUpTriforce);
            list.Add(KeyGenerator.Generate(link, itemTriforce, CollisionDirection.Right), pickUpTriforce);
            list.Add(KeyGenerator.Generate(link, itemTriforce, CollisionDirection.Bottom), pickUpTriforce);
            list.Add(KeyGenerator.Generate(link, itemWoodBoomerang, CollisionDirection.Left), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemWoodBoomerang, CollisionDirection.Top), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemWoodBoomerang, CollisionDirection.Right), pickUpItem);
            list.Add(KeyGenerator.Generate(link, itemWoodBoomerang, CollisionDirection.Bottom), pickUpItem);

            //link vs enemies
            list.Add(KeyGenerator.Generate(link, batKeese, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, batKeese, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, batKeese, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, batKeese, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, blueCentaur, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, blueCentaur, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, blueCentaur, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, blueCentaur, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, blueGorya, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, blueGorya, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, blueGorya, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, blueGorya, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, blueKnight, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, blueKnight, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, blueKnight, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, blueKnight, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, darkMoblin, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, darkMoblin, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, darkMoblin, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, darkMoblin, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, dragonBoss, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, dragonBoss, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, dragonBoss, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, dragonBoss, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, dragonProjectile, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, dragonProjectile, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, dragonProjectile, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, dragonProjectile, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelBigGray, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelBigGray, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelBigGray, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelBigGray, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelBigGreen, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelBigGreen, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelBigGreen, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelBigGreen, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelSmallBlack, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelSmallBlack, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelSmallBlack, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelSmallBlack, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelSmallTeal, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelSmallTeal, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelSmallTeal, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, gelSmallTeal, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redCentaur, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redCentaur, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redCentaur, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redCentaur, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redGorya, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redGorya, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redGorya, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redGorya, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redKnight, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redKnight, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redKnight, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redKnight, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redMoblin, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redMoblin, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redMoblin, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redMoblin, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redOcto, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redOcto, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redOcto, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, redOcto, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, skeleton, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, skeleton, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, skeleton, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, skeleton, CollisionDirection.Bottom), moveLinkDownAndGetHurt);
            list.Add(KeyGenerator.Generate(link, wallMaster, CollisionDirection.Left), moveLinkLeftAndGetHurt);
            list.Add(KeyGenerator.Generate(link, wallMaster, CollisionDirection.Top), moveLinkUpAndGetHurt);
            list.Add(KeyGenerator.Generate(link, wallMaster, CollisionDirection.Right), moveLinkRightAndGetHurt);
            list.Add(KeyGenerator.Generate(link, wallMaster, CollisionDirection.Bottom), moveLinkDownAndGetHurt);

            //enemies vs blocks

            //link items vs enemies

            return list;
        }
    }
}
