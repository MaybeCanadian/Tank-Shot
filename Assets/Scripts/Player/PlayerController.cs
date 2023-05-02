using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField]
    private float rotation = 0.0f;
    [SerializeField]
    private float rotationSpeed = 0.0f;

    [Header("Movement")]
    [SerializeField]
    private float forwardSpeed = 20.0f;

    public Vector2 moveInput = Vector2.zero;

    [Header("Visuals")]
    public TankVisuals visuals;

    private void Awake()
    {
        visuals = GetComponent<TankVisuals>();

        visuals?.SetBodyRotation(rotation);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Rotate();

        Move();
    }

    private void Rotate()
    {
        rotation += rotationSpeed * moveInput.x * Time.fixedDeltaTime;

        if (rotation > 360)
        {
            rotation = rotation % 360;
        }
        else if (rotation < 0)
        {
            rotation += 360;
        }

        visuals?.SetBodyRotation(rotation);
    }
    private void Move()
    {
        Vector3 forward = new Vector3(Mathf.Cos(rotation * Mathf.Deg2Rad), -Mathf.Sin(rotation * Mathf.Deg2Rad), 0.0f);

        //forward += CameraAngleController.GetOffset();

        Vector3 directionalInput = forward.normalized * forwardSpeed * moveInput.y * Time.fixedDeltaTime;

        transform.position += directionalInput;
    }
}
