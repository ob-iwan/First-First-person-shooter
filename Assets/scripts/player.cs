using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class player : MonoBehaviour
{
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
    private Vector3 scaleChange = new Vector3(0f, 0.5f, 0f);

    private float activeX;
    private float activeY;
    private float activeZ;

    public bool crouch = false;

    public CharacterController characterController;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkingSpeed;
        baseLineGravity = gravity;
    }

    //Update is called once per frame
    void Update()
    {
        activeX = transform.position.x;
        activeY = transform.position.y;
        activeZ = transform.position.z;

        moveX = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
        moveZ = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;

        move = transform.right * moveX +
               transform.up * gravity +
               transform.forward * moveZ;

        characterController.Move(move);

        if (Input.GetKey(KeyCode.LeftShift) && !crouch)
        {
            currentSpeed = runningSpeed;
        }
        else if (!crouch)
        {
            currentSpeed = walkingSpeed;
        }

        if (characterController.isGrounded && Input.GetButton("Jump"))
        {
            gravity = baseLineGravity;
            gravity *= -jumpSpeed;
        }
        if (gravity > baseLineGravity)
        {
            gravity -= 1 * Time.deltaTime;
        }

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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("cannonBall"))
    //    { 
    //        Destroy(gameObject.);
    //    }
    //}
}
