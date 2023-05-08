using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tank Stats/Movement", fileName = "New Tank Movement Stats", order = 1)]
public class TankMovementStats : ScriptableObject
{
    [Header("Forwards Movement Stats")]
    public TaggedStats forwardsAcceleration = new(TaggedStats_Tags.TankMovement_ForwardsAcceleration, 10.0f);
    public TaggedStats maxMoveSpeedForwards = new(TaggedStats_Tags.TankMovement_MaxMoveSpeedForwards, 3.0f);

    [Header("Backwards Movement STats")]
    public TaggedStats backwardsAcceleration = new(TaggedStats_Tags.TankMovement_BackwardsAcceleration, 5.0f);
    public TaggedStats maxMoveSpeedBackwards = new(TaggedStats_Tags.TankMovement_MaxMoveSpeedBackwards, -1.0f);

    [Header("Move Speed Decays")]
    [Tooltip("A multiplier applied to move speed to slow tank down")]
    public float moveSpeedDecay = 0.9f;
    [Tooltip("The point at which the movespeed becomes set to 0")]
    public float moveSpeedZeroPoint = 0.001f;
}
