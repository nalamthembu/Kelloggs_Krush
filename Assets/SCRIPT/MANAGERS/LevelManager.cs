using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static Managers.UIManager;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public static LevelManager LEVEL_MANAGER;

    [SerializeField] GameObject m_LoadingScreen;
    [SerializeField] Slider m_LoadingBar;

    private void Awake()
    {
        if (LEVEL_MANAGER is null)
        {
            LEVEL_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        UI_MANAGER.FadeScreenToBlack();
        yield return new WaitForSeconds(2);
        m_LoadingScreen.SetActive(true);
        UI_MANAGER.FadeScreenToClear();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            m_LoadingBar.value = progress;
            yield return null;
        }

        yield return new WaitForSeconds(2);

        UI_MANAGER.CutToBlack();

        m_LoadingScreen.SetActive(false);

        yield return new WaitForSeconds(1);

        UI_MANAGER.FadeScreenToClear();

    }
}
