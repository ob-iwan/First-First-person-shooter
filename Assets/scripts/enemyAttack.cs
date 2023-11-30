using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    private player playerCode;
    private enemyMovement movement;
    private Transform player;
    public Material defaultMat;
    public Material angrMat;
    private Renderer rend;

    public float attackRange = 10f;
    public float attackRangeCrouch = 4f;
    public float calmSpeed = 2f;
    public float angrySpeed = 5f;

    private bool attacking;

    private void Awake()
    {
        movement = GetComponent<enemyMovement>();
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCode = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerCode.crouch)
        {
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                rend.sharedMaterial = angrMat;
                movement.monster.SetDestination(player.position);
                movement.monster.speed = angrySpeed;
                attacking = true;
            }
            else if (attacking)
            {
                rend.sharedMaterial = defaultMat;
                movement.monsterMove();
                movement.monster.speed = calmSpeed;
                attacking = false;
            }
        }

        else if (playerCode.crouch)
        {
            if (Vector3.Distance(transform.position, player.position) <= attackRangeCrouch)
            {
                rend.sharedMaterial = angrMat;
                movement.monster.SetDestination(player.position);
                movement.monster.speed = angrySpeed;
                attacking = true;
            }
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
