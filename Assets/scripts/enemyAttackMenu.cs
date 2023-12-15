using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackMenu : MonoBehaviour
{
    private PlayerMovement playerCode;
    public enemyMovement movement;
    private Transform player;
    public Material defaultMat;
    public Material angrMat;
    public Renderer rend;
    private securityConsole console;

    public float attackRange = 10f;
    public float attackRangeCrouch = 4f;
    public float attackRangeRunning = 14f;
    public float calmSpeed = 2f;
    public float angrySpeed = 5f;

    public bool attacking;

    private void Awake()
    {
        console = GameObject.FindGameObjectWithTag("console").GetComponent<securityConsole>();

        movement = GameObject.FindGameObjectWithTag("enemy").GetComponent<enemyMovement>();
        
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
            //player in range or detected by cam
            if (Vector3.Distance(transform.position, player.position) <= attackRange || console.detected)
            {
                rend.sharedMaterial = angrMat;
                movement.monster.SetDestination(player.position);
                movement.monster.speed = angrySpeed;
                attacking = true;
            }
            //nothing in range and not detected by cam
            else if (attacking && !console.detected)
            {
                rend.sharedMaterial = defaultMat;
                movement.monster.speed = calmSpeed;
                movement.monsterMove();
                attacking = false;
            }
        }


        //if player is crouching enemy attack range is less
        else if (playerCode.crouch && !playerCode.running)
        {
            //player in range or detected by cam
            if (Vector3.Distance(transform.position, player.position) <= attackRangeCrouch || console.detected)
            {
                rend.sharedMaterial = angrMat;
                movement.monster.SetDestination(player.position);
                movement.monster.speed = angrySpeed;
                attacking = true;
            }

            //nothing in range and not detected by cam
            else if (attacking && !console.detected)
            {
                rend.sharedMaterial = defaultMat;
                movement.monster.speed = calmSpeed;
                movement.monsterMove();
                attacking = false;
            }
        }

        else if (playerCode.running && !playerCode.crouch)
        {
            //player in range or detected by cam
            if (Vector3.Distance(transform.position, player.position) <= attackRangeRunning || console.detected)
            {
                rend.sharedMaterial = angrMat;
                movement.monster.SetDestination(player.position);
                movement.monster.speed = angrySpeed;
                attacking = true;
            }

            //nothing in range and not detected by cam
            else if (attacking && !console.detected)
            {
                rend.sharedMaterial = defaultMat;
                movement.monster.speed = calmSpeed;
                movement.monsterMove();
                attacking = false;
            }
        }
    }
}
