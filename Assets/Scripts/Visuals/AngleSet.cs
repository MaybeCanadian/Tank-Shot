using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Angle List", menuName = "Assets/Angle List")]

public class AngleSet : ScriptableObject
{
    [SerializeField]
    private List<SpriteSet> angles = new List<SpriteSet>();

    public SpriteSet GetSet(int index)
    {
        if(index < 0 || index > angles.Count)
        {
            Debug.LogError("ERROR - Index out of range.");
            return null;
        }

        return angles[index];
    }
}
