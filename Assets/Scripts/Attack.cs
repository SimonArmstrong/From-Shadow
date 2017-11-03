using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationMode
{
    none,       // Use Quaternion.identity
    prefab,     // Use Prefab's rotation
    direction,  // transform.up = direction
    offset      // transform.up = offset
}

[System.Serializable]
public class AttackObjectInfo
{
    public AttackObject attackObject;
    public Vector2 spawnOffset;
    [Tooltip("If ticked, object will spawn according to the attack's direction")]
    public bool matchDirection;
    public RotationMode rotationMode;
}

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class Attack : ScriptableObject
{
    public float damage;
    public float cooldown;
    public float resetTime;
    public float force;
    public float knockback;

    [Space]
    public AttackObjectInfo[] objects;
}