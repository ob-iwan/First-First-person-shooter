using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeThrow : MonoBehaviour
{
    public melee melee;

    public Camera cam;

    private Ray ray;
    private RaycastHit hit;

    private bool knifeReload = false;
    public float reloadTime = 5f;
    private float timeReset;

    public int knifeDamage = 3;

    void Start()
    {
        timeReset = reloadTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !knifeReload)
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("enemy"))
                {
                    melee.hitEnemy += knifeDamage;
                    if (melee.hitEnemy > melee.hitAmountEnemy)
                    {
                        Destroy(hit.collider.gameObject);
                        knifeReload = true;
                        melee.hitEnemy = 0;
                    }
                }
                if (hit.collider.CompareTag("killAbleWall"))
                {
                    melee.hitWall += knifeDamage;
                    if (melee.hitWall > melee.hitAmountWall)
                    {
                        Destroy(hit.collider.gameObject);
                        knifeReload = true;
                        melee.hitWall = 0;
                    }
                }
                if (hit.collider.CompareTag("killable"))
                {
                    melee.Hit += knifeDamage;
                    if (melee.Hit > melee.hitAmount)
                    {
                        Destroy(hit.collider.gameObject);
                        knifeReload = true;
                        melee.Hit = 0;
                    }
                }
            }
        }
        if (knifeReload)
        {
            reloadTime -= Time.deltaTime;
        }
        if (reloadTime < 0)
        {
            Debug.Log("reloaded");
            reloadTime = timeReset;
            knifeReload = false;
        }
    }
}