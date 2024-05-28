using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour, IAI
{
    // Public variable for the movement speed of the enemy
    public float moveSpeed = 3f;

    private Transform playerTransform; // Reference to the player's transform
    private bool isMoving = false; // Flag to indicate if the enemy is currently moving
    private Vector2Int currentGridPosition; // Current grid position of the enemy
    private Vector2Int lastPlayerGridPosition; // Last known grid position of the player

    // Start method called before the first frame update
    void Start()
    {
        // Find the player's transform by tag
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Initialize the last player grid position
        if (playerTransform != null)
        {
            lastPlayerGridPosition = new Vector2Int((int)playerTransform.position.x, (int)playerTransform.position.z);
        }

        // Initialize the current grid position of the enemy
        currentGridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.z);
    }

    // Update method called once per frame
    void Update()
    {
        // Return if the player transform is null
        if (playerTransform == null) return;

        // Get the current grid position of the player
        Vector2Int playerGridPosition = new Vector2Int((int)playerTransform.position.x, (int)playerTransform.position.z);

        // Check if the player has moved to a different grid position
        if (!isMoving && playerGridPosition != lastPlayerGridPosition)
        {
            // Move towards the player's position
            MoveTowardsPlayer(playerGridPosition);
            lastPlayerGridPosition = playerGridPosition; // Update the last known player grid position
        }
    }

    // Implementation of the MoveTowardsPlayer method from the IAI interface
    public void MoveTowardsPlayer(Vector2Int playerPosition)
    {
        // Array of adjacent tiles around the current grid position
        Vector2Int[] adjacentTiles = {
            currentGridPosition + Vector2Int.up,
            currentGridPosition + Vector2Int.down,
            currentGridPosition + Vector2Int.left,
            currentGridPosition + Vector2Int.right
        };

        // Variables to track the best tile to move towards
        Vector2Int bestTile = currentGridPosition;
        float closestDistance = float.MaxValue;

        // Iterate through the adjacent tiles
        foreach (Vector2Int tile in adjacentTiles)
        {
            // Ignore out-of-bounds tiles
            if (tile.x < 0 || tile.x >= 10 || tile.y < 0 || tile.y >= 10) continue;

            // Ignore blocked tiles
            if (ObstacleData.Instance.IsTileBlocked(tile.x, tile.y)) continue;

            // Calculate the distance to the player from the current tile
            float distance = Vector2.Distance(playerPosition, tile);

            // Update the best tile if the current tile is closer to the player
            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestTile = tile;
            }
        }

        // Move towards the best tile if it is different from the current grid position
        if (bestTile != currentGridPosition)
        {
            StartCoroutine(MoveToTile(bestTile)); // Start the coroutine to move towards the best tile
        }
    }

    // Coroutine to move the enemy towards a target tile
    private IEnumerator MoveToTile(Vector2Int targetTile)
    {
        isMoving = true; // Set the flag to indicate that the enemy is moving

        // Calculate the target position based on the target tile
        Vector3 targetPosition = new Vector3(targetTile.x, transform.position.y, targetTile.y);

        // Move towards the target position until reaching close proximity
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // Set the final position to the target position
        transform.position = targetPosition;

        // Update the current grid position to the target tile
        currentGridPosition = targetTile;

        isMoving = false; // Reset the flag to indicate that the enemy has stopped moving
    }
}
