using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [HideInInspector] public float damage;
    public bool destroyOnHit;

    void Start()
    {
        if (GetComponent<AttackObject>() != null)
            damage = GetComponent<AttackObject>().damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<enemy>() != null)
        {
            other.GetComponent<enemy>().Damage(gameObject, damage, true);
            if (destroyOnHit)
                Destroy(gameObject);
        }
    }
}
