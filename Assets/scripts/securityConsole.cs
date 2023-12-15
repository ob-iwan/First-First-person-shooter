using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class securityConsole : MonoBehaviour
{
    [SerializeField] List<securityCam> securityCameras;
    [SerializeField] RawImage cameraImage;
    [SerializeField] TextMeshProUGUI activeCameraLabel;

    private bool enemyCheck = false;

    private GameObject player;
    private GameObject[] respawns;

    private enemyAttack enemyAttackScript;
    public int activeCamIndex { get; private set; } = -1;
    public securityCam activeCamera => activeCamIndex < 0 ? null : securityCameras[activeCamIndex];

    public bool detected = false;

    // Start is called before the first frame update
    void Start()
    {
        activeCameraLabel.text = "Camere: none";
    }

    void Update()
    {
        if (enemyCheck == false)
        {
            enemyAttackScript = GameObject.FindGameObjectWithTag("enemy").GetComponent<enemyAttack>();
            player = GameObject.Find("Player");
            enemyCheck = true;
        }
    }

    public void onClicked()
    {
        selectNextCamera();
    }

    void selectNextCamera()
    {
        //switch the next cam
        var previousCamera = activeCamera;
        activeCamIndex = (activeCamIndex + 1) % securityCameras.Count;

        //tell the previous cam to stop watching
        if (previousCamera != null)
            previousCamera.stopWatching(this);

        //tell the new cam to start watching
        //and to change cam name to new one
        activeCameraLabel.text = $"camera: {activeCamera.displayName}";
        activeCamera.startWatching(this);
        cameraImage.texture = activeCamera.outputTexture;
    }

    public void onDetected(GameObject target)
    {
        detected = true;
    }

    public void onAllClear()
    {
        detected = false;
    }
}
