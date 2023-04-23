using UnityEditor;
using CorePackage.Debugging;

[CustomEditor(typeof(ArrowSample))]
public class ArrowSampleEditor : Editor
{
    [DrawGizmo(GizmoType.Selected | GizmoType.Active | GizmoType.NotInSelectionHierarchy)]
    static void OnDrawGizmos(ArrowSample myScript, GizmoType gizmoType)
    {
        if (myScript.Target != null)
        {
            DrawArrow.ForGizmoTwoPoints(
                myScript.transform.position,
                myScript.Target.position,
                DrawArrow.defaultArrowHeadLength,
                DrawArrow.defaultArrowHeadAngle,
                0.5f);
        }
    }
}
