using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed, reverseMoveSpeed, turnSpeed, gravity;
    [SerializeField] Rigidbody rb;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] Transform groundReference;

    private float speed, currentSpeed;
    private float rotate, currentRotation;

    void Update()
    {
        transform.position = rb.transform.position;

        float verticalInput = joystick.Vertical;
        float horizontalInput = joystick.Horizontal;

        if (verticalInput != 0)
        {
            speed = verticalInput > 0f ? moveSpeed : -1 * reverseMoveSpeed;
        }

        if (horizontalInput != 0)
        {
            int direction = horizontalInput > 0f ? 1 : -1;
            float amount = Mathf.Abs(horizontalInput);
            Steer(direction, amount);
        }

        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, 12f * Time.deltaTime);
        speed = 0f;
        currentRotation = Mathf.Lerp(currentRotation, rotate, 4f * Time.deltaTime);
        rotate = 0f;
    }

    void FixedUpdate()
    {
        Vector3 downDirection = -groundReference.up; // Get the downward direction from the reference object
        float rotationX = groundReference.eulerAngles.x;

       rb.AddForce(transform.right * currentSpeed, ForceMode.Acceleration);
       rb.AddForce(downDirection * gravity, ForceMode.Acceleration);
       transform.eulerAngles = Vector3.Lerp(
          transform.eulerAngles,
          new Vector3(rotationX, transform.eulerAngles.y + currentRotation, 0f), 5 * Time.deltaTime);
    }

    void Steer(int direction, float amount)
    {
        rotate = (turnSpeed * (speed < 0f ? -1 * direction : direction)) * amount;
    }
}
