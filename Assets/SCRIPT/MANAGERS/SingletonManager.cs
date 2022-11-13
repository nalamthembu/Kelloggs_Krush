using UnityEngine;
using static MusicManager;
using static Managers.UIManager;
using static LevelManager;
using static Managers.GameManager;

public class SingletonManager : MonoBehaviour
{
    public void LoadScene(int sceneToLoad) => LEVEL_MANAGER.LoadScene(sceneToLoad);

    public void PromptMainMenu() => UI_MANAGER.PromptMainMenu();

    public void ResetGame()
    {
        Match currentMatch = FindObjectOfType<Match>();

        if (currentMatch)
            currentMatch.ResettingGame();
    }

    public void PlayUISound() => UI_MANAGER.PlayUISound();

    public void OverrideTimeScale() => UI_MANAGER.OverrideTimeScale();

    public void ExitGame() => GAME_MANAGER.ExitGame();

    public void PlayNextSong() => MUSIC_MANAGER.NextSong();
    public void PlayPreviousSong() => MUSIC_MANAGER.PreviousSong();
}
