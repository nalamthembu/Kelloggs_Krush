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

    [SerializeField] Transform m_BallIndicator;
    [SerializeField] Transform m_PlayerAimIndicator;

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

        if (m_PlayerAimIndicator is null || m_BallIndicator is null)
        {
            Debug.LogError("Player aim location or player ball indicator is null");
        }
    }

    private void Update()
    {
        IncrementTime();
        SetBallIndicatorLocation();
        SetBallIndicatorVisibility();
    }

    public void SetPlayerAimIndicatorLocation(Vector3 pos) => m_PlayerAimIndicator.position = pos;

    private void SetBallIndicatorVisibility()
    {
        switch (m_Resposibility)
        {
            case RESPONSIBILITY.ENEMY:
                m_BallIndicator.gameObject.SetActive(false);
                break;

            case RESPONSIBILITY.PLAYER:
                m_BallIndicator.gameObject.SetActive(true);
                break;
        }
    }

    private void IncrementTime()
    {
        m_TimeElasped += Time.deltaTime;
    }

    private void SetBallIndicatorLocation()
    {
        m_BallIndicator.position =
            Vector3.right * m_Ball.transform.position.x +
            Vector3.forward * m_Ball.transform.position.z;
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

    public void SetGameOver() => m_GameIsOver = true;

}
