using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sprite List", menuName = "Assets/Sprite List")]
public class SpriteSet : ScriptableObject
{
    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();
    public Sprite GetSprite(int index)
    {
        if (index < 0 || index > sprites.Count)
        {
            Debug.LogError("ERROR - Index out of range of the asset list");
            return null;
        }

        return sprites[index];
    }
}
