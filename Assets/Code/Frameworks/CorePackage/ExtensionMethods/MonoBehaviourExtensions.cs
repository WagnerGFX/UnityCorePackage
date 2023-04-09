using UnityEngine;

public static class MonoBehaviourExtensions
{
    /// <summary>
    /// <para>Use to check for unassigned [<see cref="SerializeField"/>] members during <c>OnValidate()</c>.</para>
    /// Accepts only Unity Objects like <see cref="GameObject"/> prefabs, <see cref="MonoBehaviour"/> components and <see cref="ScriptableObject"/>.
    /// </summary>
    public static void AssertObjectField(this MonoBehaviour component, Object fieldObject, string fieldName)
    {
        //Serialization does not understand null so it changes the field into an empty object of the same type named "null".
        //See: https://docs.unity3d.com/Manual/script-Serialization.html
        if (fieldObject == null || fieldObject.name.Equals("null"))
        {
            Debug.LogAssertion($"{component.gameObject.name} > {component.GetType().Name} > {fieldName} [{fieldObject.GetType().Name}] is required.", component);
        }
    }
}
