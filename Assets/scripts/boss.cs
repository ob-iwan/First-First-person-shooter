using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public PlayerMovement playerCode;
    public Transform player;
    public bossMovement movement;

    public float attackRange = 10f;
    public float attackRangeCrouch = 4f;
    public float attackRangeRunning = 14f;
    public float calmSpeed = 2f;
    public float angrySpeed = 5f;

    public bool attacking;

    // Update is called once per frame
    void Update()
    {
        //if player is not crouching and not running enemy attack range is normal
        if (!playerCode.crouch && !playerCode.running)
        {
            //player in range or detected by cam
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                movement.monster.SetDestination(player.position);
                attacking = true;
            }
            //nothing in range and not detected by cam
            else if (attacking)
            {
                movement.monster.speed = calmSpeed;
                movement.monster.SetDestination(transform.position);
                attacking = false;
            }
        }


        //if player is crouching enemy attack range is less
        else if (playerCode.crouch && !playerCode.running)
        {
            //player in range or detected by cam
            if (Vector3.Distance(transform.position, player.position) <= attackRangeCrouch)
            {
                movement.monster.SetDestination(player.position);
                movement.monster.speed = angrySpeed;
                attacking = true;
            }

            //nothing in range and not detected by cam
            else if (attacking)
            {
                movement.monster.speed = calmSpeed;
                movement.monster.SetDestination(transform.position);
                attacking = false;
            }
        }

        else if (playerCode.running && !playerCode.crouch)
        {
            //player in range or detected by cam
            if (Vector3.Distance(transform.position, player.position) <= attackRangeRunning)
            {
                movement.monster.SetDestination(player.position);
                movement.monster.speed = angrySpeed;
                attacking = true;
            }

            //nothing in range and not detected by cam
            else if (attacking)
            {
                movement.monster.speed = calmSpeed;
                movement.monster.SetDestination(transform.position);
                attacking = false;
            }
        }
    }
}
