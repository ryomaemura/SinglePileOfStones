using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject resultPanel;

    // Start is called before the first frame update
    void Start() {
        closeMainPanel();
        openSettingPanel();
        closeResultPanel();
    }

    // Update is called once per frame
    void Update() {
    }

    public void openMainPanel() {
        mainPanel.SetActive(true);
    }

    public void closeMainPanel() {
        mainPanel.SetActive(false);
    }

    public void openSettingPanel() {
        settingPanel.SetActive(true);
    }

    public void closeSettingPanel() {
        settingPanel.SetActive(false);
    }

    public void openResultPanel() {
        resultPanel.SetActive(true);
    }

    public void closeResultPanel() {
        resultPanel.SetActive(false);
    }
}
