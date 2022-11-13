using UnityEngine;
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

    public void ExitGame() => GAME_MANAGER.ExitGame();
}
