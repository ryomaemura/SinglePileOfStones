using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NumberManager : MonoBehaviour
{
    [SerializeField] GameObject panelManager;
    [SerializeField] TMP_InputField inputMaxText;
    [SerializeField] TMP_InputField inputPileText;
    [SerializeField] Dropdown FoulMoveDropdown;
    [SerializeField] Dropdown ComLevelDropdown;
    [SerializeField] Text nowText;
    [SerializeField] Text playLogText;
    [SerializeField] Text guideText;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Text resultText;
    int n = 0;
    int temp_n = 0;
    int m = 3;
    int nss = 12;
    bool playerTurn = true;
    bool foulMove = false;
    int comLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        pileReload();
        playLogText.text = "";
        guideText.text = "取りたい石の個数を入力して下さい";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void takeStones() {
        playerTurn = true;
        if (int.TryParse(inputField.text, out n) && n >= 1 && n <= m && n <= nss && n != temp_n) {
            playLogText.text = "あなたが" + n.ToString() + "個の石を取りました";
            temp_n = n;
            nss = nss - n;
            pileReload();
            if (nss >= 1) {
                if (comLevel == 1) {
                    Invoke("comAction1", 1);
                } else if ((!foulMove && comLevel == 2) || (foulMove && m % 2 == 0)) {
                    Invoke("comAction2", 1);
                }
            }
        } else if (n == temp_n) {
            playLogText.text = temp_n.ToString() + "以外の数字を入力して下さい";
        } else if (n >= 1 && n <= m && n > nss) {
            playLogText.text = nss.ToString() + "以下の数字を入力して下さい";
        } else {
            playLogText.text = "1~" + m.ToString() + "の数字を入力して下さい";
        }
    }

    public void pileReload() {
        nowText.text = "山：" + nss.ToString() + "個    1~" + m.ToString() + "個の石を取得可能";
        if (nss <= 0) {
            panelManager.GetComponent<PanelManager>().openResultPanel();

            if (playerTurn) {
                resultText.text = "You win !!";
            } else {
                resultText.text = "You lose";
            }
        }
    }

    // Output random numbers
    public void comAction1() {
        playerTurn = false;
        while (n == temp_n || n > nss) {
            // 1 ~ m random number
            n = UnityEngine.Random.Range(1, m + 1);
        }
        temp_n = n;
        nss = nss - n;
        playLogText.text = "Com が" + n.ToString() + "個の石を取りました";
        pileReload();
    }

    // FoulMove:False BestCom  FoulMove:True m=2n BestCom
    public void comAction2() {
        playerTurn = false;
        if (nss <= m) {
            n = nss;
            nss = nss - n;
            playLogText.text = "Com が" + n.ToString() + "個の石を取りました";
            pileReload();
        } else if (nss % (m + 1) != 0) {
            n = nss % (m + 1);
            nss = nss - n;
            playLogText.text = "Com が" + n.ToString() + "個の石を取りました";
            pileReload();
        } else {
            nss = nss - 1;
            playLogText.text = "Com が" + (1).ToString() + "個の石を取りました";
            pileReload();
        }
    }

    // FoulMove:True m=3 BestCom
    public void comAction3() {
        playerTurn = false;
    }

    public void gameStart() {
        if (
            int.TryParse(inputMaxText.text, out m) && m >= 1 &&
            int.TryParse(inputPileText.text, out nss) && nss >= 1
        ) {
            panelManager.GetComponent<PanelManager>().closeSettingPanel();
            panelManager.GetComponent<PanelManager>().openMainPanel();
            pileReload();
            temp_n = 0;
            playLogText.text = "";
            if (FoulMoveDropdown.value == 0) {
                foulMove = false;
            } else {
                foulMove = true;
            }
            comLevel = ComLevelDropdown.value + 1;
        }
    }
}
