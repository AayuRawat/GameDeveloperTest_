using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject obstaclePrefab;

    void Start()
    {
        if (ObstacleData.Instance == null)
        {
            Debug.LogError("ObstacleData instance is null. Ensure it is correctly created and placed in the Resources folder.");
            return;
        }

        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                Vector3 position = new Vector3(x, 0, z);

                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);
                cube.GetComponent<TileInfo>().gridPosition = new Vector2Int(x, z);

                if (ObstacleData.Instance.IsTileBlocked(x, z))
                {
                    // Adjust the position to place the obstacle directly on top of the tile
                    Vector3 obstaclePosition = position + new Vector3(0, 0.5f, 0); // Adjust the y value as needed
                    Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity);
                }
            }
        }
    }
}
