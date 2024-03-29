using UnityEngine;

namespace CorePackage.EventSystems.Unity.Debugging
{
    /// <summary>
    /// Extends common classes to build a debug info string.
    /// </summary>
    public static class DebugExtensions
    {
        const string DEBUG_TEMPLATE = "[{0}:{1}#{2}]";

        public static string GetDebugInfo(this MonoBehaviour target)
            => string.Format(DEBUG_TEMPLATE,
                             "U_MB",
                             target.gameObject.name,
                             target.gameObject.GetInstanceID());

        public static string GetDebugInfo(this GameObject target)
            => string.Format(DEBUG_TEMPLATE,
                             "U_GO",
                             target.name,
                             target.GetInstanceID());

        public static string GetDebugInfo(this ScriptableObject target)
            => string.Format(DEBUG_TEMPLATE,
                             "U_SO",
                             target.name,
                             target.GetInstanceID());

        public static string GetDebugInfo(this Object target)
            => string.Format(DEBUG_TEMPLATE,
                             "U_OBJ",
                             target.name,
                             target.GetInstanceID());

        public static string GetDebugInfo(this object target)
            => string.Format(DEBUG_TEMPLATE,
                             "OBJ",
                             target.GetType(),
                             target.ToString());

        /*
        // Commented to avoid InputSystem references.
        public static string GetDebugInfo(this UnityEngine.InputSystem.PlayerInput target)
            => string.Format(debugTemplate,
                            "PINPUT",
                            target.GetType(),
                            target.ToString());

        public static string GetDebugInfo(this UnityEngine.InputSystem.InputAction.CallbackContext target)
            => $"[INPUT:{target.action} <{target.action.phase}>]";
        */
    }
}
