using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeThrow : MonoBehaviour
{
    public Camera cam;
    public ParticleSystem boom;

    private Ray ray;
    private RaycastHit hit;

    private bool knifeReload = false;
    public float reloadTime = 5f;
    private float timeReset;

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
                if (hit.collider.tag.Equals("killable"))
                {
                    boom.Play();
                    Destroy(hit.collider.gameObject);
                    knifeReload = true;
                }
            }
        }
        reloadTime -= Time.deltaTime;
        if (reloadTime < 0)
        {
            Debug.Log("reloaded");
            reloadTime = timeReset;
            knifeReload = false;
        }
    }
}