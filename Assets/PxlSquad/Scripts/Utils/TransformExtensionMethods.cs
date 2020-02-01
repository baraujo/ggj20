using UnityEngine;

/// <summary>
/// Methods for setting Transform.position coordinates directly, instead of
/// creating temporary variables ad nauseum
/// </summary>
public static class TransformExtensionMethods {
    public static void setPositionX(this Transform transform, float value) {
        var current = transform.position;
        current.x = value;
        transform.position = current;
    }

    public static void setPositionY(this Transform transform, float value) {
        var current = transform.position;
        current.y = value;
        transform.position = current;
    }

    public static void setPositionZ(this Transform transform, float value) {
        var current = transform.position;
        current.z = value;
        transform.position = current;
    }
    
    public static void addPositionX(this Transform transform, float value) {
        var current = transform.position;
        current.x += value;
        transform.position = current;
    }

    public static void addPositionY(this Transform transform, float value) {
        var current = transform.position;
        current.y += value;
        transform.position = current;
    }

    public static void addPositionZ(this Transform transform, float value) {
        var current = transform.position;
        current.z += value;
        transform.position = current;
    }
}
