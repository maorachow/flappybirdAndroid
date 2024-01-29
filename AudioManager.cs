using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AudioManager
{
    public static Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
    public static void Init(MainGame game)
    {
        soundEffects = new Dictionary<string, SoundEffect>
        {
            {"wing",game.Content.Load<SoundEffect>("sfx_wing") },
            {"point",game.Content.Load<SoundEffect>("sfx_point") },
            {"hit",game.Content.Load<SoundEffect>("sfx_hit") },
            {"die",game.Content.Load<SoundEffect>("sfx_die") },
            {"swooshing",game.Content.Load<SoundEffect>("sfx_swooshing") }
        };
    }
    public static void PlaySound(string soundName)
    {
        if (soundEffects.ContainsKey(soundName))
        {
            soundEffects[soundName].Play();
        }

    }
}
