using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_Projectile : MonoBehaviour
{
    private Transform target;
    private int damage;
    private float speed = 70f;

    // ====================================================

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        SeekTarget();
    }

    private void SeekTarget()
    {
        Vector3 dir = target.position - transform.position;
        float distance = speed * Time.deltaTime;

        if(dir.magnitude <= distance)
        {
            Component_Enemy enemy = target.GetComponent<Component_Enemy>();

            try
            {
                enemy.DamageResponse();
                enemy.LostLife(damage);
            }
            catch
            {
                Destroy(gameObject);
            }

            Destroy(gameObject);
            return;
        }

        transform.Translate(dir.normalized * distance, Space.World);
        transform.LookAt(target.position);
    }

    // ====================================================
    // Set projectile attributes
    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}