using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "ScriptableObjects/ObstacleData", order = 1)]
public class ObstacleData : ScriptableObject
{
    [SerializeField]
    private bool[] obstacleGrid = new bool[100]; // 10x10 grid represented as a 1D array

    // Static reference to the instance
    private static ObstacleData instance;

    // Property to access the instance
    public static ObstacleData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<ObstacleData>("ObstacleData");
                if (instance == null)
                {
                    Debug.LogError("ObstacleData instance not found. Ensure you have created an ObstacleData asset and placed it in the Resources folder.");
                }
            }
            return instance;
        }
    }

    public bool IsTileBlocked(int x, int z)
    {
        return obstacleGrid[x + z * 10];
    }

    public void SetTileBlocked(int x, int z, bool value)
    {
        obstacleGrid[x + z * 10] = value;
    }
}
