using UnityEngine;
using Vuforia;

public class CustomPlaneFinder : MonoBehaviour
{
    private FixedJoystick joystick; // This will store the reference to your FixedJoystick.
    public PlaneFinderBehaviour behaviour; // Reference to the LogToCanvas script

    private void Awake()
    {
        joystick = FindObjectOfType<FixedJoystick>();
        if (joystick == null)
        {
            Debug.LogError("Joystick component not found in the scene.");
        }
    }

    public void CustomPerformHitTest(Vector2 position)
    {
        // Check if the joystick was clicked within the last 2 seconds.
        if (Time.time - lastClickTime <= 2f)
        {
            joystickWasClicked = true;
        }

        // Check if the joystick is currently being interacted with.
        bool isJoystickTouched = IsJoystickTouched();

        if (!isJoystickTouched && !joystickWasClicked)
        {
            // Perform base.PerformHitTest only when the joystick is not being touched
            // and it was not clicked within the last 2 seconds.
            behaviour.PerformHitTest(position);
        }

        // Reset the flag and update the lastClickTime when the joystick is not being touched.
        if (!isJoystickTouched)
        {
            joystickWasClicked = false;
            lastClickTime = Time.time;
        }
    }

    private bool IsJoystickTouched()
    {
        // Check if there is any touch or mouse input near the joystick.
        // You might need to adjust this logic based on the actual joystick's position and size.
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector2 inputPosition = Input.mousePosition; // Replace with touch position for mobile.

            Vector2 joystickCenter = joystick.transform.position;
            float joystickRadius = joystick.GetComponent<RectTransform>().rect.width / 2f;

            if (Vector2.Distance(inputPosition, joystickCenter) <= joystickRadius)
            {
                return true; // The joystick is currently being touched.
            }
        }

        return false; // The joystick is not being touched.
    }

    private bool joystickWasClicked = false;
    private float lastClickTime = 0f;

    void Update()
    {
        // Check for clicks on the joystick.
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector2 inputPosition = Input.mousePosition; // Replace with touch position for mobile.

            Vector2 joystickCenter = joystick.transform.position;
            float joystickRadius = joystick.GetComponent<RectTransform>().rect.width / 2f;

            if (Vector2.Distance(inputPosition, joystickCenter) <= joystickRadius)
            {
                // The joystick was clicked.
                joystickWasClicked = true;
                lastClickTime = Time.time;
            }
        }
    }
}
