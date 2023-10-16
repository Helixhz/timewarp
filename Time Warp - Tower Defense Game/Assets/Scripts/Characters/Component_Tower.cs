using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Component_Tower : MonoBehaviour, IPointerClickHandler
{
    public delegate void SelectTowerEvent(Component_Tower _tower);
    public delegate void OpenToolsMenuEvent(Component_Tower _tower);
    
    public event SelectTowerEvent SelectTower;
    public event OpenToolsMenuEvent OpenToolsMenu;

    private GameObject      currentTile;
    private Component_Enemy enemyLocked;
    private Manager_Level   levelManager;

    // ====================================================

    [Header("ID")]
    [SerializeField] private int marketID;
    [SerializeField] private int towerID;

    [Header("Voxel Adjust's")]

    public float adjustX;
    public float adjustY;
    public float adjustZ;
    public float rotationAdjust;
    
    [Header("Tower Info")]

    [SerializeField] private int        damage;
    [SerializeField] private float      rangeSize;

    [SerializeField] private GameObject rangeObject;
    [SerializeField] private Transform  rangeTransform;

    [Header("Projectile Settings")]
    [SerializeField] private float fireRate;
    [SerializeField] private float cooldown;

    [SerializeField] private GameObject  projectile;
    [SerializeField] private Transform[] firePoints;
    
    // ====================================================

    private void Awake()
    {
        Manager_Events.instance.TowerEvents(this);
    }

    private void Start()
    {
        rangeObject = transform.GetChild(1).gameObject;
        rangeTransform = transform.GetChild(1);
        rangeTransform.localScale = new Vector3(rangeSize, 0.1f, rangeSize);

        transform.position = new Vector3(transform.position.x + adjustX, adjustY, transform.position.z + adjustZ);
    }

    private void Update()
    {
        if(enemyLocked != null)
        {
            CheckTargetOutRange();
            return;
        }

        UpdateTarget();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowRange();

        SelectTower(this);
        OpenToolsMenu(this);
    }

    public void ShowRange()
    {
        this.rangeObject.SetActive(true);
    }

    public void HideRange()
    {
        this.rangeObject.SetActive(false);
    }

    // ====================================================
    // Shoot Method's
    
    private void CheckTargetOutRange()
    {
        if(Vector3.Distance(transform.position, enemyLocked.gameObject.transform.position) > rangeSize - 6.5f)
        {
            enemyLocked = null;
            return;
        }

        LookToEnemy();
        Shoot();
    }

    private void UpdateTarget()
    {
        foreach(GameObject enemy in levelManager.enemiesAlive)
        {   
            if(Vector3.Distance(transform.position, enemy.transform.position) <= rangeSize - 6.5f)
            {
                enemyLocked = enemy.GetComponent<Component_Enemy>();
            }
        }
    }

    private void Shoot()
    {
        if(cooldown > 0f)
        {
            cooldown -= Time.deltaTime;

            return;
        }

        foreach(Transform point in firePoints)
        {
            GameObject prjt = Instantiate(projectile, point.position, point.rotation);
            Component_Projectile bullet = prjt.GetComponent<Component_Projectile>();

            bullet.SetTarget(enemyLocked.transform);
            bullet.SetDamage(damage);

            cooldown = fireRate;
        }
    }

     private void LookToEnemy()
    {
        Vector3 dir = enemyLocked.gameObject.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        transform.LookAt(enemyLocked.gameObject.transform.position, transform.right);

        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y - rotationAdjust, 0f);
    }

    // ====================================================
    // Set ID'S

    public void SetLevelManager(Manager_Level _levelManager)
    {
        levelManager = _levelManager;
    }

    public void SetMarketID(int _id)
    {
        marketID = _id;
    }

    public void SetTile(GameObject _tile)
    {
        currentTile = _tile;
    }

    public int GetMarketID()
    {
        return marketID;
    }

    public int GetTowerID()
    {
        return towerID;
    }

    public GameObject GetCurrentTile()
    {
        return currentTile;
    }

    public string GetTowerStat(int id)
    {
        List<string> status = new List<string>();

        status.Add($"Dano: {damage}");

        if(rangeSize < 50)
        {
            status.Add("Curto");
        }
        else if(rangeSize <= 70)
        {
            status.Add("Médio");
        }
        else
        {
            status.Add("Longo");
        }

        status.Add($"Cooldown: {fireRate} segundos");

        return status[id];
    }

    /// ====================================================

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeSize - 6.5f);
    }
}