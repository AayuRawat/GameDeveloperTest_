using UnityEngine;
using TMPro;

public class TileRaycaster : MonoBehaviour
{
    public TextMeshProUGUI infoText; // refrence to the tile information

    void Update()
    {
        // create a ray from the camera through the mouse pointer
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // checks if it hit a collidwr
        if (Physics.Raycast(ray, out hit))
        {
            // getting the info from the collider
            TileInfo tileInfo = hit.collider.GetComponent<TileInfo>();
            if (tileInfo != null)
            {
                // Display tile information in a UI Text component
                infoText.text = "Tile Position: " + tileInfo.gridPosition;
            }
        }
    }
}
