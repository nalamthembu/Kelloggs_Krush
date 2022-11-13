using static AudioManager;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager MUSIC_MANAGER;

    private AudioSource m_Source;

    [SerializeField] MusicLibraryScriptable m_MusicLibrary;

    int currentSongIndex = 0;

    private void Awake()
    {
        if (MUSIC_MANAGER is null)
        {
            MUSIC_MANAGER = this;
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
        InitialiseAudioSource();

        PlaySong(Random.Range(0, m_MusicLibrary.music.Length));
    }

    void InitialiseAudioSource()
    {
        m_Source = gameObject.AddComponent<AudioSource>();
        m_Source.spatialBlend = 0;
        m_Source.playOnAwake = false;
        m_Source.outputAudioMixerGroup = AUDIO_MANAGER.GetMusicMixerGroup();
    }

    private void Update()
    {
        if (!m_Source.isPlaying)
        {
            NextSong();
        }
    }

    public void PlaySong()
    {
        if (currentSongIndex > m_MusicLibrary.music.Length)
            currentSongIndex = 0;

        if (currentSongIndex < 0)
            currentSongIndex = m_MusicLibrary.music.Length;

        m_Source.clip = m_MusicLibrary.music[currentSongIndex].clip;

        m_Source.Play();
    }

    public void PlaySong(int index)
    {
        currentSongIndex = index;

        if (currentSongIndex > m_MusicLibrary.music.Length)
            currentSongIndex = 0;

        if (currentSongIndex < 0)
            currentSongIndex = m_MusicLibrary.music.Length;

        m_Source.clip = m_MusicLibrary.music[currentSongIndex].clip;

        m_Source.Play();
    }

    public void NextSong()
    {
        currentSongIndex++;

        if (m_Source is null)
            InitialiseAudioSource();

        if (currentSongIndex > m_MusicLibrary.music.Length)
            currentSongIndex = 0;

        m_Source.clip = m_MusicLibrary.music[currentSongIndex].clip;

        m_Source.Play();
    }

    public void PreviousSong()
    {
        currentSongIndex--;

        if (m_Source is null)
            InitialiseAudioSource();

        if (currentSongIndex < 0)
            currentSongIndex = m_MusicLibrary.music.Length;

        m_Source.clip = m_MusicLibrary.music[currentSongIndex].clip;

        m_Source.Play();

    }
}