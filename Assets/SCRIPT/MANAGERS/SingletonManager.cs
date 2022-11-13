using UnityEngine;
using static Managers.UIManager;
using static LevelManager;
using static Managers.GameManager;

public class SingletonManager : MonoBehaviour
{
    public void LoadScene(int sceneToLoad) => LEVEL_MANAGER.LoadScene(sceneToLoad);
}
