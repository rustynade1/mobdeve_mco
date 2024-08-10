using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        // Ensure the position is within the screen bounds
        if (float.IsInfinity(position.x) || float.IsInfinity(position.y) || float.IsNaN(position.x) || float.IsNaN(position.y))
        {
            Debug.LogWarning("Invalid screen position: " + position);
            return Vector3.zero; // Return a default value
        }

        // Add a check for the z-value as well
        position.z = Mathf.Clamp(position.z, camera.nearClipPlane, camera.farClipPlane);

        return camera.ScreenToWorldPoint(position);
    }

}
