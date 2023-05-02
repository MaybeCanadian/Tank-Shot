using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class TankVisuals : MonoBehaviour
{
    [Header("Angles")]
    [Range(0.0f, 360.0f)]
    public float bodyRotation;
    [Range(0.0f, 360.0f)]
    public float turretRotation;
    [Range(0, 3)]
    public int cameraAngle;
    public bool syncTurret = true;

    [Header("Updates")]
    public bool constantUpdate = false;
    public bool singleUpdate = false;

    [Header("Sprites")]
    public AngleSet bodySprites;
    public AngleSet turretSprites;

    [Header("Renderers")]
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer turretRenderer;

    [Header("Offsets")]
    public List<Vector3> cameraTurretOffsets;

    private void Start()
    {
        UpdateGraphics();
    }
    private void Update()
    {
        if (constantUpdate)
        {
            if(cameraAngle != CameraAngleController.GetCameraAngle())
            {
                CameraAngleController.SetCameraAngle(cameraAngle);
            }

            UpdateGraphics();
        }

        if (singleUpdate)
        {
            if (cameraAngle != CameraAngleController.GetCameraAngle())
            {
                CameraAngleController.SetCameraAngle(cameraAngle);
            }

            singleUpdate = false;
            UpdateGraphics();
        }
    }

    #region Graphics
    public void SetBodyRotation(float angle)
    {
        bodyRotation = angle;

        UpdateGraphics();
    }
    public void SetTurretAngle(float angle)
    {
        turretRotation = angle;

        UpdateGraphics();
    }
    public void SetCameraAngle(int angle)
    {
        angle = Mathf.Clamp(angle, 0, 3);

        cameraAngle = angle;

        UpdateGraphics();
    }
    public void UpdateGraphics()
    {
        if (bodyRenderer == null || turretRenderer == null)
        {
            return;
        }

        if (bodySprites == null || turretSprites == null)
        {
            return;
        }

        if (syncTurret)
        {
            turretRotation = bodyRotation;
        }

        float bodyRot = ((bodyRotation % 360.0f) / 11.5f);

        int bodyVal = Mathf.RoundToInt(bodyRot);

        float turretRot = ((turretRotation % 360.0f) / 11.5f);

        int turretVal = Mathf.RoundToInt(turretRot);

        Sprite bodySprite = bodySprites.GetSet(CameraAngleController.GetCameraAngle()).GetSprite(bodyVal);
        Sprite turretSprite = turretSprites.GetSet(CameraAngleController.GetCameraAngle()).GetSprite(turretVal);

        if (bodySprite == null || turretSprite == null)
        {
            return;
        }

        bodyRenderer.sprite = bodySprite;
        turretRenderer.sprite = turretSprite;

        turretRenderer.transform.localPosition = cameraTurretOffsets[cameraAngle];
    }
    #endregion
}
