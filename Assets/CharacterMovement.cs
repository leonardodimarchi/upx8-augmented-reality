using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public FixedJoystick joystick;
    private Transform arCameraTransform; // Reference to the AR Camera's transform

    void Start()
    {
        // Find the AR Camera in the scene
        arCameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Get input from the joystick
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Calculate the movement direction relative to the AR Camera
        Vector3 cameraForward = arCameraTransform.forward;
        Vector3 cameraRight = arCameraTransform.right;

        // Flatten the vectors (ignore vertical component) to ensure forward is parallel to the ground
        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the desired movement direction
        Vector3 movement = (cameraForward * verticalInput + cameraRight * horizontalInput) * moveSpeed * Time.deltaTime;

        // Move the character
        transform.Translate(movement);
    }
}
