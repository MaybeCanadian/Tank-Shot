using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class TaggedStats
{
    private TaggedStats_Tags tag;
    public float value;

    public TaggedStats(TaggedStats_Tags tag, float value)
    {
        this.tag = tag;
        this.value = value;
    }

    public bool CompateTag(TaggedStats_Tags other)
    {
        return tag == other;
    }
}
