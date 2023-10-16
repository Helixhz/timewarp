using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager_Level : MonoBehaviour
{
    [Header("Wave")]
    public int currentWave;
    public int waveMax;

    public TMP_Text waveTMP;

    public List<GameObject> enemiesAlive = new List<GameObject>();
    public void WaveText()
    {
        waveTMP.text = $"{currentWave}/{waveMax}";
    }
}