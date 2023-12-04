using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class securityCam : MonoBehaviour
{
    [SerializeField] string _displayName;
    [SerializeField] Camera linkedCamera;

    [SerializeField] bool syncToMainCameraConfig = true;

    [SerializeField] AudioListener audioListener;
    [SerializeField] Transform pivotPoint;
    [SerializeField] float defaultPitch = 20f;
    [SerializeField] float angleSwept = 60f;
    [SerializeField] float sweepSpeed = 8f;
    [SerializeField] int outputTextureSize = 256;

    public RenderTexture outputTexture { get; private set; }
    public string displayName => _displayName;

    float currentAngle = 0f;
    bool sweepClockwise = true;

    List<securityConsole> currenlyWatchingConsoles = new List<securityConsole>();


    // Start is called before the first frame update
    void Start()
    {
        linkedCamera.enabled = false;
        audioListener.enabled = false;

        if (syncToMainCameraConfig)
        {
            linkedCamera.clearFlags = Camera.main.clearFlags;
            linkedCamera.backgroundColor = Camera.main.backgroundColor;
        }

        outputTexture = new RenderTexture(outputTextureSize, outputTextureSize, 32);
        linkedCamera.targetTexture = outputTexture;
    }

    // Update is called once per frame
    void Update()
    {
        //update angle
        currentAngle += sweepSpeed * Time.deltaTime * (sweepClockwise ? 1f : 1f);
        if (Mathf.Abs(currentAngle) >= (angleSwept * 0.5f))
            sweepClockwise = !sweepClockwise;
        //rotate camera
        pivotPoint.transform.localEulerAngles = new Vector3(defaultPitch, currentAngle, 0f);
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
