using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed, reverseMoveSpeed, turnSpeed, gravity;
    [SerializeField] Rigidbody rb;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] GameObject groundReference;
    public CustomCanvasLogger logToCanvasScript; // Reference to the LogToCanvas script
    private float logDebounceTime = 2.0f; // Debounce time in seconds
    private bool logIsDebouncing = false;
    private bool isMobile = true;


    private float speed, currentSpeed;
    private float rotate, currentRotation;

    private void Start()
    {
        isMobile = SystemInfo.deviceType == DeviceType.Handheld;

        if (!isMobile)
        {
            GlobalVariables.hasGround = true;
        }      
    }

    void Update()
    {
        if (!GlobalVariables.hasGround && isMobile)
        {
            return;
        }

        transform.position = new Vector3(rb.transform.position.x, transform.position.y, rb.transform.position.z);

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
        Vector3 downDirection = -groundReference.transform.up; // Get the downward direction from the reference object

        // Debounced logging
        if (!logIsDebouncing && logToCanvasScript)
        {
            logToCanvasScript.HandleLog("Y: " + rb.transform.position.y, "", LogType.Log);
            logToCanvasScript.HandleLog("DownDirection: " + downDirection, "", LogType.Log);
            logIsDebouncing = true;
            StartCoroutine(DebounceCoroutine(logDebounceTime));
        }


        if (!GlobalVariables.hasGround && isMobile)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }

        rb.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

        rb.AddForce(downDirection * gravity, ForceMode.Acceleration);

        Vector3 angles = transform.eulerAngles;
        angles.x = 0;
        transform.eulerAngles = angles;

        transform.eulerAngles = Vector3.Lerp(
          transform.eulerAngles,
          new Vector3(0f, transform.eulerAngles.y + currentRotation, 0f), 5 * Time.deltaTime);
    }

    IEnumerator DebounceCoroutine(float debounceTime)
    {
        yield return new WaitForSeconds(debounceTime);
        logIsDebouncing = false;
    }

    void Steer(int direction, float amount)
    {
        rotate = (turnSpeed * (speed < 0f ? -1 * direction : direction)) * amount;
    }
}
