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
    [SerializeField] Text nowText;
    [SerializeField] Text playLogText;
    [SerializeField] Text guideText;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Text resultText;
    int n = 0;
    int m = 3;
    int nss = 12;
    int randomNumber = 0;
    bool playerTurn = true;

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
        if (int.TryParse(inputField.text, out n) && n >= 1 && n <= m) {
            playLogText.text = "あなたが" + n.ToString() + "個の石を取りました";
            nss = nss - n;
            pileReload();
            if (nss >= 1) {
                Invoke("comAction1", 1);
            }
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
        // 1 ~ m random number
        randomNumber = UnityEngine.Random.Range(1, m + 1);
        nss = nss - randomNumber;
        playLogText.text = "Com が" + randomNumber.ToString() + "個の石を取りました";
        pileReload();
    }

    public void gameStart() {
        if (
            int.TryParse(inputMaxText.text, out m) && m >= 1 &&
            int.TryParse(inputPileText.text, out nss) && nss >= 1
        ) {
            panelManager.GetComponent<PanelManager>().closeSettingPanel();
            panelManager.GetComponent<PanelManager>().openMainPanel();
            pileReload();
        }
    }
}
