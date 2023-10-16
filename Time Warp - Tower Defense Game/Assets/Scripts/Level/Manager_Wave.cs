using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Wave : MonoBehaviour
{
    [SerializeField] private bool  canStartSpawn = false;
    [SerializeField] private float delay;
    
    public Transform spawnLocation;
    public Transform enemiesRoot;

    [Header("Configuração da Wave")]
    [SerializeField] private WaveData   currentWave;
    [SerializeField] private WaveData[] waves;

    public void Update()
    {
        if(canStartSpawn && !IsEnd())
        {
            WaveSpawner();
        }
    }
    public void WaveSpawner()
    {
        if(delay > 0f)
        {
            delay -= Time.deltaTime;

            return;
        }

        for(int i = 0; i < currentWave.quantity.Length; i++)
        {
            if(currentWave.quantity[i] > 0f && delay <= 0f)
            {
                GameObject enemy = Instantiate(currentWave.prefabs[i], spawnLocation.position, Quaternion.identity, enemiesRoot);
                
                currentWave.quantity[i] -= 1;
                delay = 0.5f;
            }
        }
    }

    public bool IsEnd()
    {
        if(currentWave.quantity[currentWave.quantity.Length - 1] <= 0f)
        { 
            canStartSpawn = false;

            return true;
        }

        return false;
    }

    // ====================================================

    public void SetCanSpawn(bool _self)
    {
        canStartSpawn = _self;
    }

    public void SetCurrentWave(int _waveID)
    {
        if(_waveID > waves.Length - 1)
        {
            this.enabled = false;

            return;
        }

        currentWave = waves[_waveID];
    }
}

[System.Serializable]
public class WaveData
{
    public int[]        quantity;
    public GameObject[] prefabs;
}