using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum TaggedStats_Tags
{
    TankRotation_AngularAcceleration,
    TankRotation_MaxAngularSpeed,

    TankMovement_ForwardsAcceleration,
    TankMovement_BackwardsAcceleration,
    TankMovement_MaxMoveSpeedForwards,
    TankMovement_MaxMoveSpeedBackwards,

    TankBoosting_MeterMaxValue,
    TankBoosting_UseRate,
    TankBoosting_RegenRate,
    TankBoosting_AccelerationMultiplier,
    TankBoosting_MaxSpeedMultiplier,
    TankBoosting_MeterOveruseAmount,

    TankShooting_ShotCooldown
}
