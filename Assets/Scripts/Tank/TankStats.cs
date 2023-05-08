using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStats : MonoBehaviour
{
    [Header("Rotations")]
    [Header("Body Rotation")]
    [Range(0.0f, 360.0f)]
    public float bodyRotation = 0.0f;

    [Header("Body Rotation Stats")]
    public float rotationSpeed = 0.0f;
    public float maxRotationSpeed = 200.0f;
    public float rotationForce = 50.0f;
    public float rotationSpeedDecay = 0.9f;
    public float rotationSpeedZeroPoint = 0.001f;

    [Header("Turret Rotation")]
    [Range(0.0f, 360.0f)]
    public float turretRotation = 0.0f;
    public bool syncBodyAndTurret = true;

    [Header("Movement")]
    [Header("Move Speed")]
    public float forwardsMoveSpeed = 0.0f;

    [Header("Forwards Movement Stats")]
    public float moveForceForwards = 10.0f;
    public float maxForwardsMoveSpeed = 1.0f;

    [Header("Backwards Movement STats")]
    public float moveForceBackwards = 5.0f;
    public float maxBackwardsMoveSpeed = -0.5f;

    [Header("Move Speed Decays")]
    public float moveSpeedDecay = 0.9f;
    public float moveSpeedZeroPoint = 0.001f;

    [Header("Boost")]
    [Header("Boost Effects")]
    public float boostForceMultiplier = 2.0f;
    public float boostMaxSpeedMultiplier = 2.0f;
    public bool boostActive = false;

    [Header("Boost Meter")]
    public float boostMeter = 100.0f;
    public float boostMeterUseRate = 10.0f;
    public float boostMeterRegenRate = 10.0f;
    public float boostMeterMaxValue = 100.0f;
    public float bosstMeterCancelPoint = 0.05f;
    public float boostMinRequired = 1.0f;

    [Header("Visuals")]
    [Header("Camera")]
    [Range(0, 3)]
    public int cameraAngle;
}
