using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Component_Buttons : MonoBehaviour
{
    public float originalFontSize = 55f;

    public Color defaultColor, highlightColor;
    public GameObject[] menuButtons;   
    
    [Space]
    
    public TMP_Text      faseNameTMP;
    public TMP_Text      faseDescTMP;
    
    public RectTransform fader;

    public string[]      faseNames;
    public string[]      faseDescriptions;

    // ==============================================
    // On Click
    
    public IEnumerator Transition()
    {
        float size = 0;

        while(size < 1934)
        {
            size += 200f;

            fader.anchoredPosition = new Vector2(fader.anchoredPosition.x + 100f, fader.anchoredPosition.y);
            fader.sizeDelta = new Vector2(size, 1081f);

            yield return null;
        }
    }

    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
        gameObject.SetActive(false);

        DisableHighligth();
    }

    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
     
    public void ExitGame()
    {
        Application.Quit();
    }

    // ==============================================
    // On Enter/On Exit
    
    public void DisplayFaseInfo(int id)
    {
        faseNameTMP.text = faseNames[id];
        faseDescTMP.text = faseDescriptions[id];
    }

    public void ResetFaseInfo()
    {
        faseNameTMP.text = "Escolha o nivel";
        faseDescTMP.text = "";
    }

    public void HighlightText(TMP_Text textTMP)
    {
        DisableHighligth();
        
        textTMP.fontSize = 65f;
        textTMP.color = highlightColor;
    }

    public void DisableHighligth()
    {
        foreach(GameObject btn in menuButtons)
        {
            TMP_Text textTMP = btn.transform.GetChild(0).GetComponent<TMP_Text>();

            textTMP.fontSize = originalFontSize;
            textTMP.color = defaultColor;
        }
    }
}