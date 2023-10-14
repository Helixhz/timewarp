using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager_Tutorial : MonoBehaviour
{
    public TMP_Text                   dialogueTMP;
    public GameObject                 toNextTMP;

    private string                    currentDialogue;

    [SerializeField] private int      dialogueIndex = -1;
    [SerializeField] private string[] dialogues;

    [Header("Keyboard")]

    public  GameObject                 keyTogglesParent;
    private int                        keyCount;

    [SerializeField] private Toggle[]  keyToggles;
    [SerializeField] private string[]  keyNames;

    private void Start()
    {
        NextDialogue();
    }

    private void Update()
    {
        TutorialManager();
    }

    // ====================================================
    // Dialogue box

    public void NextDialogue()
    {
        if(dialogueIndex + 1 > dialogues.Length - 1)
        {
            return;
        }

        if(dialogueTMP.text == currentDialogue)
        {
            dialogueIndex += 1;
        }

        if(currentDialogue != dialogues[dialogueIndex])
        {
            StopAllCoroutines();
            StartCoroutine(Writer());

            currentDialogue = dialogues[dialogueIndex];
        }
    }

    private IEnumerator Writer()
    {
        dialogueTMP.text = "";
        toNextTMP.SetActive(false);

        foreach(char c in dialogues[dialogueIndex])
        {
            dialogueTMP.text += c;
            yield return new WaitForSeconds(1f * Time.deltaTime);
        }

        toNextTMP.SetActive(true);
    }

    // ====================================================
    // Tutorial Parts

    private void TutorialManager()
    {
        if(dialogueIndex == 2)
        {
            keyTogglesParent.SetActive(true);
            KeyboardTutorial();

            return;
        }
            
        if(Input.GetMouseButtonDown(0))
        {
            NextDialogue();
        }
    }

    private void KeyboardTutorial()
    {
        if(keyCount >= keyToggles.Length - 1 && Input.GetMouseButtonDown(0))
        {
            keyTogglesParent.SetActive(false);
            NextDialogue();

            return;
        }

        toNextTMP.SetActive(false);

        for(int id = 0; id < keyToggles.Length; id++)
        {
            if(keyToggles[id].isOn == false && Input.GetKeyDown(keyNames[id]))
            {
                keyToggles[id].isOn = true;
                keyCount += 1;
            }
        }
    }

    private void TowerTutorial()
    {

    }
}