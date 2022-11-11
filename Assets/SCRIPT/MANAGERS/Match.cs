using UnityEngine;
using Gameplay;

public enum RESPONSIBILITY
{
    PLAYER,
    ENEMY
}

public class Match : MonoBehaviour
{
    [SerializeField] Color[] m_PlayerColours;

    RESPONSIBILITY m_Resposibility;

    float m_TimeElasped;

    bool m_GameIsOver = false;

    Ball m_Ball;

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

    private void GetBall() => m_Ball = FindObjectOfType<Ball>();

    public int GetPlayerColourCount()
    {
        return m_PlayerColours.Length - 1;
    }

    public Color[] GetPlayerColours()
    {
        return m_PlayerColours;
    }

    private void Start() => StartGame();

    public void StartGame()
    {
        GetBall();
        m_GameIsOver = false;
    }

    public void SetResponsibility(RESPONSIBILITY rsp)
    {
        m_Resposibility = rsp;

        m_Ball.ChangeColour(rsp);
    }

    public void Update()
    {
        m_TimeElasped += Time.deltaTime;
    }

    public void SetGameOver() => m_GameIsOver = true;

}
