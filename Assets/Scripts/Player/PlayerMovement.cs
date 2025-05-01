using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float acceleration = 20f;
    public float deceleration = 25f;

    private Vector3 currentVelocity;
    private Vector3 inputDirection;
    private Vector2 moveLimitsX = new Vector2(-4.75f, 4.75f);
    private Vector2 moveLimitsZ = new Vector2(-6.5f, 6.5f);

    private Vector3 moveInput;

    void Update()
    {
        // Read input
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(inputX, 0f, inputZ).normalized;

        // Acceleration and Deceleration
        if (inputDirection.magnitude > 0)
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, inputDirection * moveSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        // Apply movement
        Vector3 moveDelta = moveInput * moveSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + currentVelocity + moveDelta;

        // Clamp movement within bounds
        newPos.x = Mathf.Clamp(newPos.x, moveLimitsX.x, moveLimitsX.y);
        newPos.z = Mathf.Clamp(newPos.z, moveLimitsZ.x, moveLimitsZ.y);

        transform.position = newPos;
    }

}
