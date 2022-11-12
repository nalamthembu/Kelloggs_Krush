using UnityEngine;
using System;
using TMPro;

public class Settings : MonoBehaviour
{
    [Header("Dropdowns")]
    [SerializeField] TMP_Dropdown m_ResolutionDimension;
    [SerializeField] TMP_Dropdown m_DisplayMode;

    private Resolution[] m_StoreResolutions;
    private FullScreenMode m_ScreenMode;

    int m_CountRes;

    #region Resolution and Display
    void AddResolutions(Resolution[] res)
    {
        m_CountRes = 0;
        //Display res's at current screen refresh rate
        for(int i = 0; i < res.Length; i++)
        {
            if (res[i].refreshRate.Equals(Screen.currentResolution.refreshRate))
            {
                m_StoreResolutions[m_CountRes] = res[i];
                m_CountRes++;
            }
        }

        //Add the dropdown string, value and listener (upon change) to each dropdown
        for(int i = 0; i < m_CountRes; i++)
        {
            m_ResolutionDimension.options.Add(new TMP_Dropdown.OptionData(ResolutionToString(m_StoreResolutions[i])));
        }
    }

    //Determines what screen mode we should use
    void ScreenOptions(string mode)
    {
        if (mode == "Full Screen")
            m_ScreenMode = FullScreenMode.ExclusiveFullScreen;
        else if (mode == "Windowed")
            m_ScreenMode = FullScreenMode.Windowed;
        else
            m_ScreenMode = FullScreenMode.FullScreenWindow;

        Screen.fullScreenMode = m_ScreenMode;
    }

    //Determine current resolution value upon starting the game
    void ResolutionInitialise(Resolution[] res)
    {
        for (int i = 0; i < res.Length; i++)
        {
            if (Screen.width == res[i].width && Screen.height == res[i].height)
                m_ResolutionDimension.value = i;
        }

        m_ResolutionDimension.RefreshShownValue();
    }

    //Determine screen mode upon starting the game
    void ScreenInitialise()
    {
        if (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
        {
            m_DisplayMode.value = 0;
        }
        else if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            m_DisplayMode.value = 1;
        }
        else
        {
            m_DisplayMode.value = 2;
            m_ScreenMode = FullScreenMode.FullScreenWindow;
        }
    }

    string ResolutionToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }

    #endregion

    private void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        Array.Reverse(resolutions);
        m_StoreResolutions = new Resolution[resolutions.Length];

        ScreenInitialise();
        AddResolutions(resolutions);
        ResolutionInitialise(m_StoreResolutions);

        m_DisplayMode.onValueChanged.AddListener(delegate { ScreenOptions(m_DisplayMode.options[m_DisplayMode.value].text); });
        m_ResolutionDimension.onValueChanged.AddListener(
            delegate
            {
                Screen.SetResolution(m_StoreResolutions[m_ResolutionDimension.value].width,
                m_StoreResolutions[m_ResolutionDimension.value].height, m_ScreenMode);
            });
    }
}
