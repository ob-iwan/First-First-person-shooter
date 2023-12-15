using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class securityCam : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] string _displayName;
    [SerializeField] Camera linkedCamera;

    [SerializeField] bool syncToMainCameraConfig = true;

    [SerializeField] AudioListener audioListener;
    [SerializeField] Transform pivotPoint;
    [SerializeField] float defaultPitch = 20f;
    [SerializeField] float angleSwept = 60f;
    [SerializeField] float sweepSpeed = 8f;
    [SerializeField] int outputTextureSize = 256;
    [SerializeField] float targetVOffset = 1f;
    [SerializeField] float maxRotationSpeed = 15f;

    [Header("Detection")]
    [SerializeField] float detectionHalfAngle = 30f;
    [SerializeField] float detectionrange = 20f;
    [SerializeField] SphereCollider detectionTrigger;
    [SerializeField] Light detectionLight;
    [SerializeField] Color colour_NothingDetected = Color.green;
    [SerializeField] Color colour_FullyDetected = Color.red;
    [SerializeField] float detectionBuildRate = 0.5f;
    [SerializeField] float detectionDecayRate = 0.5f;
    [SerializeField] [Range (0f, 1f)] float suspisionThreshHold = 0.5f;
    [SerializeField] List<string> detectableTags;
    [SerializeField] LayerMask detectionLayerMask = ~0;

    [SerializeField] UnityEvent<GameObject> onDetected = new UnityEvent<GameObject>();
    [SerializeField] UnityEvent onAllClear = new UnityEvent();

    Dictionary<GameObject, potentialTarget> allTargets = new Dictionary<GameObject, potentialTarget>();

    private GameObject player;
    private GameObject[] respawns;

    private enemyAttack enemyAttackScript;

    public RenderTexture outputTexture { get; private set; }
    public string displayName => _displayName;
    float cosDetectionHalfAngle;
    public GameObject currenlyDetectedTarget { get; private set; }

    public bool HasDetectedTarget { get; private set; } = false;

    float currentAngle = 0f;
    bool sweepClockwise = true;

    public bool playerSpotted = false;
    public bool enemyCheck = false;

    List<securityConsole> currenlyWatchingConsoles = new List<securityConsole>();

    // Start is called before the first frame update
    void Start()
    {
        //turn cam off by default
        linkedCamera.enabled = false;
        audioListener.enabled = false;


        //setup collider and light
        detectionLight.color = colour_NothingDetected;
        detectionLight.range = detectionrange;
        detectionLight.spotAngle = detectionHalfAngle * 2f;
        detectionTrigger.radius = detectionrange;

        //cache the detection data
        cosDetectionHalfAngle = Mathf.Cos(Mathf.Deg2Rad * detectionHalfAngle);

        if (syncToMainCameraConfig)
        {
            linkedCamera.clearFlags = Camera.main.clearFlags;
            linkedCamera.backgroundColor = Camera.main.backgroundColor;
        }

        //setup render texture
        outputTexture = new RenderTexture(outputTextureSize, outputTextureSize, 32);
        linkedCamera.targetTexture = outputTexture;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCheck == false)
        {
            enemyAttackScript = GameObject.FindGameObjectWithTag("enemy").GetComponent<enemyAttack>();
            player = GameObject.Find("Player");
            enemyCheck = true;
        }

        refreshTargetInfo();
        Quaternion desiredRotation = pivotPoint.rotation;

        //freeze cam position if detectionLevel is past suspisionThreshHold

        if (currenlyDetectedTarget != null && allTargets[currenlyDetectedTarget].detecionLevel >= suspisionThreshHold)
        {
            if (allTargets[currenlyDetectedTarget].inFOV)
            {
                var vecToTarget = (currenlyDetectedTarget.transform.position + targetVOffset * Vector3.up -
                                   pivotPoint.transform.position).normalized;

                desiredRotation = Quaternion.LookRotation(vecToTarget, Vector3.up) * Quaternion.Euler(0f, 0f, 0f);
            }
        }
        else
        {
            //update angle
            currentAngle += sweepSpeed * Time.deltaTime * (sweepClockwise ? 1f : -1f);
            if (Mathf.Abs(currentAngle) >= (angleSwept * 0.5f))
            {
                sweepClockwise = !sweepClockwise;
            }

            //calculate the rotation
            desiredRotation = pivotPoint.transform.parent.rotation * Quaternion.Euler(defaultPitch, currentAngle, 0f);
        }

        pivotPoint.transform.rotation = Quaternion.RotateTowards(pivotPoint.rotation,
                                                                 desiredRotation,
                                                                 maxRotationSpeed * Time.deltaTime);
    }
    
    //detection
    void refreshTargetInfo()
    {
        float highestDetectionLevel = 0f;
        currenlyDetectedTarget = null;

        //refresh each target
        foreach (var target in allTargets)
        {
            var targetInfo = target.Value;

            bool isVisible = false;

            //is the target in the field of view
            Vector3 vecToTarget = (targetInfo.linkedGO.transform.position + targetVOffset * Vector3.up - linkedCamera.transform.position).normalized;
            if (Vector3.Dot(linkedCamera.transform.forward, vecToTarget.normalized) >= cosDetectionHalfAngle)
            {
                //check if we can see the target
                RaycastHit hitInfo;
                if (Physics.Raycast(linkedCamera.transform.position, vecToTarget,
                    out hitInfo, detectionrange, detectionLayerMask, QueryTriggerInteraction.Ignore))
                {
                    if (hitInfo.collider.gameObject == targetInfo.linkedGO)
                        isVisible = true;
                }
            }

            //update the detection level
            targetInfo.inFOV = isVisible;
            if (isVisible)
            {
                targetInfo.detecionLevel = Mathf.Clamp01(targetInfo.detecionLevel + detectionBuildRate * Time.deltaTime);

                //notify that the target was seen
                if (targetInfo.detecionLevel >= 1f && !targetInfo.onDetectedEventSent)
                {
                    HasDetectedTarget = true;
                    targetInfo.onDetectedEventSent = true;
                    onDetected.Invoke(targetInfo.linkedGO);
                }  
            }
            else
            {
                targetInfo.detecionLevel = Mathf.Clamp01(targetInfo.detecionLevel - detectionDecayRate * Time.deltaTime);
            }

            //found a new more detected target?
            if (targetInfo.detecionLevel > highestDetectionLevel)
            {
                highestDetectionLevel = targetInfo.detecionLevel;
                currenlyDetectedTarget = targetInfo.linkedGO;
            }
        }

        //update the light color
        if (currenlyDetectedTarget != null)
        {
            detectionLight.color = Color.Lerp(colour_NothingDetected, colour_FullyDetected, highestDetectionLevel);
        }
        else
        {
            detectionLight.color = colour_NothingDetected;

            if (HasDetectedTarget)
            {
                HasDetectedTarget = false;
                onAllClear.Invoke();
            }
        }
    }

    class potentialTarget
    {
        public GameObject linkedGO;
        public bool inFOV;
        public float detecionLevel;
        public bool onDetectedEventSent;
    }


    private void OnTriggerEnter(Collider other)
    {
        //skip if the tag isn't supported
        if (!detectableTags.Contains(other.tag))
            return;

        //add to our target list
        allTargets[other.gameObject] = new potentialTarget() { linkedGO  = other.gameObject };
    }

    private void OnTriggerExit(Collider other)
    {
        //skip if the tag isn't supported
        if (!detectableTags.Contains(other.tag))
            return;

        //remove from the target list
        allTargets.Remove(other.gameObject);
    }
    public void startWatching(securityConsole linkedConsole)
    {
        if (!currenlyWatchingConsoles.Contains(linkedConsole))
            currenlyWatchingConsoles.Add(linkedConsole);

        onWatchersChanged();
    }

    public void stopWatching(securityConsole linkedConsole)
    {
        currenlyWatchingConsoles.Remove(linkedConsole);

        onWatchersChanged();
    }

    void onWatchersChanged()
    {
        linkedCamera.enabled = currenlyWatchingConsoles.Count > 0;
    }
}
