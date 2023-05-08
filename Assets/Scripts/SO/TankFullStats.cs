using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tank Stats/Full", fileName = "New Tank Full Stats", order = 0)]
public class TankFullStats : ScriptableObject
{
    public TankBodyRotationStats rotationStats;
    public TankMovementStats movementStats;
    public TankBoostStats boostStats;
    public TankShootingStats shootingStats;
}
