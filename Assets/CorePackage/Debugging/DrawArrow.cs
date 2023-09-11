using UnityEngine;

//Handles not used to avoid UnityEditor references

namespace CorePackage.Debugging
{
    /// <summary>
    /// Draws arrows with lines using Gizmos or Debug.
    /// </summary>
    public static class DrawArrow
    {
        public const float DEFAULT_ARROW_HEAD_LENGTH = 0.25f;
        public const float DEFAULT_ARROW_HEAD_ANGLE = 20f;
        public const float DEFAULT_ARROW_HEAD_NORMALIZED_POSITION = 1f;

        /// <summary>
        /// Draw with Gizmos from origin towards direction, using the magnitude as arrow length.
        /// </summary>
        public static void ForGizmo(Vector3 origin, Vector3 direction, float arrowHeadLength = DEFAULT_ARROW_HEAD_LENGTH, float arrowHeadAngle = DEFAULT_ARROW_HEAD_ANGLE, float arrowHeadNormalizedPosition = DEFAULT_ARROW_HEAD_NORMALIZED_POSITION)
        {
            Arrow(DrawMode.Gizmo, origin, direction, Gizmos.color, arrowHeadLength, arrowHeadAngle, arrowHeadNormalizedPosition);
        }

        /// <summary>
        /// Draws with Gizmos for two points with the arrow head pointing towards the destination.
        /// </summary>
        public static void ForGizmoTwoPoints(Vector3 origin, Vector3 destination, float arrowHeadLength = DEFAULT_ARROW_HEAD_LENGTH, float arrowHeadAngle = DEFAULT_ARROW_HEAD_ANGLE, float arrowHeadNormalizedPosition = DEFAULT_ARROW_HEAD_NORMALIZED_POSITION)
        {
            Vector3 direction = destination - origin;
            Arrow(DrawMode.Gizmo, origin, direction, Gizmos.color, arrowHeadLength, arrowHeadAngle, arrowHeadNormalizedPosition);
        }

        /// <summary>
        /// Draw with Debug from origin towards direction, using the magnitude as arrow length.
        /// </summary>
        public static void ForDebug(Vector3 origin, Vector3 direction, float arrowHeadLength = DEFAULT_ARROW_HEAD_LENGTH, float arrowHeadAngle = DEFAULT_ARROW_HEAD_ANGLE, float arrowHeadNormalizedPosition = DEFAULT_ARROW_HEAD_NORMALIZED_POSITION)
        {
            ForDebug(origin, direction, Color.white, arrowHeadLength, arrowHeadAngle, arrowHeadNormalizedPosition);
        }

        /// <summary>
        /// Draw with Debug from origin towards direction, using the magnitude as arrow length.
        /// </summary>
        public static void ForDebug(Vector3 origin, Vector3 direction, Color color, float arrowHeadLength = DEFAULT_ARROW_HEAD_LENGTH, float arrowHeadAngle = DEFAULT_ARROW_HEAD_ANGLE, float arrowHeadNormalizedPosition = DEFAULT_ARROW_HEAD_NORMALIZED_POSITION)
        {
            Arrow(DrawMode.Debug, origin, direction, color, arrowHeadLength, arrowHeadAngle, arrowHeadNormalizedPosition);
        }

        /// <summary>
        /// Draws with Debug for two points with the arrow head pointing towards the destination. Default color as white.
        /// </summary>
        public static void ForDebugTwoPoints(Vector3 origin, Vector3 destination, float arrowHeadLength = DEFAULT_ARROW_HEAD_LENGTH, float arrowHeadAngle = DEFAULT_ARROW_HEAD_ANGLE, float arrowHeadNormalizedPosition = DEFAULT_ARROW_HEAD_NORMALIZED_POSITION)
        {
            Vector3 direction = destination - origin;
            Arrow(DrawMode.Gizmo, origin, direction, Color.white, arrowHeadLength, arrowHeadAngle, arrowHeadNormalizedPosition);
        }

        /// <summary>
        /// Draws with Debug for two points with the arrow head pointing towards the destination.
        /// </summary>
        public static void ForDebugTwoPoints(Vector3 origin, Vector3 destination, Color color, float arrowHeadLength = DEFAULT_ARROW_HEAD_LENGTH, float arrowHeadAngle = DEFAULT_ARROW_HEAD_ANGLE, float arrowHeadNormalizedPosition = DEFAULT_ARROW_HEAD_NORMALIZED_POSITION)
        {
            Vector3 direction = destination - origin;
            Arrow(DrawMode.Gizmo, origin, direction, color, arrowHeadLength, arrowHeadAngle, arrowHeadNormalizedPosition);
        }

        /// <summary>
        /// Returns a value between 0 and 1 for a position where the arrow head should be drawn in the arrow body.
        /// <para>Used for arrowHeadNormalizedPosition.</para>
        /// </summary>
        /// <param name="toDestination">Should evaluate the distance to destination or origin?</param>
        public static float GetNormalizedArrowPosition(Vector3 origin, Vector3 destination, float arrowHeadDistance, bool toDestination = true)
        {
            float arrowBodyLength = (destination - origin).magnitude;

            if (arrowBodyLength == 0f)
            { return 0f; }

            if (arrowBodyLength < arrowHeadDistance)
            { return toDestination ? 0f : 1f; }

            float normalizedDistance = (arrowBodyLength - arrowHeadDistance) / arrowBodyLength;

            return toDestination ? normalizedDistance : 1f - normalizedDistance;
        }


        private static void Arrow(DrawMode drawMode, Vector3 origin, Vector3 direction, Color color, float arrowHeadLength = DEFAULT_ARROW_HEAD_LENGTH, float arrowHeadAngle = DEFAULT_ARROW_HEAD_ANGLE, float arrowHeadNormalizedPosition = DEFAULT_ARROW_HEAD_NORMALIZED_POSITION)
        {
            if (direction.magnitude == 0f)
            { return; }


            Vector3 arrowTip = origin + (direction * arrowHeadNormalizedPosition);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(arrowHeadAngle, 0, 0)  * Vector3.back * arrowHeadLength;
            Vector3 left  = Quaternion.LookRotation(direction) * Quaternion.Euler(-arrowHeadAngle, 0, 0) * Vector3.back * arrowHeadLength;
            Vector3 up    = Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0)  * Vector3.back * arrowHeadLength;
            Vector3 down  = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back * arrowHeadLength;

            switch (drawMode)
            {
                case DrawMode.Gizmo:
                    Gizmos.DrawRay(origin, direction);
                    Gizmos.DrawRay(arrowTip, right);
                    Gizmos.DrawRay(arrowTip, left);
                    Gizmos.DrawRay(arrowTip, up);
                    Gizmos.DrawRay(arrowTip, down);
                    break;
                case DrawMode.Debug:
                    Debug.DrawRay(origin, direction, color);
                    Debug.DrawRay(arrowTip, right, color);
                    Debug.DrawRay(arrowTip, left, color);
                    Debug.DrawRay(arrowTip, up, color);
                    Debug.DrawRay(arrowTip, down, color);
                    break;
            }
        }

        private enum DrawMode
        {
            Gizmo, Debug
        }
    }
}
