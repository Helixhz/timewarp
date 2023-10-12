using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Component_FpsCounter : MonoBehaviour
{
    public TMP_Text fprCountTMP;
    public float deltaTime;

    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fprCountTMP.text = "Fps: " + Mathf.Ceil(fps).ToString();
    }
}