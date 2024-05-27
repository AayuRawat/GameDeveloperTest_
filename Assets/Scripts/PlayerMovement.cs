using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 targetPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked tile is not blocked by an obstacle
                if (!ObstacleData.Instance.IsTileBlocked((int)hit.point.x, (int)hit.point.z))
                {
                    targetPosition = hit.point;
                    isMoving = true;
                }
            }
        }

        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        float speed = 5f;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }
}
