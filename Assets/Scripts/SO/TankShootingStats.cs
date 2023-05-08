using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tank Stats/Shooting", fileName = "New Tank Shooting Stats", order = 0)]
public class TankShootingStats : ScriptableObject
{
    public TaggedStats shotCooldown = new (TaggedStats_Tags.TankShooting_ShotCooldown, 1.0f);

    
}
