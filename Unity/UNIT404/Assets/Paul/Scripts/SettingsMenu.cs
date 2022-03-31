using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class SettingsMenu : MonoBehaviour
{
    public GameObject[] frame;
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void LoadSettings()
    {
        frame[1].SetActive(false);
        frame[2].SetActive(true);
    }
}
