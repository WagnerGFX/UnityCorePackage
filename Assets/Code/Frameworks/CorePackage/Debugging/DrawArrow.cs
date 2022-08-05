using UnityEngine;

//Handles removed to avoid UnityEditor references

namespace CorePackage.Debugging
{
    public static class DrawArrow
    {
        public const float defaultArrowHeadLength = 0.25f;
        public const float defaultArrowHeadAngle = 20f;
        public const float defaultNormalizedArrowPosition = 1f;


        public static void ForGizmo(Vector3 pos, Vector3 direction, float arrowHeadLength = defaultArrowHeadLength, float arrowHeadAngle = defaultArrowHeadAngle, float normalizedArrowPosition = defaultNormalizedArrowPosition)
        {
            Arrow(DrawMode.Gizmo, pos, direction, Gizmos.color, arrowHeadLength, arrowHeadAngle, normalizedArrowPosition);
        }

        public static void ForGizmoTwoPoints(Vector3 from, Vector3 to, float arrowHeadLength = defaultArrowHeadLength, float arrowHeadAngle = defaultArrowHeadAngle, float normalizedArrowPosition = defaultNormalizedArrowPosition)
        {
            Vector3 direction = to - from;
            Arrow(DrawMode.Gizmo, from, direction, Gizmos.color, arrowHeadLength, arrowHeadAngle, normalizedArrowPosition);
        }


        public static void ForDebug(Vector3 pos, Vector3 direction, float arrowHeadLength = defaultArrowHeadLength, float arrowHeadAngle = defaultArrowHeadAngle, float normalizedArrowPosition = defaultNormalizedArrowPosition)
        {
            ForDebug(pos, direction, Color.white, arrowHeadLength, arrowHeadAngle, normalizedArrowPosition);
        }

        public static void ForDebug(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = defaultArrowHeadLength, float arrowHeadAngle = defaultArrowHeadAngle, float normalizedArrowPosition = defaultNormalizedArrowPosition)
        {
            Arrow(DrawMode.Debug, pos, direction, color, arrowHeadLength, arrowHeadAngle, normalizedArrowPosition);
        }


        public static void ForDebugTwoPoints(Vector3 from, Vector3 to, float arrowHeadLength = defaultArrowHeadLength, float arrowHeadAngle = defaultArrowHeadAngle, float normalizedArrowPosition = defaultNormalizedArrowPosition)
        {
            Vector3 direction = to - from;
            Arrow(DrawMode.Gizmo, from, direction, Color.white, arrowHeadLength, arrowHeadAngle, normalizedArrowPosition);
        }

        public static void ForDebugTwoPoints(Vector3 from, Vector3 to, Color color, float arrowHeadLength = defaultArrowHeadLength, float arrowHeadAngle = defaultArrowHeadAngle, float normalizedArrowPosition = defaultNormalizedArrowPosition)
        {
            Vector3 direction = to - from;
            Arrow(DrawMode.Gizmo, from, direction, color, arrowHeadLength, arrowHeadAngle, normalizedArrowPosition);
        }


        public static float GetNormalizedArrowPosition(Vector3 from, Vector3 to, float arrowDistance, bool toTarget = true)
        {
            float distance = (to - from).magnitude;

            if (distance == 0f)
                return 0f;

            if (distance < arrowDistance)
                return toTarget ? 0f : 1f;

            float normalizedDistance = (distance - arrowDistance) / distance;

            if (toTarget)
                return normalizedDistance;
            else
                return 1f - normalizedDistance;
        }


        private static void Arrow(DrawMode drawMode, Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = defaultArrowHeadLength, float arrowHeadAngle = defaultArrowHeadAngle, float normalizedArrowPosition = defaultNormalizedArrowPosition)
        {
            if (direction.magnitude == 0f)
                return;


            Vector3 arrowTip = pos + (direction * normalizedArrowPosition);

            Vector3 right = (Quaternion.LookRotation(direction) * Quaternion.Euler(arrowHeadAngle, 0, 0) * Vector3.back) * arrowHeadLength;
            Vector3 left = (Quaternion.LookRotation(direction) * Quaternion.Euler(-arrowHeadAngle, 0, 0) * Vector3.back) * arrowHeadLength;
            Vector3 up = (Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.back) * arrowHeadLength;
            Vector3 down = (Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back) * arrowHeadLength;

            switch (drawMode)
            {
                case DrawMode.Gizmo:
                    Gizmos.DrawRay(pos, direction);
                    Gizmos.DrawRay(arrowTip, right);
                    Gizmos.DrawRay(arrowTip, left);
                    Gizmos.DrawRay(arrowTip, up);
                    Gizmos.DrawRay(arrowTip, down);
                    break;
                case DrawMode.Debug:
                    Debug.DrawRay(pos, direction, color);
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