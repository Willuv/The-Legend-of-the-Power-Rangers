﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace Legend_of_the_Power_Rangers
{
    public class AudioManager
    {
        private static AudioManager instance;
        private Dictionary<string, SoundEffect> soundEffects;
        private Dictionary<string, Song> music;
        private SoundEffectInstance currentMusicInstance;
        private bool isMuted;
        public float MusicVolume { get; set; } = 0.5f;

        private AudioManager()
        {   
            soundEffects = new Dictionary<string, SoundEffect>();
            music = new Dictionary<string, Song>();
            isMuted = false;
            MediaPlayer.Volume = MusicVolume;
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
            music["Dungeon"] = content.Load<Song>("Dungeon");
            music["Main_Theme"] = content.Load<Song>("Main Theme");
            music["Win"] = content.Load<Song>("Win");
            soundEffects["Arrow_Boomerang"] = content.Load<SoundEffect>("LOZ_Arrow_Boomerang");
            soundEffects["Bomb_Blow"] = content.Load<SoundEffect>("LOZ_Bomb_Blow");
            soundEffects["Bomb_Drop"] = content.Load<SoundEffect>("LOZ_Bomb_Drop");
            soundEffects["Boss_Hit"] = content.Load<SoundEffect>("LOZ_Boss_Hit");
            soundEffects["Boss_Scream1"] = content.Load<SoundEffect>("LOZ_Boss_Scream1");
            soundEffects["Candle"] = content.Load<SoundEffect>("LOZ_Candle");
            soundEffects["Door_Unlock"] = content.Load<SoundEffect>("LOZ_Door_Unlock");
            soundEffects["Enemy_Die"] = content.Load<SoundEffect>("LOZ_Enemy_Die");
            soundEffects["Enemy_Hit"] = content.Load<SoundEffect>("LOZ_Enemy_Hit");
            soundEffects["Get_Heart"] = content.Load<SoundEffect>("LOZ_Get_Heart");
            soundEffects["Get_Item"] = content.Load<SoundEffect>("LOZ_Get_Item");
            soundEffects["Get_Ruppee"] = content.Load<SoundEffect>("LOZ_Get_Rupee");
            soundEffects["Key_Appear"] = content.Load<SoundEffect>("LOZ_Key_Appear");
            soundEffects["Link_Die"] = content.Load<SoundEffect>("LOZ_Link_Die");
            soundEffects["Link_Hurt"] = content.Load<SoundEffect>("LOZ_Link_Hurt");
            soundEffects["LowHealth"] = content.Load<SoundEffect>("LOZ_LowHealth");
            soundEffects["Refill_Loop"] = content.Load<SoundEffect>("LOZ_Refill_Loop");
            soundEffects["Secret"] = content.Load<SoundEffect>("LOZ_Secret");
            soundEffects["Shield"] = content.Load<SoundEffect>("LOZ_Shield");
            soundEffects["Stairs"] = content.Load<SoundEffect>("LOZ_Stairs");
            soundEffects["Sword_Combined"] = content.Load<SoundEffect>("LOZ_Sword_Combined");
            soundEffects["Sword_Shoot"] = content.Load<SoundEffect>("LOZ_Sword_Shoot");
            soundEffects["Sword_Slash"] = content.Load<SoundEffect>("LOZ_Sword_Slash");
            soundEffects["Text"] = content.Load<SoundEffect>("LOZ_Text");
            soundEffects["Text_Slow"] = content.Load<SoundEffect>("LOZ_Text_Slow");
            soundEffects["Blue_Portal"] = content.Load<SoundEffect>("Blue Portal");
            soundEffects["Orange_Portal"] = content.Load<SoundEffect>("Orange Portal");
        }

        public void PlaySound(string soundName)
        {
            if (!isMuted && soundEffects.ContainsKey(soundName))
            {
                soundEffects[soundName].Play();
            }
            else if (!soundEffects.ContainsKey(soundName))
            {
                System.Console.WriteLine($"Sound effect {soundName} not found.");
            }
        }

        public void PlayMusic(string musicName)
        {
            MusicVolume = 0.5f;
            MediaPlayer.Volume = MusicVolume;
            if (!isMuted && music.ContainsKey(musicName))
            {
                MediaPlayer.Stop();
                if (musicName == "Dungeon")
                {
                    MediaPlayer.IsRepeating = true;
                }
                else
                {
                    MediaPlayer.IsRepeating = false;
                }
                MediaPlayer.Play(music[musicName]);
            }
        }

        public void Mute()
        {
            isMuted = true;
            MediaPlayer.Pause();
        }

        public void Unmute()
        {
            isMuted = false;
            MediaPlayer.Resume();
        }

        public bool IsMuted()
        {
            return isMuted;
        }
    }
}
