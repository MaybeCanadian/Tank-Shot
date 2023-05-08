using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Tank Stats/Boost", fileName = "New Tank Boost Stats", order = 3)]
public class TankBoostStats : ScriptableObject
{
    [Header("Boost Bonuses")]
    [Tooltip("The amount boosting increases the tank's normal acceleration")]
    public TaggedStats accelerationMuliplier = new(TaggedStats_Tags.TankBoosting_AccelerationMultiplier, 1.5f);
    [Tooltip("The amount boosting increases the tank's normal max speeds")]
    public TaggedStats maxSpeedMultiplier = new(TaggedStats_Tags.TankBoosting_MaxSpeedMultiplier, 1.5f);

    [Header("Boost Meter")]
    [Tooltip("How fast the boost meter depletes per second")]
    public TaggedStats meterUseRate = new(TaggedStats_Tags.TankBoosting_UseRate, 100.0f);
    [Tooltip("How fast the boost meter regenerates per second")]
    public TaggedStats regenUseRate = new(TaggedStats_Tags.TankBoosting_RegenRate, 50.0f);
    [Tooltip("The maximum value of the boost meter")]
    public TaggedStats meterMaxValue = new(TaggedStats_Tags.TankBoosting_MeterMaxValue, 100.0f);
    [Tooltip("The value the meter must reach to reuse boost if boost meter is fully emptied")]
    public TaggedStats overuseAmount = new(TaggedStats_Tags.TankBoosting_MeterOveruseAmount, 0.0f);

    [Tooltip("At what boost meter value the boost automatically stops")]
    public float meterCancelPoint = 0.05f;
    [Tooltip("The minimum value the boost meter requires to start boosting")]
    public float meterMinimumUseValue = 1.0f;

}