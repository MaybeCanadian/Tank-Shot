using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class TankVisuals : MonoBehaviour
{
    private TankStats stats = null;

    private float oldBodyRotation;
    private float oldTurretRotation;
    private float oldCameraAngle;

    [Header("Sprites")]
    public AngleSet bodySprites;
    public AngleSet turretSprites;

    [Header("Colour")]
    public Color tankColour;
    public Color turretColour;

    private Color oldTankColor;
    private Color oldTurretColor;

    [Header("Renderers")]
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer turretRenderer;

    [Header("Offsets")]
    public List<Vector3> cameraTurretOffsets;


    #region Init Functions
    private void Awake()
    {
        stats = GetComponent<TankStats>();
    }
    private void Start()
    {
        SetOldValues();

        SetOldColors();

        UpdateGraphics();

        UpdateRenderColor();
    }
    private void SetOldValues()
    {
        oldBodyRotation = stats.bodyRotation;
        oldTurretRotation = stats.turretRotation;
    }
    private void SetOldCamera()
    {
        oldCameraAngle = CameraAngleController.GetCameraAngle();
    }
    private void SetOldColors()
    {
        oldTankColor = tankColour;
        oldTurretColor = turretColour;
    }
    #endregion

    #region Updates
    private void LateUpdate()
    {
        bool dirty = false;

        if (stats.bodyRotation != oldBodyRotation || stats.turretRotation != oldTurretRotation)
        {
            dirty = true;

            SetOldValues();
        }

        if(CameraAngleController.GetCameraAngle() != oldCameraAngle)
        {
            dirty = true;

            SetOldCamera();
        }

        if(tankColour != oldTankColor || turretColour != oldTurretColor)
        {
            UpdateRenderColor();

            SetOldColors();
        }

        if(dirty)
        {
            UpdateGraphics();
        }
    }
    #endregion

    #region Graphics
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

        if (stats.syncBodyAndTurret)
        {
            stats.turretRotation = stats.bodyRotation;
        }

        float bodyRot = ((stats.bodyRotation % 360.0f) / 11.5f);

        int bodyVal = Mathf.RoundToInt(bodyRot);

        float turretRot = ((stats.turretRotation % 360.0f) / 11.5f);

        int turretVal = Mathf.RoundToInt(turretRot);

        Sprite bodySprite = bodySprites.GetSet(CameraAngleController.GetCameraAngle()).GetSprite(bodyVal);
        Sprite turretSprite = turretSprites.GetSet(CameraAngleController.GetCameraAngle()).GetSprite(turretVal);

        if (bodySprite == null || turretSprite == null)
        {
            return;
        }

        //Debug.Log("Sprite is " + bodySprite.name + ", and camera angle is " + CameraAngleController.GetCameraAngle());

        bodyRenderer.sprite = bodySprite;
        turretRenderer.sprite = turretSprite;

        turretRenderer.transform.localPosition = cameraTurretOffsets[CameraAngleController.GetCameraAngle()];
    }
    private void UpdateRenderColor()
    {
        if(bodyRenderer == null || turretRenderer == null)
        {
            return;
        }

        bodyRenderer.color = tankColour;
        turretRenderer.color = turretColour;
    }
    #endregion
}
