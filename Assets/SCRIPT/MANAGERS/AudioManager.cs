using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public static AudioManager AUDIO_MANAGER;

    [SerializeField] AudioMixerGroup m_UIMixerGroup;
    [SerializeField] AudioMixerGroup m_SFXMixerGroup;
    [SerializeField] AudioMixerGroup m_MusicMixerGroup;

    private AudioSource m_UIAudioSource;
    private AudioSource m_MusicAudioSource;

    public AudioMixerGroup GetMusicMixerGroup()
    {
        return m_MusicMixerGroup;
    }

    private void Awake()
    {
        if (AUDIO_MANAGER is null)
        {
            AUDIO_MANAGER = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_UIAudioSource = InitFlatSource(m_UIMixerGroup);
        m_MusicAudioSource = InitFlatSource(m_MusicMixerGroup);
    }

    private AudioSource InitFlatSource(AudioMixerGroup mixerGroup)
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.spatialBlend = 0;
        newSource.playOnAwake = false;
        newSource.outputAudioMixerGroup = mixerGroup;
        return newSource;
    }

    public void PlayAudio(AudioClip clip, bool IsMusic = false)
    {
        if (IsMusic)
        {
            m_MusicAudioSource.clip = clip;
            m_MusicAudioSource.loop = true;
            m_MusicAudioSource.Play();

            return;
        }

        m_UIAudioSource.clip = clip;
        m_UIAudioSource.loop = false;
        m_UIAudioSource.Play();
    }

    public void PlayAudio(AudioClip clip, Vector3 point)
    {
        GameObject tmpObject = new("tmp_sfx");
        AudioSource s = tmpObject.AddComponent<AudioSource>();
        s.clip = clip;
        s.pitch = Random.Range(1, 1.25f);
        s.outputAudioMixerGroup = m_SFXMixerGroup;
        s.spatialBlend = 1;

        tmpObject.transform.position = point;

        s.Play();
        Destroy(tmpObject, s.clip.length + 0.1f);
    }
}
