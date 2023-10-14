using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Gallery : MonoBehaviour
{
    [Header("See Tower/Enemies")]

    private GameObject    currentPrefab;

    public TMP_Text      nickname;
    public TMP_Text      towerName;
    public TMP_Text      description;

    public TowerInfo[]   informations;

    [Space]
    
    public Image[]       gridImages;
    public Sprite[]      towerSprites;
    public Sprite[]      enemiesSprites;

    [Space]

    public GameObject[]  towerPrefabs;
    public GameObject[]  enemiesPrefabs;

    private GameObject[] currentPrefabs;

    public void Start()
    {
        TowerGrid();
    }

    public void SpawnObject(int id)
    {
        TowerInfo currentInfo = informations[id];

        currentPrefab    = Instantiate(currentPrefabs[id], Vector3.zero, Quaternion.identity);

        nickname.text    = currentInfo.nickname;
        towerName.text   = currentInfo.towerName;
        description.text = currentInfo.description;
    }

    public void DestroyObject()
    {
        Destroy(currentPrefab);
    }

    public void TowerGrid()
    {
        currentPrefabs = towerPrefabs;

        foreach(Image img in gridImages)
        {
            img.gameObject.SetActive(false);
            img.sprite = null;
        }

        for(int i = 0; i < towerSprites.Length; i++)
        {
            gridImages[i].gameObject.SetActive(true);
            gridImages[i].sprite = towerSprites[i];
        }
    }

    public void EnemiesGrid()
    {
        currentPrefabs = enemiesPrefabs;

        foreach(Image img in gridImages)
        {
            img.gameObject.SetActive(false);
            img.sprite = null;
        }

        for(int i = 0; i < enemiesSprites.Length; i++)
        {
            gridImages[i].gameObject.SetActive(true);
            gridImages[i].sprite = enemiesSprites[i];
        }
    }
}

[System.Serializable]
public class TowerInfo
{
    public string nickname;
    public string towerName;
    public string description;
}