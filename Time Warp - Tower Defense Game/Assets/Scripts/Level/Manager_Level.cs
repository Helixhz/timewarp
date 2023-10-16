using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager_Level : MonoBehaviour
{
    [Header("Wave")]
    public int waveRound;
    public int waveMax;
    private int waveEnemiesCountage;

    public TMP_Text waveTMP;

    public List<GameObject> enemiesAlive = new List<GameObject>();
    public List<Manager_Wave> waves = new List<Manager_Wave>();
    
    public void NewWave()
    {
        waveRound += 1;

        waveTMP.text = $"{waveRound + 1}/{waveMax}";

        foreach(Manager_Wave wave in waves)
        {
            wave.SetCurrentWave(waveRound);
            wave.SetCanSpawn(true);
        }
    }
}