using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private float distance = 5.0f;
    [SerializeField] private float height = 1.5f;
    [SerializeField] private float heightDamping = 2.0f;
    [SerializeField] private float rotationDamping = 3.0f;
    [SerializeField] private Transform target;

    private bool isAccelerating = false;

    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target)
        {
            return;
        }

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = transform.position;
        pos = target.position - currentRotation * Vector3.forward * distance;
        pos.y = currentHeight;
        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAccelerating = true;
        }
        if (isAccelerating)
        {
            distance = Mathf.Lerp(distance, 10, 10 * Time.deltaTime);
            if (distance > 9.5)
                isAccelerating = false;
        }
        else
            distance = Mathf.Lerp(distance, 5, 2 * Time.deltaTime);

        // Always look at the target
        transform.LookAt(target);
    }
}