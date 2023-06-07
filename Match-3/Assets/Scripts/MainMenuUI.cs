using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject settingsPanel;
    private bool onOffPanel;   

    private void Start()
    {
        onOffPanel = false;
        settingsPanel.SetActive(onOffPanel);
    }

    public void SettingsPanelOnOff()
    {
        startPanel.SetActive(onOffPanel);
        settingsPanel.SetActive(!onOffPanel);
        onOffPanel = !onOffPanel;
    }

    public void InputInMenu()
    {
        startPanel.SetActive(true);
    }

    public void BackToGame()
    {
        startPanel.SetActive(false);
    }
}
