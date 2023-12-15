using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class camMenu : MonoBehaviour
{
    [SerializeField] Camera linkedCamera;

    [SerializeField] AudioListener audioListener;
    [SerializeField] Transform pivotPoint;
    [SerializeField] float defaultPitch = 20f;
    [SerializeField] float angleSwept = 60f;
    [SerializeField] float sweepSpeed = 8f;
    [SerializeField] float maxRotationSpeed = 15f;

    [SerializeField] SphereCollider detectionTrigger;
    [SerializeField] Light detectionLight;

    float currentAngle = 0f;
    bool sweepClockwise = true;

    // Start is called before the first frame update
    void Start()
    {
        //turn cam off by default
        linkedCamera.enabled = false;
        audioListener.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion desiredRotation = pivotPoint.rotation;

            //update angle
            currentAngle += sweepSpeed * Time.deltaTime * (sweepClockwise ? 1f : -1f);
            if (Mathf.Abs(currentAngle) >= (angleSwept * 0.5f))
            {
                sweepClockwise = !sweepClockwise;
            }

            //calculate the rotation
            desiredRotation = pivotPoint.transform.parent.rotation * Quaternion.Euler(defaultPitch, currentAngle, 0f);

        pivotPoint.transform.rotation = Quaternion.RotateTowards(pivotPoint.rotation,
                                                                 desiredRotation,
                                                                 maxRotationSpeed * Time.deltaTime);
    }
}
