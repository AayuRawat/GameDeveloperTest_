using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Indicates if the player is currently moving
    private bool isMoving = false;

    // The position the player is moving towards
    private Vector3 targetPosition;

    void Update()
    {
        // Check if the left mouse button is pressed and the player is not already moving
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            // Create a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any collider
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked tile is not blocked by an obstacle
                if (!ObstacleData.Instance.IsTileBlocked((int)hit.point.x, (int)hit.point.z))
                {
                    // Set the target position to the clicked point and mark as moving
                    targetPosition = hit.point;
                    isMoving = true;
                }
            }
        }

        // If the player is marked as moving, call the MoveToTarget method
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        // Define the movement speed
        float speed = 5f;

        // Move the player towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the player has reached the target position
        if (transform.position == targetPosition)
        {
            // Mark the player as not moving
            isMoving = false;
        }
    }
}
