using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_Enemy : MonoBehaviour
{
    [SerializeField] private int life;

    // ====================================================

    public void DamageResponse()
    {

    }

    public void LostLife(int _damage)
    {
        life -= _damage;
    }
}