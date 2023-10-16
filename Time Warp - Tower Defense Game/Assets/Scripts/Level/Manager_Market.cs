using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager_Market : MonoBehaviour
{
    private int marketID  = -1;
    private int upgradeID = -1;
    private bool isBuying = false;

    [Header("Tower Market Bar")]
    [SerializeField] private int          currentMoney;
    [SerializeField] private GameObject   currentBlueprint;
    
    [Space]
    
    [Tooltip("Preços, prefabs e upgrades")]
    [SerializeField] private TowerData[]     towersData;
    [SerializeField] private GameObject[]    blueprints;

    [Space]

    [SerializeField] private TMP_Text     moneyTMP;
    [SerializeField] private TMP_Text[]   towerPricesTMP;
    [SerializeField] private GameObject[] lockedPanels;

    [Header("Helicopter")]
    public GameObject marketHelicopter;
    public Transform  towerRoot;

    [Header("Selections")]
    [SerializeField] private GameObject      selectionTile;
    [SerializeField] private Component_Tower selectedTower;

    // ====================================================
    private void Start()
    {
        SetupMarketBar();
    }

    private void Update()
    {
        moneyTMP.text = $"{currentMoney}";
    }

    private void SetupMarketBar()
    {
        for(int i = 0; i < towerPricesTMP.Length; i++)
        {
            towerPricesTMP[i].text = $"R$: {towersData[i].prices[0]}";
            
            if(towersData[i].isUnlocked[0])
            {
                lockedPanels[i].SetActive(false);
            }
        }
    }

    // ====================================================
    // ID's and Buttons

    public void SetMarketID(int _id)
    {
        marketID = _id;
    }

    public void BuyButton(int _id)
    {
        Destroy(currentBlueprint);

        marketID = _id;
        isBuying = true;

        currentBlueprint = Instantiate(blueprints[marketID], Vector3.zero, Quaternion.identity);
        currentBlueprint.GetComponent<Component_Blueprint>().SetMarket(this);
    }

    public void ResetAll()
    {
        Destroy(currentBlueprint);

        marketID = -1;
        isBuying = false;

        selectedTower.HideRange();
        selectedTower = null;
    }

    public void ResetID()
    {
        marketID = -1;
    }

    public void EndBuying()
    {
        isBuying = false;
    }

    public void IncreaseMoney(int _price)
    {
        currentMoney += _price;
    }

    public void DecreaseMoney(int _price)
    {
        currentMoney -= _price;
    }

    // ====================================================
    // Sell Tower

    public void SellTower()
    {
        GameObject _tile = selectedTower.GetComponent<Component_Tower>().GetCurrentTile();
        _tile.tag = "Tile/Empty";

        IncreaseMoney((SellPrice() * 20) / 100);
        Destroy(selectedTower.gameObject);
    }

    public int SellPrice()
    {
        return towersData[selectedTower.GetMarketID()].prices[selectedTower.GetTowerID()];
    }

    // ====================================================
    // Buy Tower

    private bool CanBuy(int _price)
    {
        if(currentMoney >= _price)
        {
            return true;
        }

        return false;
    }

    private bool IsBuying()
    {
        if(marketID <= -1 || !isBuying)
        {
            return false;
        }

        return true;
    }

    public void OnSelectTile(bool _activeSelf, Color32 _color, Transform _tilePostion)
    {
        selectionTile.SetActive(_activeSelf);
        selectionTile.GetComponent<Renderer>().material.color = _color;
        selectionTile.transform.position = _tilePostion.position;
    }

    public void OnSelectTower(Component_Tower _tower)
    {
        EndBuying();

        if(selectedTower != null && selectedTower != _tower)
        {
            selectedTower.HideRange();
        }

        selectedTower = _tower;
        
        marketID  = selectedTower.GetMarketID();
        upgradeID = selectedTower.GetTowerID();
    }
    
    public void OnBuyTower(GameObject _tile)
    {
        if(!IsBuying())
        {
            return;
        }

        if(!CanBuy(towersData[marketID].prices[0]))
        {
            return;
        }

        GameObject helicopter = Instantiate(marketHelicopter, Vector3.zero, Quaternion.identity, towerRoot);
        Component_Helicopter helicopterScript = helicopter.GetComponent<Component_Helicopter>();

        helicopterScript.SetDelivery(towersData[marketID].prefabs[0], _tile, _tile.transform, GetComponent<Manager_Level>());
        _tile.tag = "Tile/Busy";

        DecreaseMoney(towersData[marketID].prices[0]);
        EndBuying();
        ResetID();

        Destroy(currentBlueprint);
    }

    // ====================================================
    // Upgrade Tower

    private bool IsUnlocked()
    {
        if(towersData[selectedTower.GetMarketID()].isUnlocked[selectedTower.GetTowerID() + 1])
        {
            return true;
        }

        return false;
    }

    public int UpgradePrice()
    {
        return towersData[selectedTower.GetMarketID()].prices[selectedTower.GetTowerID() + 1];
    }

    private GameObject UpgradedTower()
    {
        return towersData[selectedTower.GetMarketID()].prefabs[selectedTower.GetTowerID() + 1];
    }

    public void UpgradeTower()
    {
        if(selectedTower.GetTowerID() >= towersData[selectedTower.GetMarketID()].prefabs.Length - 1)
        {
            return;
        }

        if(!IsUnlocked())
        {
            print("block");
            return;
        }

        if(!CanBuy(UpgradePrice()))
        {
            return;
        }
        
        GameObject upgradedTower = Instantiate(UpgradedTower(), selectedTower.gameObject.transform.position, Quaternion.identity, towerRoot);
        upgradedTower.GetComponent<Component_Tower>().SetTile(selectedTower.GetCurrentTile());
        upgradedTower.GetComponent<Component_Tower>().SetLevelManager(GetComponent<Manager_Level>());

        DecreaseMoney(UpgradePrice());
        Destroy(selectedTower.gameObject);
    }
}

[System.Serializable]
public class TowerData
{
    public string       towerName;

    public int[]        prices;
    public bool[]       isUnlocked;
    public GameObject[] prefabs;
}