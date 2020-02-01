using UnityEngine;

public static class VectorExtensions
{
    public static Vector2[] toVector2Array(this Vector3[] v3) {
        return System.Array.ConvertAll(v3, getV3fromV2);
    }

    public static Vector2 getV3fromV2(Vector3 v3) {
        return new Vector2(v3.x, v3.y);
    }

    public static Vector3[] toVector3Array(this Vector2[] v2) {
        return System.Array.ConvertAll(v2, getV2fromV3);
    }

    public static Vector3 getV2fromV3(Vector2 v2) {
        return new Vector3(v2.x, v2.y, 0);
    }

    /// <summary>
    /// Performs a Catmull-Rom interpolation using the specified positions.
    /// </summary>
    /// <param name="value1">The first position in the interpolation.</param>
    /// <param name="value2">The second position in the interpolation.</param>
    /// <param name="value3">The third position in the interpolation.</param>
    /// <param name="value4">The fourth position in the interpolation.</param>
    /// <param name="amount">Weighting factor.</param>
    /// <param name="result">When the method completes, contains the result of the Catmull-Rom interpolation.</param>
    public static void CatmullRom(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, float amount, out Vector2 result) {
        float squared = amount * amount;
        float cubed = amount * squared;

        result.x = 0.5f * ((((2.0f * value2.x) + ((-value1.x + value3.x) * amount)) +
        (((((2.0f * value1.x) - (5.0f * value2.x)) + (4.0f * value3.x)) - value4.x) * squared)) +
        ((((-value1.x + (3.0f * value2.x)) - (3.0f * value3.x)) + value4.x) * cubed));

        result.y = 0.5f * ((((2.0f * value2.y) + ((-value1.y + value3.y) * amount)) +
            (((((2.0f * value1.y) - (5.0f * value2.y)) + (4.0f * value3.y)) - value4.y) * squared)) +
            ((((-value1.y + (3.0f * value2.y)) - (3.0f * value3.y)) + value4.y) * cubed));
    }

    /// <summary>
    /// Performs a Catmull-Rom interpolation using the specified positions.
    /// </summary>
    /// <param name="value1">The first position in the interpolation.</param>
    /// <param name="value2">The second position in the interpolation.</param>
    /// <param name="value3">The third position in the interpolation.</param>
    /// <param name="value4">The fourth position in the interpolation.</param>
    /// <param name="amount">Weighting factor.</param>
    /// <returns>A vector that is the result of the Catmull-Rom interpolation.</returns>
    public static Vector2 CatmullRom(Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount) {
        Vector2 result;
        CatmullRom(ref value1, ref value2, ref value3, ref value4, amount, out result);
        return result;
    }

    public static Vector3 SetX(this Vector3 v, float x) {
        return new Vector3(x, v.y, v.z);
    }

    public static Vector3 SetY(this Vector3 v, float y) {
        return new Vector3(v.x, y, v.z);
    }

    public static Vector3 SetZ(this Vector3 v, float z) {
        return new Vector3(v.x, v.y, z);
    }

    public static Vector2 SetX(this Vector2 v, float x) {
        return new Vector2(x, v.y);
    }

    public static Vector2 SetY(this Vector2 v, float y) {
        return new Vector2(v.x, y);
    }
}
