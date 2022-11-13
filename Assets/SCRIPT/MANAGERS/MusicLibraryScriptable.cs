using UnityEngine;

[CreateAssetMenu(fileName = "Music_Library", menuName = "Data Storage/Music Library")]
public class MusicLibraryScriptable : ScriptableObject
{
    public Music[] music;
}

[System.Serializable]
public class Music
{
    public string title;
    public string artist;
    public AudioClip clip;
}