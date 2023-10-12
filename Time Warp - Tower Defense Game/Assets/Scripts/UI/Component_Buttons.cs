using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Component_Buttons : MonoBehaviour
{
    public float   originalFontSize = 55f;
    public Color defaultColor, highlightColor;

    [Space]
    public GameObject[] buttons;   

    // ==============================================
    // On Click

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
    // Highligths
    
    public void HighlightText(TMP_Text textTMP)
    {
        DisableHighligth();
        
        textTMP.fontSize = 65f;
        textTMP.color = highlightColor;
    }

    public void DisableHighligth()
    {
        foreach(GameObject btn in buttons)
        {
            TMP_Text textTMP = btn.transform.GetChild(0).GetComponent<TMP_Text>();

            textTMP.fontSize = originalFontSize;
            textTMP.color = defaultColor;
        }
    }
}