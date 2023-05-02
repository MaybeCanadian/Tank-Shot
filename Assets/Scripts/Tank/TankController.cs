using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [Header("Visuals")]
    public TankVisuals visuals;

    private void Awake()
    {
        visuals = GetComponent<TankVisuals>();

        if(visuals == null)
        {
            Debug.LogError("ERROR - Could not locate tank visuals");
        }
    }


}
