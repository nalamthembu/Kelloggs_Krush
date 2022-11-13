using UnityEngine;
using Gameplay;
using System;
using TMPro;

public enum RESPONSIBILITY
{
    PLAYER,
    ENEMY
}

public class Match : MonoBehaviour
{
    #region PROPERTIES

    [SerializeField] Color[] m_PlayerColours;

    [SerializeField] Transform m_BallIndicator;
    [SerializeField] Transform m_PlayerAimIndicator;
    [SerializeField] GameObject m_EndOfMatchCamera;
    [SerializeField] MatchUserInterface m_MatchUI;
    RESPONSIBILITY m_Resposibility;

    float m_TimeElasped;

    bool m_GameIsOver = false;

    Ball m_Ball;

    [SerializeField] bool m_IsTutorial = false;

    #endregion

    #region PRIVATE
    private void Awake()
    {
        if (m_IsTutorial)
            return;

        if (m_PlayerAimIndicator is null || m_BallIndicator is null)
        {
            Debug.LogError("Player aim location or player ball indicator is null");
        }
    }

    private void Update()
    {
        if (m_IsTutorial)
            return;

        if (m_GameIsOver)
        {
            if (Time.timeScale > 0.005f)
                Time.timeScale = Mathf.Lerp(Time.timeScale, 0, Time.deltaTime * 10);
            else
                Time.timeScale = 0;
        }
        else
        {
            IncrementTime();
        }

        SetBallIndicatorLocation();
        SetBallIndicatorVisibility();
    }

    public void ResettingGame() => m_Ball.GetComponent<Rigidbody>().isKinematic = true;

    public bool IsGameOver()
    {
        return m_GameIsOver;
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

        m_MatchUI.SetElapsedTime(m_TimeElasped);
    }

    private void SetBallIndicatorLocation()
    {
        m_BallIndicator.position =
            Vector3.right * m_Ball.transform.position.x +
            Vector3.forward * m_Ball.transform.position.z;
    }

    private void GetBall() => m_Ball = FindObjectOfType<Ball>();

    private void Start() => StartGame();

    #endregion

    #region PUBLIC

    public bool IsInTutorialMode()
    {
        return m_IsTutorial;
    }

    public int GetPlayerColourCount()
    {
        return m_PlayerColours.Length - 1;
    }

    public Color[] GetPlayerColours()
    {
        return m_PlayerColours;
    }

    public void StartGame()
    {
        GetBall();

        if (IsInTutorialMode())
            return;

        m_MatchUI.MakeEndOfGameUIVisibleWin(false);
        m_MatchUI.MakeInGameUIVisible(true);
        m_GameIsOver = false;
    }

    public void SetResponsibility(RESPONSIBILITY rsp)
    {
        m_Resposibility = rsp;

        m_Ball.ChangeColour(rsp);
    }

    public void SetGameOver()
    {
        m_GameIsOver = true;

        switch (m_Resposibility)
        {
            case RESPONSIBILITY.ENEMY:
                m_MatchUI.MakeEndOfGameUIVisibleLoss(true);
                break;

            case RESPONSIBILITY.PLAYER:
                m_MatchUI.MakeEndOfGameUIVisibleWin(true);
                break;
        }

        m_MatchUI.MakeInGameUIVisible(false);
        m_EndOfMatchCamera.SetActive(true);
    }

    public float GetElapsedTime()
    {
        return m_TimeElasped;
    }

    #endregion
}

#region USER_INTERFACE
[Serializable]
public class MatchUserInterface
{
    public TMP_Text m_ElapsedTime;
    public TMP_Text m_EndOfGameElapsedTime;
    public GameObject m_InGameUI;
    public GameObject m_EndOfGameUILoss;
    public GameObject m_EndOfGameUIWin;

    public void SetElapsedTime(float timePassed) =>
        m_ElapsedTime.text = m_EndOfGameElapsedTime.text = FormattedTime(timePassed);

    public void MakeEndOfGameUIVisibleLoss(bool isVisible) => m_EndOfGameUILoss.SetActive(isVisible);
    public void MakeInGameUIVisible(bool isVisible) => m_InGameUI.SetActive(isVisible);
    public void MakeEndOfGameUIVisibleWin(bool isVisible) => m_EndOfGameUILoss.SetActive(isVisible);



    private static string FormattedTime(float currentTime)
    {
        TimeSpan t = TimeSpan.FromSeconds(currentTime);

        var sb = new System.Text.StringBuilder();

        return sb.Append(string.Format
            (
                "{0:00}:{1:00}:{2:000}",
                 t.Minutes,
                 t.Seconds,
                 Mathf.FloorToInt(t.Milliseconds) / 10f
            )).ToString();
    }
}
#endregion
