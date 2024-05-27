using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour, IAI
{
    public float moveSpeed = 3f; // Speed at which the enemy moves
    private Transform playerTransform;
    private bool isMoving = false;
    private Vector2Int currentGridPosition;
    private Vector2Int playerGridPosition;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform != null)
        {
            playerGridPosition = new Vector2Int((int)playerTransform.position.x, (int)playerTransform.position.z);
        }
        currentGridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.z);
    }

    void Update()
    {
        if (!isMoving)
        {
            MoveTowardsPlayer();
        }
    }

    public void MoveTowardsPlayer()
    {
        if (playerTransform == null) return;

        Vector2Int playerPosition = new Vector2Int((int)playerTransform.position.x, (int)playerTransform.position.z);

        // Get a list of adjacent tiles
        Vector2Int[] adjacentTiles = {
            currentGridPosition + Vector2Int.up,
            currentGridPosition + Vector2Int.down,
            currentGridPosition + Vector2Int.left,
            currentGridPosition + Vector2Int.right
        };

        // Find the closest tile to the player that is not blocked
        Vector2Int bestTile = currentGridPosition;
        float closestDistance = float.MaxValue;

        foreach (Vector2Int tile in adjacentTiles)
        {
            if (tile.x < 0 || tile.x >= 10 || tile.y < 0 || tile.y >= 10) continue; // Ignore out-of-bounds tiles
            if (ObstacleData.Instance.IsTileBlocked(tile.x, tile.y)) continue; // Ignore blocked tiles

            float distance = Vector2.Distance(playerPosition, tile);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestTile = tile;
            }
        }

        // Move to the best tile
        if (bestTile != currentGridPosition)
        {
            StartCoroutine(MoveToTile(bestTile));
        }
    }

    private IEnumerator MoveToTile(Vector2Int targetTile)
    {
        isMoving = true;
        Vector3 targetPosition = new Vector3(targetTile.x, transform.position.y, targetTile.y);
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
        currentGridPosition = targetTile;
        isMoving = false;
    }
}
