using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TankController : MonoBehaviour
{
    private TankStats stats = null;
    private Vector2 moveInput = Vector2.zero;

    #region Init Functions
    private void Awake()
    {
        stats = GetComponent<TankStats>();
    }
    private void Start()
    {
        moveInput = Vector2.zero;
    }
    #endregion

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        Rotate(delta);

        if (stats.boostActive)
        {
            Boost(delta);
        }
        else
        {
            Accelerate(delta);
        }

        Move(delta);

        TickBoost(delta);

        CheckForDecays();

        return;
    }

    #region Movement
    private void Rotate(float delta)
    {
        stats.rotationSpeed += moveInput.x * stats.rotationForce * delta;

        stats.rotationSpeed = Mathf.Clamp(stats.rotationSpeed, -stats.maxRotationSpeed, stats.maxRotationSpeed);

        stats.bodyRotation += stats.rotationSpeed * delta;

        if (moveInput.x == 0)
        {
            stats.rotationSpeed *= stats.rotationSpeedDecay;

            if(Mathf.Abs(stats.rotationSpeed) <= stats.rotationSpeedZeroPoint)
            {
                stats.rotationSpeed = 0;
            }
        }

        if(stats.bodyRotation < 0)
        {
            stats.bodyRotation += 360;
        }
        else if(stats.bodyRotation > 360)
        {
            stats.bodyRotation = stats.bodyRotation % 360;
        }
    }
    private void Accelerate(float delta)
    {
        if(moveInput.y > 0 && stats.forwardsMoveSpeed < stats.maxForwardsMoveSpeed)
        {
            stats.forwardsMoveSpeed += moveInput.y * stats.moveForceForwards * delta;
        }
        else if(moveInput.y < 0 && stats.forwardsMoveSpeed > stats.maxBackwardsMoveSpeed)
        {
            stats.forwardsMoveSpeed += moveInput.y * stats.moveForceBackwards * delta;
        }

        stats.forwardsMoveSpeed = Mathf.Clamp(stats.forwardsMoveSpeed, stats.maxBackwardsMoveSpeed * stats.boostMaxSpeedMultiplier, stats.maxForwardsMoveSpeed * stats.boostMaxSpeedMultiplier);
    }
    private void Boost(float delta)
    {
        stats.forwardsMoveSpeed += stats.moveForceForwards * stats.boostForceMultiplier * delta;

        stats.forwardsMoveSpeed = Mathf.Min(stats.forwardsMoveSpeed, stats.maxForwardsMoveSpeed * stats.boostMaxSpeedMultiplier); 
    }
    private void TickBoost(float delta)
    {
        if (stats.boostActive)
        {
            stats.boostMeter -= stats.boostMeterUseRate * delta;

            stats.boostMeter = Mathf.Max(stats.boostMeter, 0.0f);

            if (stats.boostMeter <= stats.bosstMeterCancelPoint)
            {
                stats.boostActive = false;
            }
            return;
        }

        stats.boostMeter += stats.boostMeterRegenRate * delta;

        stats.boostMeter = Mathf.Min(stats.boostMeter, stats.boostMeterMaxValue);
    }
    private void Move(float delta)
    {
        Vector3 forwards = new Vector3(Mathf.Cos(stats.bodyRotation * Mathf.Deg2Rad), -Mathf.Sin(stats.bodyRotation * Mathf.Deg2Rad), 0.0f).normalized;

        transform.position += forwards * stats.forwardsMoveSpeed * delta;
    }
    private void CheckForDecays()
    {
        if(moveInput.y == 0)
        {
            ApplyMoveDecay();
            return;
        }

        if(!stats.boostActive)
        {
            if (stats.forwardsMoveSpeed > stats.maxForwardsMoveSpeed || stats.forwardsMoveSpeed < stats.maxBackwardsMoveSpeed)
            {
                ApplyMoveDecay();
                return;
            }
        }
    }
    private void ApplyMoveDecay()
    {
        stats.forwardsMoveSpeed *= stats.moveSpeedDecay;
        if (Mathf.Abs(stats.forwardsMoveSpeed) <= stats.moveSpeedZeroPoint)
        {
            stats.forwardsMoveSpeed = 0.0f;
        }
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
            if (stats.boostMeter > stats.boostMinRequired)
            {
                stats.boostActive = true;
            }

            return;
        }

        if(context.canceled)
        {
            stats.boostActive = false;
        }

    }
    #endregion
}
