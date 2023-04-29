using UnityEditor;
using UnityEngine;

public static class UnityObjectExtensions
{
    // Serialization does not understand null so it changes the field into an empty object of the same type named "null".
    // See: https://docs.unity3d.com/Manual/script-Serialization.html


    /// <summary>
    /// Check if Object is valid.
    /// </summary>
    /// <returns>true = valid, false = null</returns>
    public static bool IsValid(this Object obj)
    {
        return obj != null && obj.ToString() != "null";
    }

    /// <summary>
    /// Check if Object is invalid.
    /// </summary>
    /// <returns>true = null; false = valid</returns>
    public static bool IsNull(this Object obj)
    {
        return obj == null || obj.ToString() == "null";
    }

    /// <summary>
    /// Check if Object is a file in the project.
    /// </summary>
    /// <returns>False if invalid, folder or scene asset.</returns>
    public static bool IsAssetFile(this Object obj)
    {
        if (obj.IsNull())
        { return false; }

        bool inDataBase = AssetDatabase.Contains(obj);
        string assetPath = AssetDatabase.GetAssetPath(obj);

        if (string.IsNullOrEmpty(assetPath))
        { return false; }

        bool isFile = !AssetDatabase.IsValidFolder(assetPath);

        return inDataBase && isFile;
    }

    /// <summary>
    /// Check if Object is a folder in the project.
    /// </summary>
    /// <returns>False if invalid, file or scene asset.</returns>
    public static bool IsAssetFolder(this Object obj)
    {
        if (obj.IsNull())
        { return false; }

        bool inDataBase = AssetDatabase.Contains(obj);
        string assetPath = AssetDatabase.GetAssetPath(obj);

        if (string.IsNullOrEmpty(assetPath))
        { return false; }

        bool isFolder = AssetDatabase.IsValidFolder(assetPath);

        return inDataBase && isFolder;
    }

    /// <summary>
    /// Check if Object is a file or folder in the project.
    /// </summary>
    /// <returns>False if invalid, or scene asset.</returns>
    public static bool IsAssetFileOrFolder(this Object obj)
    {
        if (obj.IsNull())
        { return false; }

        bool inDataBase = AssetDatabase.Contains(obj);
        string assetPath = AssetDatabase.GetAssetPath(obj);

        if (string.IsNullOrEmpty(assetPath))
        { return false; }

        return inDataBase;
    }

    /// <summary>
    /// Check if Object is a prefab file in the project.
    /// </summary>
    public static bool IsPrefabAsset(this Object obj)
    {
        if (obj.IsNull())
        { return false; }

        bool inDataBase = AssetDatabase.Contains(obj);
        string assetPath = AssetDatabase.GetAssetPath(obj);

        if (string.IsNullOrEmpty(assetPath))
        { return false; }

        bool isFile = !AssetDatabase.IsValidFolder(assetPath);

        bool isPrefab = typeof(GameObject).IsAssignableFrom(obj.GetType());

        return inDataBase && isFile && isPrefab;
    }
}
