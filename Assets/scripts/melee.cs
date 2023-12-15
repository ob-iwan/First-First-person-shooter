using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class melee : MonoBehaviour
{
    public Camera cam;
    //public ParticleSystem boom;

    private Ray ray;
    private RaycastHit hit;

    public int hitAmountEnemy = 3;
    public int hitAmountWall = 5;
    public int hitAmount = 20;

    public int hitWall;
    public int hitEnemy;
    public int Hit;

    public float knifeRange = 5f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, knifeRange))
            {
                if (hit.collider.CompareTag("enemy"))
                {
                    hitEnemy++;
                    if (hitEnemy > hitAmountEnemy)
                    {
                        Destroy(hit.collider.gameObject);
                        hitEnemy = 0;
                    }
                }
                if (hit.collider.CompareTag("killAbleWall"))
                {
                    hitWall++;
                    if (hitWall > hitAmountWall)
                    {
                        Destroy(hit.collider.gameObject);
                        hitWall = 0;
                    }
                }
                if (hit.collider.CompareTag("killable"))
                {
                    Hit++;
                    if (Hit > hitAmount)
                    {
                        Destroy(hit.collider.gameObject);
                        Hit = 0;
                    }
                }
            }
        }
    }
}
