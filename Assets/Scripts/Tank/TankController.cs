using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TankController : MonoBehaviour
{
    [Header("Visuals")]
    public TankVisuals visuals = null;

    [Header("Rotation")]
    public float rotation = 0.0f;
    public float rotateSpeed = 50.0f;

    [Header("Movement")]
    public float moveSpeed = 1.0f;

    private Vector2 moveInput = Vector2.zero;

    private void Awake()
    {
        visuals = GetComponent<TankVisuals>();

        if(visuals == null)
        {
            Debug.LogError("ERROR - Could not locate tank visuals");
        }
    }

    #region Movement
    private void FixedUpdate()
    {
        Rotate();

        Move();
    }
    private void Rotate()
    {
        rotation += rotateSpeed * moveInput.x * Time.fixedDeltaTime;

        if(rotation < 0)
        {
            rotation += 360;
        }
        else if(rotation > 360)
        {
            rotation = rotation % 360;
        }

        visuals.SetBodyRotation(rotation);
    }
    private void Move()
    {
        Vector3 forwards = new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad), -Mathf.Sin(rotation * Mathf.Deg2Rad), 0.0f).normalized;

        transform.position += forwards * moveSpeed * Time.fixedDeltaTime;
    }
    #endregion

    #region Event Recievers
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    #endregion
}
