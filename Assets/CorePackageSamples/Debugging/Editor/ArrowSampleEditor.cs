using CorePackage.Debugging;
using UnityEditor;

namespace CorePackageSamples.Debugging
{
    [CustomEditor(typeof(ArrowSample))]
    public class ArrowSampleEditor : Editor
    {
        [DrawGizmo(GizmoType.Selected | GizmoType.Active | GizmoType.NotInSelectionHierarchy)]
#pragma warning disable IDE0060, IDE0051 // Unused private member and parameter, used by DrawGizmo
        private static void OnDrawGizmos(ArrowSample myScript, GizmoType gizmoType)
#pragma warning restore IDE0060, IDE0051
        {
            if (myScript.Target != null)
            {
                DrawArrow.ForGizmoTwoPoints(
                    myScript.transform.position,
                    myScript.Target.position,
                    DrawArrow.DEFAULT_ARROW_HEAD_LENGTH,
                    DrawArrow.DEFAULT_ARROW_HEAD_ANGLE,
                    0.5f);
            }
        }
    }
}
