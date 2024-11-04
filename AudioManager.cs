using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Legend_of_the_Power_Rangers
{
    public class AudioManager
    {
        private static AudioManager instance;
        private Dictionary<string, SoundEffect> soundEffects;
        private bool isMuted;

        private AudioManager()
        {
            soundEffects = new Dictionary<string, SoundEffect>();
            isMuted = false;
        }

        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AudioManager();
                }
                return instance;
            }
        }

        public void Initialize(ContentManager content)
        {
            soundEffects["Arrow_Boomerang"] = content.Load<SoundEffect>("LOZ_Arrow_Boomerang");
            //Done
            soundEffects["Bomb_Blow"] = content.Load<SoundEffect>("LOZ_Bomb_Blow");
            //Done
            soundEffects["Bomb_Drop"] = content.Load<SoundEffect>("LOZ_Bomb_Drop");
            soundEffects["Boss_Hit"] = content.Load<SoundEffect>("LOZ_Boss_Hit");
            soundEffects["Boss_Scream1"] = content.Load<SoundEffect>("LOZ_Boss_Scream1");
            soundEffects["Boss_Scream2"] = content.Load<SoundEffect>("LOZ_Boss_Scream2");
            soundEffects["Boss_Scream3"] = content.Load<SoundEffect>("LOZ_Boss_Scream3");
            soundEffects["Candle"] = content.Load<SoundEffect>("LOZ_Candle");
            soundEffects["Door_Unlock"] = content.Load<SoundEffect>("LOZ_Door_Unlock");
            soundEffects["Enemy_Die"] = content.Load<SoundEffect>("LOZ_Enemy_Die");
            soundEffects["Enemy_Hit"] = content.Load<SoundEffect>("LOZ_Enemy_Hit");
            soundEffects["Fanfare"] = content.Load<SoundEffect>("LOZ_Fanfare");
            //Done
            soundEffects["Get_Heart"] = content.Load<SoundEffect>("LOZ_Get_Heart");
            //Done
            soundEffects["Get_Item"] = content.Load<SoundEffect>("LOZ_Get_Item");
            //Done
            soundEffects["Get_Ruppee"] = content.Load<SoundEffect>("LOZ_Get_Rupee");
            soundEffects["Key_Appear"] = content.Load<SoundEffect>("LOZ_Key_Appear");
            soundEffects["Link_Die"] = content.Load<SoundEffect>("LOZ_Link_Die");
            soundEffects["Link_Hurt"] = content.Load<SoundEffect>("LOZ_Link_Hurt");
            soundEffects["LowHeath"] = content.Load<SoundEffect>("LOZ_LowHealth");
            soundEffects["MagicalRod"] = content.Load<SoundEffect>("LOZ_MagicalRod");
            soundEffects["Recorder"] = content.Load<SoundEffect>("LOZ_Recorder");
            soundEffects["Refill_Loop"] = content.Load<SoundEffect>("LOZ_Refill_Loop");
            soundEffects["Secret"] = content.Load<SoundEffect>("LOZ_Secret");
            soundEffects["Shield"] = content.Load<SoundEffect>("LOZ_Shield");
            soundEffects["Shore"] = content.Load<SoundEffect>("LOZ_Shore");
            soundEffects["Stairs"] = content.Load<SoundEffect>("LOZ_Stairs");
            soundEffects["Sword_Combined"] = content.Load<SoundEffect>("LOZ_Sword_Combined");
            soundEffects["Sword_Shoot"] = content.Load<SoundEffect>("LOZ_Sword_Shoot");
            //Done
            soundEffects["Sword_Slash"] = content.Load<SoundEffect>("LOZ_Sword_Slash");
            soundEffects["Text"] = content.Load<SoundEffect>("LOZ_Text");
            soundEffects["Text_Slow"] = content.Load<SoundEffect>("LOZ_Text_Slow");
        }

        public void PlaySound(string soundName)
        {
            if (!isMuted && soundEffects.ContainsKey(soundName))
            {
                soundEffects[soundName].Play();
            }
            else if (!soundEffects.ContainsKey(soundName))
            {
                System.Console.WriteLine($"Sound {soundName} not found.");
            }
        }

        public void Mute()
        {
            isMuted = true;
        }

        public void Unmute()
        {
            isMuted = false;
        }

        public bool IsMuted()
        {
            return isMuted;
        }
    }
}
