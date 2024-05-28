using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/ObstacleData", order = 1)]
public class ObstacleData : ScriptableObject
{
    // This array stores the obstacle data for a 10x10 grid. Each element represents whether a tile is blocked.
    [SerializeField]
    private bool[] obstacleGrid = new bool[100]; // 10x10 grid represented as a 1D array

    // Static reference to the instance of this ScriptableObject
    private static ObstacleData instance;

    // Property to access the instance
    public static ObstacleData Instance
    {
        get
        {
            if (instance == null)
            {
                // Load the instance from the Resources folder
                instance = Resources.Load<ObstacleData>("ObstacleData");
            }
            return instance;
        }
    }

    // Method to check if a specific tile is blocked
    public bool IsTileBlocked(int x, int z)
    {
        // Convert 2D coordinates to 1D index and return the value
        return obstacleGrid[x + z * 10];
    }

    // Method to set the blocked state of a specific tile
    public void SetTileBlocked(int x, int z, bool value)
    {
        // Convert 2D coordinates to 1D index and set the value
        obstacleGrid[x + z * 10] = value;
    }
}
