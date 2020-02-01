using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtensions
{
    public static float Map(this float from, float fromMin, float fromMax, float toMin, float toMax) {
        float normalized = Mathf.InverseLerp(fromMin, fromMax, from);
        return Mathf.Lerp(toMin, toMax, normalized);
    }
}
