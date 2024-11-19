using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMover : MonoBehaviour
{
    //Borrowing code from another final project I'm working on to add player movement

    //Serialized Fields
    [Header("Movemenet Attributes")]

    [SerializeField]
    float fltGroundVelocity;

    [SerializeField]
    InputAction movementInput;

    [SerializeField]
    [Min(0)]
    float maxVelocity = 10f;

    //Cashe References
    Rigidbody playerRb;
    Camera mainCam;
    BoxCollider playerCollider;

    void Awake()
    {
        InitializeReferences();
    }

    private void InitializeReferences()
    {
        playerRb = GetComponent<Rigidbody>();
        if (playerRb == null)
        {
            Debug.Log("No Rigidbody found");
        }
        else
        {
            Debug.Log("Player Rigidbody found");
        }

        mainCam = FindObjectOfType<Camera>();
        if (mainCam == null)
        {
            Debug.Log("No camera found");
        }
        else
        {
            Debug.Log("Camera found");
        }

        playerCollider = GetComponentInChildren<BoxCollider>();
        if (playerCollider == null)
        {
            Debug.Log("No capsule collider found in children");
        }
        else
        {
            Debug.Log("Capsule collider found in children");
        }
    }

    void OnEnable()
    {
        movementInput.Enable();
    }

    void OnDisable()
    {
        movementInput.Disable();
    }

    void Update()
    {
        PlayerMoves();
    }
    void PlayerMoves()
    {
        //Do not run if the movement input is deactivated
        if (movementInput.enabled == false)
        {
            return;
        }

        //if (!isGrounded)
        //{
        //    return;
        //}

        //Read the input values of the movement input
        Vector2 inputDir = movementInput.ReadValue<Vector2>();
        Debug.Log($"Movement direction: {inputDir}");

        //Direction of movement is relative to the cinemachine's forward direction.
        //Thus, moving the player "forward" is relative to the cinemachine's forward direction

        //Get the main camera's forwards vector
        Vector2 forwardDir = new Vector2(
            mainCam.transform.forward.x,
            mainCam.transform.forward.z
            ).normalized;
        Vector2 rightDir = new Vector2(
            mainCam.transform.right.x,
            mainCam.transform.right.z
            ).normalized;

        //Debug.Log($"Camera's forward direction = {forwardDir}");
        //Debug.Log($"Camera's right direction = {rightDir}");

        //Logic: A/D moves left/right of the left direction.
        //       W/S moves up/down of the forward direction.

        //Vector direction to move
        Vector3 moveDir = new Vector3(0, 0, 0);

        if (Mathf.Abs(inputDir.x) > Mathf.Epsilon)
        {
            //Add rightDir to the moveDir
            moveDir += new Vector3(rightDir.x * inputDir.x, 0, rightDir.y * inputDir.x) * fltGroundVelocity;
        }
        if (Mathf.Abs(inputDir.y) > Mathf.Epsilon)
        {
            moveDir += new Vector3(forwardDir.x * inputDir.y, 0, forwardDir.y * inputDir.y) * fltGroundVelocity;
        }

        //moveDir.y = playerRb.velocity.y;
        Debug.Log($"Move Direction: {moveDir}");

        playerRb.AddForce(moveDir);
        Debug.Log(playerRb.velocity);

        //Clamp the min & max velocity for x and z velocity if on the ground

        playerRb.velocity = new Vector3(
            Mathf.Clamp(playerRb.velocity.x, -maxVelocity, maxVelocity),
            playerRb.velocity.y,
            Mathf.Clamp(playerRb.velocity.z, -maxVelocity, maxVelocity)
            );

        //Debug.Log($"Player velocity: {playerRb.velocity}");
        Debug.DrawRay(transform.position, playerRb.velocity);
    }

}
