using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackObject : MonoBehaviour
{
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public Vector2 attackPoint;
    [HideInInspector] public float damage;
    [HideInInspector] public float knockback;
}
