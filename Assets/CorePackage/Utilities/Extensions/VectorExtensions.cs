using UnityEngine;

public static class VectorExtensions
{
    private static readonly Vector3[] _compass3D = { Vector3.left, Vector3.right, Vector3.up, Vector3.down, Vector3.forward, Vector3.back };
    private static readonly Vector2[] _compass2D = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

    /// <summary>
    /// Collapse each axis to be either 1, 0 or -1. With optional epsilon precision.
    /// </summary>
    public static Vector3 CollapseAxisToSign(this Vector3 vector, float epsilon = 0f)
    {
        vector.x = Mathf.Abs(vector.x) > epsilon ? Mathf.Sign(vector.x) : 0f;

        vector.y = Mathf.Abs(vector.y) > epsilon ? Mathf.Sign(vector.y) : 0f;

        vector.z = Mathf.Abs(vector.z) > epsilon ? Mathf.Sign(vector.z) : 0f;

        return vector;
    }

    /// <summary>
    /// Collapse each axis to be either 1, 0 or -1. With optional epsilon precision.
    /// </summary>
    public static Vector2 CollapseAxisToSign(this Vector2 vector, float epsilon = 0f)
    {
        vector.x = Mathf.Abs(vector.x) > epsilon ? Mathf.Sign(vector.x) : 0f;

        vector.y = Mathf.Abs(vector.y) > epsilon ? Mathf.Sign(vector.y) : 0f;

        return vector;
    }

    /// <summary>
    /// Returns true if the vector is one of the six cardinal directions.
    /// </summary>
    public static bool IsCardinal(this Vector3 vector)
    {
        return vector == Vector3.right
                || vector == Vector3.left
                || vector == Vector3.up
                || vector == Vector3.down
                || vector == Vector3.forward
                || vector == Vector3.back;
    }

    /// <summary>
    /// Returns true if the vector is one of the four cardinal directions.
    /// </summary>
    public static bool IsCardinal(this Vector2 vector)
    {
        return vector == Vector2.right
            || vector == Vector2.left
            || vector == Vector2.up
            || vector == Vector2.down;
    }


    /// <summary>
    /// Returns the closest of the six cardinal direction.
    /// </summary>
    public static Vector3 ClosestCardinalDirection(this Vector3 direction)
    {
        float maxDotProduct = -Mathf.Infinity;
        Vector3 returnValue = Vector3.zero;

        foreach (Vector3 cardinalDir in _compass3D)
        {
            float dotProduct = Vector3.Dot(direction, cardinalDir);

            if (dotProduct > maxDotProduct)
            {
                returnValue = cardinalDir;
                maxDotProduct = dotProduct;
            }
        }

        return returnValue;
    }

    /// <summary>
    /// Returns the closest of the four cardinal direction.
    /// </summary>
    public static Vector2 ClosestCardinalDirection(this Vector2 direction)
    {
        float maxDotProduct = -Mathf.Infinity;
        Vector2 returnValue = Vector2.zero;

        foreach (Vector2 cardinalDir in _compass2D)
        {
            float dotProduct = Vector2.Dot(direction, cardinalDir);

            if (dotProduct > maxDotProduct)
            {
                returnValue = cardinalDir;
                maxDotProduct = dotProduct;
            }
        }

        return returnValue;
    }
}
