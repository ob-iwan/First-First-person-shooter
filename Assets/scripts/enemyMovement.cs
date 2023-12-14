using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class enemyMovement : MonoBehaviour
{
    public NavMeshAgent monster;

    private enemySpawn spawner;
    private Transform enemyPos;

    private float posX;
    private float posY;
    private float posZ;

    private float beginPosX;
    private float beginPosY;
    private float beginPosZ;

    public float squareOMovement = 40;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    public float close = 2f;

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("spawner").GetComponent<enemySpawn>();
        enemyPos = GameObject.FindGameObjectWithTag("enemy").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        posY = transform.position.y;

        minX = -squareOMovement;
        maxX = squareOMovement;
        minZ = -squareOMovement;
        maxZ = squareOMovement;

        monsterMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, new Vector3 (posX, posY, posZ)) <= close)
        {
            monsterMove();
        }
    }

    public void monsterMove()
    {
        posX = Random.Range(minX, maxX);
        posZ = Random.Range(minZ, maxZ);
        monster.SetDestination(new Vector3(posX, posY, posZ));
    }
}
