﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Managers
{
    public class UIManager : Manager
    {
        public static UIManager UI_MANAGER;
        [SerializeField] Image m_BlackScreen;
        [SerializeField] GameObject m_PauseMenu;
        [SerializeField] GameObject m_MainMenuPrompt;
        [SerializeField] GameObject m_ExitPrompt;

        Match m_CurrentMatch;

        bool m_ResetTimeScale = false;

        private void Awake()
        {
            if (UI_MANAGER is null)
            {
                UI_MANAGER = this;
            }
            else
                return;

            DontDestroyOnLoad(gameObject);

            FindMatch();
        }

        public void OnLevelWasLoaded(int level)
        {
            FindMatch();
        }
        
        private void FindMatch()
        {
            m_CurrentMatch = FindObjectOfType<Match>();
        }

        private void Update()
        {
            if (m_CurrentMatch is not null && !m_CurrentMatch.IsInTutorialMode())
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    m_PauseMenu.SetActive(!m_PauseMenu.activeSelf);
                }
            }

            if (!m_ResetTimeScale)
            Time.timeScale = (m_PauseMenu.activeSelf) ? 0 : 1;

            if (m_ResetTimeScale)
                m_ResetTimeScale = false;
        }

        public void PromptMainMenu() => m_MainMenuPrompt.SetActive(true);

        public void PromptExit() => m_ExitPrompt.SetActive(true);

        public void ResetTimeScale()
        {
            m_ResetTimeScale = true;
            Time.timeScale = 1;
        }

        #region METHODS_AND_BOILER
        public void FadeScreenToBlack()
        {
            if (!m_BlackScreen)
            {
                Debug.Log("Black screen not initialised!");
                return;
            }

            StartCoroutine(FadeTo(FADE_To.BLACK));

        }

        public void CutToBlack() => m_BlackScreen.color += new Color(0, 0, 0, 1);

        public void FadeScreenToClear()
        {
            if (!m_BlackScreen)
            {
                Debug.Log("Black screen not initialised!");
                return;
            }

            StartCoroutine(FadeTo(FADE_To.CLEAR));
        }

        IEnumerator FadeTo(FADE_To colourToFadeTo)
        {
            float epsilomValue = 0.01f;


            switch (colourToFadeTo)
            {
                case FADE_To.BLACK:

                    while (m_BlackScreen.color.a < 1)
                    {
                        Color newAlpha = m_BlackScreen.color;

                        newAlpha.a += Time.deltaTime;

                        if (newAlpha.a > 0.9f + epsilomValue)
                        {
                            newAlpha.a = 1;
                        }

                        m_BlackScreen.color = newAlpha;

                        yield return new WaitForEndOfFrame();
                    }

                    break;

                case FADE_To.CLEAR:

                    while (m_BlackScreen.color.a > 0)
                    {
                        Color newAlpha = m_BlackScreen.color;

                        newAlpha.a -= Time.deltaTime;

                        if (newAlpha.a <= 0 + epsilomValue)
                        {
                            newAlpha.a = 0;
                        }

                        m_BlackScreen.color = newAlpha;

                        yield return new WaitForEndOfFrame();
                    }

                    break;

            }
        }
    }

    enum FADE_To
    {
        BLACK,
        CLEAR
    }
    #endregion
}