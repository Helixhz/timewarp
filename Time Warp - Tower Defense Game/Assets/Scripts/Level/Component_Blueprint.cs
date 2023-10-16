using UnityEngine;

public class Component_Blueprint : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;

    private Manager_Market marketManager;

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(1))
        {
            marketManager.ResetID();
            marketManager.EndBuying();

            Destroy(gameObject);
        }

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.CompareTag("Tile/Empty") || hit.collider.CompareTag("Tile/Busy") || hit.collider.CompareTag("Characters/Tower"))
            {   
                transform.position = hit.point;
            }
        }
    }

    public void SetMarket(Manager_Market _manager)
    {
        marketManager = _manager;
    }
}
