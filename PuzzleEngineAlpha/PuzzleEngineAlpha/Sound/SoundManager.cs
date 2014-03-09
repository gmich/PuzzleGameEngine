using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace myPlatformer
{
    public class SoundManager
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static Dictionary<string, SoundEffect> sfx = new Dictionary<string, SoundEffect>();
        static Dictionary<string, Song> songs = new Dictionary<string, Song>();

        public static void AddSfx(string sfxTag, SoundEffect soundfx)
        {
            try
            {
                sfx.Add(sfxTag, soundfx);

            }
            catch (Exception ex)
            {
                log.Error("Failed to load sfx " + sfxTag + " " + ex.Message);
            }
        }


        public static void AddSong(string songName, Song song)
        {
            try
            {
                songs.Add(songName, song);

            }
            catch (Exception ex)
            {
                log.Error("Failed to load song " + songName + " " + ex.Message);
            }
        }

        public static void PlaySFX(string sfxName, float pitch)
        {
            try
            {
                // if (sfx.ContainsKey(sfxName))
                sfx[sfxName].Play(0.2f, pitch, 0.0f);
            }
            catch (Exception ex)
            {
                log.Error("Failed to play sfx " + sfxName + " " + ex.Message);
            }
        }

        public static void PlaySong(string song, bool repeat, float volume)
        {
            try
            {
                MediaPlayer.IsRepeating = repeat;
                MediaPlayer.Volume = volume;

                //  if (songs.ContainsKey(song))
                MediaPlayer.Play(songs[song]);

            }
            catch (Exception ex)
            {
                log.Error("Failed to play song " + song + " " + ex.Message);
            }
        }

    }
}