using UnityEditor;
using UnityEngine;

public class ObstacleEditorTool : EditorWindow
{
    [MenuItem("Window/Obstacle Editor Tool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ObstacleEditorTool));
    }

    void OnGUI()
    {
        // Display toggleable buttons for each grid tile
        for (int z = 0; z < 10; z++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < 10; x++)
            {
                bool isObstacle = ObstacleData.Instance.IsTileBlocked(x, z);
                bool newIsObstacle = EditorGUILayout.Toggle(isObstacle);
                if (newIsObstacle != isObstacle)
                {
                    ObstacleData.Instance.SetTileBlocked(x, z, newIsObstacle);
                    EditorUtility.SetDirty(ObstacleData.Instance); // Mark the ScriptableObject as dirty to save changes
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
