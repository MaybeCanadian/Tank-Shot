using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStats : MonoBehaviour
{
    [Header("Body Rotation")]
    [Range(0.0f, 360.0f)]
    public float bodyRotation = 0.0f;
    public float angularSpeed = 0.0f;

    [Header("Turret Rotation")]
    [Range(0.0f, 360.0f)]
    public float turretRotation = 0.0f;
    public bool syncBodyAndTurret = true;

    [Header("Move Speed")]
    public float moveSpeed = 0.0f;

    [Header("Boost")]
    public bool boostActive = false;
    public float boostMeter = 100.0f;

    public TankFullStats defualts;
}