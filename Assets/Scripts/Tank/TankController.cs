using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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

        RotateBody(delta, ref stats.angularSpeed, ref stats.bodyRotation);

        if (stats.boostActive)
        {
            Boost(delta, ref stats.moveSpeed);
        }
        else
        {
            Accelerate(delta, ref stats.moveSpeed);
        }

        Move(delta, stats.moveSpeed);

        TickBoost(delta, ref stats.boostMeter, ref stats.boostActive);

        CheckForDecays(ref stats.moveSpeed, ref stats.angularSpeed);

        FixAngles(ref stats.bodyRotation);

        return;
    }

    #region Movement
    private void RotateBody(float delta, ref float angularSpeed, ref float bodyRotation)
    {
        float angularAcceleration = stats.defualts.rotationStats.angularAcceleration.value;
        float maxAngularSpeed = stats.defualts.rotationStats.maxAngularSpeed.value;

        angularSpeed += moveInput.x * angularAcceleration * delta;

        angularSpeed = Mathf.Clamp(angularSpeed, -maxAngularSpeed, maxAngularSpeed);

        bodyRotation += angularSpeed * delta;
    }
    private void Accelerate(float delta, ref float moveSpeed)
    {
        float forwardsAcceleration = stats.defualts.movementStats.forwardsAcceleration.value;
        float backwardsAcceleration = stats.defualts.movementStats.backwardsAcceleration.value;

        float maxSpeedForwards = stats.defualts.movementStats.maxMoveSpeedForwards.value;
        float maxSpeedBackwards = stats.defualts.movementStats.maxMoveSpeedBackwards.value;

        if(moveInput.y > 0 && moveSpeed < maxSpeedForwards)
        {
            moveSpeed += forwardsAcceleration * delta;
        }
        else if(moveInput.y < 0 && moveSpeed > maxSpeedBackwards)
        {
            moveSpeed += moveInput.y * backwardsAcceleration * delta;
        }
    }
    private void Boost(float delta, ref float moveSpeed)
    {
        float forwardsAcceleration = stats.defualts.movementStats.forwardsAcceleration.value;
        float boostAccelerationMultiplier = stats.defualts.boostStats.accelerationMuliplier.value;

        float maxForwardsSpeed = stats.defualts.movementStats.maxMoveSpeedForwards.value;
        float boostMaxSpeedMultiplier = stats.defualts.boostStats.maxSpeedMultiplier.value;

        moveSpeed += forwardsAcceleration * boostAccelerationMultiplier * delta;

        moveSpeed = Mathf.Min(moveSpeed, maxForwardsSpeed * boostMaxSpeedMultiplier); 
    }
    private void TickBoost(float delta, ref float boostMeter, ref bool boostActive)
    {
        if (boostActive)
        {
            float useRate = stats.defualts.boostStats.meterUseRate.value;
            float cancelPoint = stats.defualts.boostStats.meterCancelPoint;

            boostMeter -= useRate * delta;

            boostMeter = Mathf.Max(boostMeter, 0.0f);

            if (stats.boostMeter <= cancelPoint)
            {
                boostActive = false;
            }
            return;
        }


        float regenRate = stats.defualts.boostStats.regenUseRate.value;
        float maxValue = stats.defualts.boostStats.meterMaxValue.value;

        boostMeter += regenRate * delta;

        boostMeter = Mathf.Min(boostMeter, maxValue);
    }
    private void Move(float delta, float moveSpeed)
    {
        Vector3 forwards = new Vector3(Mathf.Cos(stats.bodyRotation * Mathf.Deg2Rad), -Mathf.Sin(stats.bodyRotation * Mathf.Deg2Rad), 0.0f).normalized;

        transform.position += forwards * moveSpeed * delta;
    }
    private void CheckForDecays(ref float moveSpeed, ref float angularSpeed)
    {
        float maxForwardsMoveSpeed = stats.defualts.movementStats.maxMoveSpeedForwards.value;
        float maxBackwardsMoveSpeed = stats.defualts.movementStats.maxMoveSpeedBackwards.value;

        if(moveInput.y == 0)
        {
            ApplyMoveDecay(ref moveSpeed);
            return;
        }
        else if(!stats.boostActive)
        {
            if(moveSpeed > maxForwardsMoveSpeed || moveSpeed < maxBackwardsMoveSpeed)
            {
                ApplyMoveDecay(ref moveSpeed);
            }
        }

        if(moveInput.x == 0)
        {
            ApplyRotationDecay(ref angularSpeed);
        }
    }
    private void ApplyMoveDecay(ref float moveSpeed)
    {
        float moveSpeedDecay = stats.defualts.movementStats.moveSpeedDecay;
        float moveSpeedZeroPoint = stats.defualts.movementStats.moveSpeedZeroPoint;

        moveSpeed *= moveSpeedDecay;
        if (Mathf.Abs(moveSpeed) <= moveSpeedZeroPoint)
        {
            moveSpeed = 0.0f;
        }
    }
    private void ApplyRotationDecay(ref float angularSpeed)
    {
        float angularDecay = stats.defualts.rotationStats.speedDecayRate;
        float angularSpeedZeroPoint = stats.defualts.rotationStats.speedZeroPoint;

        angularSpeed *= angularDecay;

        if (Mathf.Abs(angularSpeed) <= angularSpeedZeroPoint)
        {
            angularSpeed = 0;
        }
    }
    private void FixAngles(ref float bodyRotation)
    {
        if (bodyRotation < 0)
        {
            bodyRotation += 360;
        }
        else if (bodyRotation > 360)
        {
            bodyRotation %= 360;
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
            float minBoost = stats.defualts.boostStats.meterMinimumUseValue;

            if (stats.boostMeter > minBoost)
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
