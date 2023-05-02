using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraAngleController
{
    #region Event Dispatchers
    public delegate void CameraAngleEvent(int newAngle);
    public static event CameraAngleEvent CameraAngleChanged;
    #endregion

    private static Dictionary<int, float> angleDict = new Dictionary<int, float>()
    {
        {0, 0.0f },
        {1, 45.0f },
        {2, 60.0f},
        {3, 90.0f }
    };

    private static int cameraAngle = 0;
    private static Vector3 offset = Vector3.zero;

    #region Camera Angle
    public static void SetCameraAngle(int angle)
    {
        if(angle < 0 || angle > 3)
        {
            Debug.LogError("ERROR - Given camera angle is out of range.");
            return;
        }

        cameraAngle = angle;

        GenerateOffset();

        CameraAngleChanged?.Invoke(cameraAngle);
    }
    private static void GenerateOffset()
    {
        offset = new Vector3(Mathf.Cos(angleDict[cameraAngle] * Mathf.Deg2Rad), Mathf.Sin(angleDict[cameraAngle] * Mathf.Deg2Rad), 0.0f).normalized;
    }
    public static Vector3 GetOffset()
    {
        return offset;
    }
    public static int GetCameraAngle()
    {
        return cameraAngle;
    }
    #endregion
}
