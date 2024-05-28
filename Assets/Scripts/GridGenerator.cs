using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // Prefabs for the tiles and obstacles
    public GameObject cubePrefab;
    public GameObject obstaclePrefab;

    void Start()
    {
        // Check if the ObstacleData instance is correctly loaded
        if (ObstacleData.Instance == null)
        {
            return;
        }

        // Loop through a 10x10 grid to generate tiles and obstacles
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                // Calculate the position for the current tile
                Vector3 position = new Vector3(x, 0, z);

                // Instantiate the tile at the calculated position
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                // Assign the grid position to the TileInfo component of the instantiated tile
                cube.GetComponent<TileInfo>().gridPosition = new Vector2Int(x, z);

                // Check if the current tile should have an obstacle
                if (ObstacleData.Instance.IsTileBlocked(x, z))
                {
                    // Adjust the position to place the obstacle directly on top of the tile
                    Vector3 obstaclePosition = position + new Vector3(0, 0.5f, 0); // Adjust the y value as needed
                    // Instantiate the obstacle at the adjusted position
                    Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
                }
            }
        }
    }
}
