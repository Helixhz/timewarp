using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Events : MonoBehaviour
{
    public static Manager_Events instance;

    private Manager_Menu        menuManager;
    private Manager_Market      marketManager;
    private Component_Tile[]    tiles;

    private void Awake()
    {
        instance = this;

        menuManager   = GetComponent<Manager_Menu>();
        marketManager = GetComponent<Manager_Market>();
        tiles         = FindObjectsOfType<Component_Tile>();
    }

    private void Start()
    {
        TilesEvents();
        MarketEvents();
    }

    private void MarketEvents()
    {

    }

    private void TilesEvents()
    {
        foreach(Component_Tile tile in tiles)
        {
            tile.SelectTile += marketManager.OnSelectTile;
            tile.BuyTower   += marketManager.OnBuyTower;
        }
    }

    public void TowerEvents(Component_Tower _tower)
    {
        _tower.SelectTower += marketManager.OnSelectTower;
        _tower.OpenToolsMenu += menuManager.OnOpenToolsMenu;
    }
}