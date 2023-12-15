using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public sceneSystem scene;
    private float currentSpeed;
    public float walkingSpeed = 10f;
    public float runningSpeed = 17f;
    public float crouchingSpeed = 5f;
    public float gravity = -0.5f;
    public float jumpSpeed = 0.8f;
    private float baseLineGravity;

    private float moveX;
    private float moveZ;

    private Vector3 move;
    private Vector3 scaleChange = new Vector3(0f, 1f, 0f);

    private float activeX;
    private float activeY;
    private float activeZ;

    public bool crouch = false;
    public bool running = false;

    public int score = 0;

    public TextMeshProUGUI scoreBoard;

    public CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = walkingSpeed;
        baseLineGravity = gravity;
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            Cursor.lockState = CursorLockMode.None;
            scene.loadMenu();
        }

        activeX = transform.position.x;
        activeY = transform.position.y;
        activeZ = transform.position.z;

        //movement
        moveX = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
        moveZ = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;

        move = transform.right * moveX +
               transform.up * gravity +
               transform.forward * moveZ;

        characterController.Move(move);

        //sprinting
        if (Input.GetKey(KeyCode.LeftShift) && !crouch)
        {
            currentSpeed = runningSpeed;
            running = true;
        }
        else if (!crouch)
        {
            currentSpeed = walkingSpeed;
            running = false;
        }

        //jumping
        if (characterController.isGrounded && Input.GetButton("Jump"))
        {
            gravity = baseLineGravity;
            gravity *= -jumpSpeed;
        }
        if (gravity > baseLineGravity)
        {
            gravity -= 1 * Time.deltaTime;
        }

        //crouching
        if (Input.GetKeyDown(KeyCode.C) && !crouch)
        {
            currentSpeed = crouchingSpeed;
            transform.localScale -= scaleChange;
            crouch = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && crouch)
        {
            currentSpeed = walkingSpeed;
            transform.localScale += scaleChange;
            crouch = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("file"))
        {
            score++;
            scoreBoard.text = $"{score}/7 files collected";
            Destroy(other.gameObject);
        }
    }
}
