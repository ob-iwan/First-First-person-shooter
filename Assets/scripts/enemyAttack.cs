using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    private PlayerMovement playerCode;
    public enemyMovement movement;
    private Transform player;
    public Material defaultMat;
    public Material angrMat;
    public Renderer rend;

    public float attackRange = 10f;
    public float attackRangeCrouch = 4f;
    public float attackRangeRunning = 14f;
    public float calmSpeed = 2f;
    public float angrySpeed = 5f;

    public bool attacking;

    private void Awake()
    {
        movement = GetComponent<enemyMovement>();
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCode = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player is not crouching and not running enemy attack range is normal
        if (!playerCode.crouch && !playerCode.running)
        {
            //player in range
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                rend.sharedMaterial = angrMat;
                movement.monster.SetDestination(player.position);
                movement.monster.speed = angrySpeed;
                attacking = true;
            }
            //nothing in range
            else if (attacking)
            {
                rend.sharedMaterial = defaultMat;
                movement.monsterMove();
                movement.monster.speed = calmSpeed;
                attacking = false;
            }
        }


        //if player is crouching enemy attack range is less
        else if (playerCode.crouch && !playerCode.running)
        {
            //player in range
            if (Vector3.Distance(transform.position, player.position) <= attackRangeCrouch)
            {
                rend.sharedMaterial = angrMat;
                movement.monster.SetDestination(player.position);
                movement.monster.speed = angrySpeed;
                attacking = true;
            }

            //nothing in range
            else if (attacking)
            {
                rend.sharedMaterial = defaultMat;
                movement.monsterMove();
                movement.monster.speed = calmSpeed;
                attacking = false;
            }
        }

        else if (playerCode.running && !playerCode.crouch)
        {
            //player in range
            if (Vector3.Distance(transform.position, player.position) <= attackRangeRunning)
            {
                rend.sharedMaterial = angrMat;
                movement.monster.SetDestination(player.position);
                movement.monster.speed = angrySpeed;
                attacking = true;
            }

            //nothing in range
            else if (attacking)
            {
                rend.sharedMaterial = defaultMat;
                movement.monsterMove();
                movement.monster.speed = calmSpeed;
                attacking = false;
            }
        }
    }
}
