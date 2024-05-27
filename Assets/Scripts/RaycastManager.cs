using UnityEngine;
using TMPro;

public class TileRaycaster : MonoBehaviour
{
    public TextMeshProUGUI infoText; 

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            TileInfo tileInfo = hit.collider.GetComponent<TileInfo>();
            if (tileInfo != null)
            {
                // Display tile information in a UI Text component
                infoText.text = "Tile Position: " + tileInfo.gridPosition;
            }
        }
    }
}
