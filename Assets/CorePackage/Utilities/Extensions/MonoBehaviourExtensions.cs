using UnityEngine;

public static class MonoBehaviourExtensions
{
    private const string THICK_ARROW = "\u279C";
    private const string OPEN = "\u300C";
    private const string CLOSE = "\u300D";

    /// <summary>
    /// <para>Use to check for unassigned [<see cref="SerializeField"/>] members during <c>OnValidate()</c>.</para>
    /// Accepts only Unity Objects like <see cref="GameObject"/> prefabs, <see cref="MonoBehaviour"/> components and <see cref="ScriptableObject"/>.
    /// </summary>
    public static void AssertObjectField(this MonoBehaviour component, Object fieldObject, string fieldName)
    {
        if (fieldObject.IsNull())
        {
            Debug.LogAssertion($"  {component.gameObject.name} {THICK_ARROW} {component.GetType().Name} {THICK_ARROW} {fieldName}  {OPEN} field is required {CLOSE}", component);
        }
    }
}
