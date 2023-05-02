using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TankController : MonoBehaviour
{
    [Header("Visuals")]
    public TankVisuals visuals = null;

    [Header("Rotation")]
    public float rotation = 0.0f;
    public float rotationForce = 50.0f;
    public float rotationSpeed = 0.0f;
    public float MaxRotateSpeed = 200.0f;
    public float rotationDecay = 0.9f;

    [Header("Movement")]
    public float moveSpeed = 0.0f;

    public float forwardsMoveForce = 10.0f;
    public float backwardsMoveForce = 5.0f;

    public float maxForwardsMoveSpeed = 1.0f;
    public float maxBackwardsMoveSpeed = -0.5f;

    public float moveSpeedDecay = 0.9f;

    
    private Vector2 moveInput = Vector2.zero;

    [Header("Boost")]
    public float boostMaxSpeedMultiplier = 2.0f;
    public float boostForceMultiplier = 2.0f;

    public float boostMeter = 100.0f;
    public float boostUseRate = 10.0f;

    public bool boostActive = false;

    #region Init Functions
    private void Awake()
    {
        visuals = GetComponent<TankVisuals>();

        if(visuals == null)
        {
            Debug.LogError("ERROR - Could not locate tank visuals");
        }
    }
    private void Start()
    {
        moveInput = Vector2.zero;

        moveSpeed = 0.0f;
        rotationSpeed = 0.0f;
    }
    #endregion

    #region Movement
    private void FixedUpdate()
    {
        Rotate();

        if (boostActive)
        {
            Boost();
        }
        else
        {
            Accelerate();
        }

        Move();

        return;
    }
    private void Rotate()
    {
        rotationSpeed += moveInput.x * rotationForce * Time.fixedDeltaTime;

        rotationSpeed = Mathf.Clamp(rotationSpeed, -MaxRotateSpeed, MaxRotateSpeed);

        rotation += rotationSpeed * Time.fixedDeltaTime;

        if (moveInput.x == 0)
        {
            rotationSpeed *= rotationDecay;
        }

        if(rotation < 0)
        {
            rotation += 360;
        }
        else if(rotation > 360)
        {
            rotation = rotation % 360;
        }

        visuals.SetBodyRotation(rotation);
    }
    private void Accelerate()
    {
        if(moveInput.y > 0)
        {
            moveSpeed += moveInput.y * forwardsMoveForce * Time.fixedDeltaTime;
        }
        else if(moveInput.y < 0)
        {
            moveSpeed += moveInput.y * backwardsMoveForce * Time.fixedDeltaTime;
        }

        moveSpeed = Mathf.Clamp(moveSpeed, maxBackwardsMoveSpeed, maxForwardsMoveSpeed);
    }
    private void Boost()
    {
        moveSpeed += forwardsMoveForce * boostForceMultiplier * Time.fixedDeltaTime;

        moveSpeed = Mathf.Min(moveSpeed, maxForwardsMoveSpeed * boostMaxSpeedMultiplier); 
    }
    private void Move()
    {
        Vector3 forwards = new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad), -Mathf.Sin(rotation * Mathf.Deg2Rad), 0.0f).normalized;

        transform.position += forwards * moveSpeed * Time.fixedDeltaTime;

        moveSpeed *= moveSpeedDecay;
    }
    #endregion

    #region Event Recievers
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnBoostInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            boostActive = true;

            return;
        }

        if(context.canceled)
        {
            boostActive = false;
        }

    }
    #endregion
}
