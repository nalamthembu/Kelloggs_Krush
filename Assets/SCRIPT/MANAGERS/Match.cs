using UnityEngine;

public enum RESPONSIBILITY
{
    PLAYER,
    ENEMY
}

public class Match : MonoBehaviour
{
    RESPONSIBILITY m_Resposibility;

    float m_TimeElasped;

    bool m_GameIsOver = false;

    public static Match MATCH;

    private void Awake()
    {
        if (MATCH is null)
        {
            MATCH = this;
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() => StartGame();

    public void StartGame() => m_GameIsOver = true;

    public void SetResponsibility(RESPONSIBILITY rsp) => m_Resposibility = rsp;

    public void Update()
    {
        m_TimeElasped += Time.deltaTime;
    }

    public void SetGameOver() => m_GameIsOver = true;

}
