using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField]
    private float rotation = 0.0f;
    private float rotationSpeed = 0.0f;


    [SerializeField]
    private float rotationAccel = 10.0f;
    private float rotationFriction = 0.9f;



    public Vector2 moveInput = Vector2.zero;

    [Header("Visuals")]
    public TankVisuals visuals;

    private void Awake()
    {
        visuals = GetComponent<TankVisuals>();

        visuals?.SetBodyRotation(rotation);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Rotate();

        Move();
    }

    private void Rotate()
    {
        rotationSpeed += rotationAccel * Time.fixedDeltaTime * moveInput.x;

        rotation += rotationSpeed * Time.fixedDeltaTime;

        rotationSpeed *= rotationFriction;

        if (rotation > 360)
        {
            rotation = rotation % 360;
        }
        else if (rotation < 0)
        {
            rotation += 360;
        }

        visuals?.SetBodyRotation(rotation);
    }
    private void Move()
    {

    }
}
