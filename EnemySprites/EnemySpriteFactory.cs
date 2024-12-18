﻿using Microsoft.Xna.Framework.Graphics;
using System;

namespace Legend_of_the_Power_Rangers
{
    public class EnemySpriteFactory
    {
        private static EnemySpriteFactory instance = new EnemySpriteFactory();
        private Texture2D enemySpriteSheet;
        private Texture2D projectileSpriteSheet;
        private Texture2D bossSpriteSheet;
        private Texture2D itemSpriteSheet;
        public static EnemySpriteFactory Instance
        {
            get { return instance; }
        }

        private EnemySpriteFactory() { }

        public void SetEnemySpritesheet(Texture2D spritesheet)
        {
            enemySpriteSheet = spritesheet;
        }

        public void SetProjectileSpritesheet(Texture2D spritesheet)
        {
            projectileSpriteSheet = spritesheet;
        }
        public void SetBossSpritesheet(Texture2D spritesheet)
        {
            bossSpriteSheet = spritesheet;
        }
        public void SetItemSpritesheet(Texture2D spritesheet)
        {
            itemSpriteSheet = spritesheet;
        }

        public IEnemy CreateEnemy(string enemyType)
        {
            switch (enemyType)
            {
                case "01":
                    return new BatKeese();
                case "02":
                    return new BlueCentaur();
                case "03":
                    return new BlueGorya();
                case "04":
                    return new BlueKnight();
                case "05":
                    return new BlueOcto(projectileSpriteSheet);
                case "06":
                    return new DarkMoblin();
                case "07":
                    return new DragonBoss(bossSpriteSheet, projectileSpriteSheet);
                case "08":
                    return new RedCentaur();
                case "09":
                    return new RedGorya(itemSpriteSheet);
                case "10":
                    return new RedKnight();
                case "11":
                    return new RedMoblin();
                case "12":
                    return new RedOcto(projectileSpriteSheet);
                case "13":
                    return new Skeleton();
                case "14":
                    return new GelSmallBlack();
                case "15":
                    return new WallMaster();
                case "16":
                    return new GelBigGray();
                case "17":
                    return new GelBigGreen();
                case "18":
                    return new GelSmallTeal();
                case "19":
                    return new TrapEnemy();
                case "98":
                    return new OldMan();
                default:
                    throw new ArgumentException($"Block type {enemyType} not recognized");
            }
        }

        public Texture2D GetEnemySpriteSheet() => enemySpriteSheet;
        public Texture2D GetprojectileSpriteSheet => projectileSpriteSheet;
    }
}
