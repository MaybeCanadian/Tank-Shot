using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Tank Stats/Rotation", fileName = "New Tank Rotation Stats", order = 2)]
public class TankBodyRotationStats : ScriptableObject
{
    [Tooltip("The rate at rotation speed is increased")]
    public TaggedStats angularAcceleration = new(TaggedStats_Tags.TankRotation_AngularAcceleration, 400.0f);
    [Tooltip("The maximum allowed angular speed")]
    public TaggedStats maxAngularSpeed = new(TaggedStats_Tags.TankRotation_MaxAngularSpeed, 150.0f);

    [Tooltip("A multiplier applied to the speed when no input is pressed")]
    public float speedDecayRate = 0.9f;
    [Tooltip("The value at which the speed will be set to zero")]
    public float speedZeroPoint = 0.001f;
}