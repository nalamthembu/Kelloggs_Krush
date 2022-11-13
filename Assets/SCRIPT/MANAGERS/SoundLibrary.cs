using UnityEngine;


[CreateAssetMenu(fileName = "SoundLibrary", menuName = "Data Storage/SoundLibrary")]
public class SoundLibrary : ScriptableObject
{
    public Sound[] sounds;
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip[] clip;
    public SOUND sound;
    public SOUND_TYPE soundType;
}

public enum SOUND_TYPE
{
    UI,
    SFX,
    MUSIC
}

public enum SOUND
{
    SFX_BALL_HIT,
    UI_BUTTON_CLICK,
    MUSIC_INGAME
}