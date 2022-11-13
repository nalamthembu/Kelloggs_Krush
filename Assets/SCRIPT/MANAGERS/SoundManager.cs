using static AudioManager;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SOUND_MANAGER;

    [SerializeField] SoundLibrary m_SoundLib;

    private void Awake()
    {
        if (SOUND_MANAGER is null)
        {
            SOUND_MANAGER = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public Sound GetSound(SOUND soundToFind)
    {
        for(int i = 0; i < m_SoundLib.sounds.Length; i++)
        {
            if (m_SoundLib.sounds[i].sound == soundToFind)
            {
                return m_SoundLib.sounds[i];
            }
        }

        Debug.LogError("Could not find specified sound");

        return null;
    }

    public void PlaySound(Sound soundToPlay)
    {
        switch (soundToPlay.soundType)
        {
            case SOUND_TYPE.MUSIC:
                AUDIO_MANAGER.PlayAudio(soundToPlay.clip[Random.Range(0, soundToPlay.clip.Length)], true);
                break;

            case SOUND_TYPE.UI:
                AUDIO_MANAGER.PlayAudio(soundToPlay.clip[Random.Range(0, soundToPlay.clip.Length)]);
                break;
        }
    }

    public void PlaySound(Sound soundToPlay, Vector3 location)
    {
        switch (soundToPlay.soundType)
        {
            case SOUND_TYPE.SFX:
                AUDIO_MANAGER.PlayAudio(soundToPlay.clip[Random.Range(0, soundToPlay.clip.Length)], location);
                break;
        }
    }
}