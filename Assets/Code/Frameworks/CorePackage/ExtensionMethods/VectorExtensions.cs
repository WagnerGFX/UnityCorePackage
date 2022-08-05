using UnityEngine;

public static class VectorExtensions
{
    private static readonly Vector3[] compass3D = { Vector3.left, Vector3.right, Vector3.up, Vector3.down, Vector3.forward, Vector3.back };
    private static readonly Vector2[] compass2D = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

    /// <summary>
    /// Collapse each axis to be either 1, 0 or -1. With optional epsilon precision.
    /// </summary>
    public static Vector3 CollapseAxisToSign(this Vector3 vector, float epsilon = 0f)
    {
        if (Mathf.Abs(vector.x) > epsilon)
            vector.x = Mathf.Sign(vector.x);
        else
            vector.x = 0f;

        if (Mathf.Abs(vector.y) > epsilon)
            vector.y = Mathf.Sign(vector.y);
        else
            vector.y = 0f;

        if (Mathf.Abs(vector.z) > epsilon)
            vector.z = Mathf.Sign(vector.z);
        else
            vector.z = 0f;

        return vector;
    }

    /// <summary>
    /// Collapse each axis to be either 1, 0 or -1. With optional epsilon precision.
    /// </summary>
    public static Vector2 CollapseAxisToSign(this Vector2 vector, float epsilon = 0f)
    {
        if (Mathf.Abs(vector.x) > epsilon)
            vector.x = Mathf.Sign(vector.x);
        else
            vector.x = 0f;

        if (Mathf.Abs(vector.y) > epsilon)
            vector.y = Mathf.Sign(vector.y);
        else
            vector.y = 0f;

        return vector;
    }


    /// <summary>
    /// Returns true if the vector is one of the six cardinal directions.
    /// </summary>
    public static bool IsCardinal(this Vector3 vector)
    {
        if (vector == Vector3.right)
            return true;
        if (vector == Vector3.left)
            return true;
        if (vector == Vector3.up)
            return true;
        if (vector == Vector3.down)
            return true;
        if (vector == Vector3.forward)
            return true;
        if (vector == Vector3.back)
            return true;

        return false;
    }

    /// <summary>
    /// Returns true if the vector is one of the four cardinal directions.
    /// </summary>
    public static bool IsCardinal(this Vector2 vector)
    {
        if (vector == Vector2.right)
            return true;
        if (vector == Vector2.left)
            return true;
        if (vector == Vector2.up)
            return true;
        if (vector == Vector2.down)
            return true;

        return false;
    }

    
    /// <summary>
    /// Returns the closest of the six cardinal direction.
    /// </summary>
    public static Vector3 ClosestCardinalDirection(this Vector3 direction)
    {
        float maxDotProduct = -Mathf.Infinity;
        Vector3 returnValue = Vector3.zero;

        foreach (Vector3 cardinalDir in compass3D)
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

        foreach (Vector2 cardinalDir in compass2D)
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
