using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager_Menu : MonoBehaviour
{
    // ====================================================

    [Header("Buttons")]
    public TMP_Text   pauseButtonTMP;
    public TMP_Text   volumeSliderTMP;
    public TMP_Text   speedButtonTMP;

    [Header("Timer")]
    public GameObject timerObject;
    public TMP_Text   timerTMP;

    private float timerCount;
    private bool callTimer;

    [Header("Tower Tools Bar")]
    public GameObject   toolsMenu; 

    public TMP_Text     towerSellPriceTMP;
    public TMP_Text     towerUpgradePriceTMP;

    [Space]

    public Image[]      toolsButtonsHighlight;
    public TMP_Text[]   towerStatusTMP;

    [Header("Popups")]
    public GameObject[] alerts;

    [Header("Game Screens")]
    public GameObject winScreen;
    public GameObject loseScreen;

    // ====================================================

    private void Start()
    {
        //VolumeSlider(0.2f);
    }

    private void FixedUpdate()
    {
        TimerPopUP();
    }

    // ====================================================
    // Game Buttons

    public void StopMusic(bool _self)
    {
        AudioListener.pause = _self;
    }

    public void PauseButton()
    {
        if(Time.timeScale == 0f)
        {
            ChangeTime(1f);
            AudioListener.pause = false;
            pauseButtonTMP.text = "II";

            return;
        }
        
        ChangeTime(0f);
        AudioListener.pause = true;
        pauseButtonTMP.text = ">";
    }

    public void GameSpeedButton()
    {
        if(Time.timeScale <= 1f)
        {
            Time.timeScale = 3f;
            speedButtonTMP.text = "3X";            
            
            return;
        }
        else if(Time.timeScale != 6f)
        {
            Time.timeScale = 6f;    
            speedButtonTMP.text = "1X";

            return;
        }

        Time.timeScale = 1f;
        speedButtonTMP.text = "2X"; 
    }

    public void ChangeTime(float _time)
    {
        Time.timeScale = _time;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    // ==================================================== 
    // Utility

    public void ActiveObject(GameObject _object)
    {
        _object.SetActive(true);
    }

    public void DesactiveObject(GameObject _object)
    {
        _object.SetActive(false);
    }

    public void VolumeSlider(float volume)
    {
        volumeSliderTMP.text = (volume * 100).ToString("0") + "%";
        AudioListener.volume = volume;
    }

    public void HighlightOn(Image _image)
    {
        _image.color = new Color32(0,146,255, 255);
    }

    public void HighlightOff(Image _image)
    {
        _image.color = new Color32(255,255,255, 255);
    }

    public void DisableHighlights()
    {
        foreach(Image img in toolsButtonsHighlight)
        {
            img.color = new Color32(255, 255, 255, 255);
        }
    }

    // ==================================================== 
    // Alerts

    private void TimerPopUP()
    {
        if(!callTimer)
        {
            return;
        }

        timerTMP.text = string.Format("Próxima wave em: {00:00:00}", timerCount);
        timerCount -= Time.deltaTime;
    }

    public IEnumerator CallTimer(float time)
    {
        ActiveObject(timerObject);
        callTimer = true;
        timerCount = time;

        yield return new WaitForSeconds(time);

        DesactiveObject(timerObject);
        callTimer = false;
    }

    public IEnumerator CallAlert(string alertText, float time)
    {
        GameObject usingAlert = null;

        foreach(GameObject alert in alerts)
        {
            if(alert.activeSelf == false)
            {
                usingAlert = alert;

                usingAlert.SetActive(true);
                usingAlert.transform.GetChild(1).GetComponent<TMP_Text>().text = alertText;

                break;
            }
        }

        yield return new WaitForSeconds(time);

        usingAlert.SetActive(false);
    }

    public void LockedUpgradeAlert()
    {
        StartCoroutine(CallAlert("Desbloqueie essa torre na loja de melhorias!", 10f));
    }

    // ==================================================== 
    // Screens and Scene

    public void WinScreen()
    {
        ChangeTime(0f);
        winScreen.SetActive(true);
    }

    public void LoseScreen()
    {
        ChangeTime(0f);
        loseScreen.SetActive(true);
    }

    public void OnOpenToolsMenu(Component_Tower _tower)
    {
        Manager_Market market = GetComponent<Manager_Market>();
        toolsMenu.SetActive(true);  

        towerUpgradePriceTMP.text = $"Melhorar por {market.UpgradePrice()}";
        towerSellPriceTMP.text    = $"Vender por {market.SellPrice()}";

        for(int i = 0; i < towerStatusTMP.Length; i++)
        {
            towerStatusTMP[i].text = _tower.GetTowerStat(i);    
        }
    }

    public void SceneLoading(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SceneLoadingASync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}