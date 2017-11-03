using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public Attack[] combo;
    float currentResetTime;

    int currentAttack;
    float currentCooldown;

    Rigidbody2D rb;

    void Start()
    {
        currentAttack = 0;
        currentCooldown = 0;
        currentResetTime = 0;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!Pause.paused)
        {
            currentCooldown -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && currentCooldown <= 0)
            {
                if (currentResetTime <= 0)
                    currentAttack = 0;

                currentCooldown = combo[currentAttack].cooldown;
                currentResetTime = combo[currentAttack].resetTime + combo[currentAttack].cooldown;

                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                direction.Normalize();

                for (int i = 0; i < combo[currentAttack].objects.Length; i++)
                {
                    GameObject attack;
                    if (combo[currentAttack].objects[i].matchDirection)
                    {
                        Vector2 directionNormal = Vector3.Cross
                        (
                            new Vector3(direction.x, direction.y, 0),
                            new Vector3(0, 0, 1)
                        ).normalized;

                        Vector3 direction3 = new Vector3(direction.x, direction.y, 0);
                        Vector3 directionNormal3 = new Vector3(directionNormal.x, directionNormal.y, 0);
                        Vector3 spawnOffset = direction3 * combo[currentAttack].objects[i].spawnOffset.y + directionNormal3 * combo[currentAttack].objects[i].spawnOffset.x;


                        attack = Instantiate
                        (
                            combo[currentAttack].objects[i].attackObject.gameObject,
                            transform.position + spawnOffset,
                            Quaternion.identity
                        );

                        attack.transform.up = direction;
                    }
                    else
                    {
                        attack = Instantiate
                        (
                            combo[currentAttack].objects[i].attackObject.gameObject,
                            transform.position + new Vector3
                            (
                                combo[currentAttack].objects[i].spawnOffset.x,
                                combo[currentAttack].objects[i].spawnOffset.y,
                                0
                            ),
                            Quaternion.identity
                        );
                    }

                    switch (combo[currentAttack].objects[i].rotationMode)
                    {
                        case RotationMode.prefab:
                            attack.transform.rotation = combo[currentAttack].objects[i].attackObject.gameObject.transform.rotation;
                            break;
                        case RotationMode.direction:
                            attack.transform.up = direction;
                            break;
                        case RotationMode.offset:
                            if (combo[currentAttack].objects[i].matchDirection)
                            {
                                Vector2 directionNormal = Vector3.Cross
                                (
                                    new Vector3(direction.x, direction.y, 0),
                                    new Vector3(0, 0, 1)
                                ).normalized;

                                Vector3 direction3 = new Vector3(direction.x, direction.y, 0);
                                Vector3 directionNormal3 = new Vector3(directionNormal.x, directionNormal.y, 0);
                                attack.transform.up = direction3 * combo[currentAttack].objects[i].spawnOffset.y + directionNormal3 * combo[currentAttack].objects[i].spawnOffset.x;
                            }
                            else
                                attack.transform.up = combo[currentAttack].objects[i].spawnOffset;

                            break;
                    }

                    AttackObject attackScript = attack.GetComponent<AttackObject>();

                    attackScript.direction = direction;
                    attackScript.attackPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    attackScript.damage = combo[currentAttack].damage;
                    attackScript.knockback = combo[currentAttack].knockback;
                }

                rb.AddForce(combo[currentAttack].force * direction);

                currentAttack++;
                if (currentAttack >= combo.Length)
                    currentAttack = 0;
            }
        }
    }
}
