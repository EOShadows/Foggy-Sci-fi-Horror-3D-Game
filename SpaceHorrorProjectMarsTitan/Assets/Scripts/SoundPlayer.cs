using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private static SoundPlayer main;

    public AudioClip[] sounds;
    private Dictionary<string, AudioClip> soundBank;

    private void Start()
    {
        if (!main)
        {

            soundBank = new Dictionary<string, AudioClip>();

            foreach (AudioClip sound in sounds)
            {
                soundBank.Add(sound.name, sound);
            }

            main = this;
        }
    }

    public void PlaySound(string sound, Vector3 position)
    {
        PlaySound(sound, position, 1);
    }

    public void PlaySound(string sound, Vector3 position, float volume)
    {
        if (!soundBank.ContainsKey(sound))
        {
            Debug.LogWarning("SoundPlayer.PlaySound() -> Sound does not exist.");
        }

        AudioSource.PlayClipAtPoint(soundBank[sound], position, volume);
    }

    private void PlaySound(AudioClip clip, Vector3 position, float volume)
    {
        if (!clip)
        {
            Debug.LogError("SoundPlayer.PlaySound() -> Sound is null.");
            return;
        }

        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public static SoundPlayer GetInstance()
    {
        return main;
    }
}
