using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MinimapController : MonoBehaviour
{
    public GameObject[] objectsToTrack;
    public Image symbolPrefab;
    public Sprite symbolSprite;

    public Vector2 minimapSize = new Vector2(200, 200); // Adjust as per your minimap size

    private void Start()
    {
        // Iterate through objects and add symbols to the minimap
        foreach (GameObject obj in objectsToTrack)
        {
            AddSymbolToMinimap(obj.transform.position);
        }
    }

    void AddSymbolToMinimap(Vector3 position)
    {
        // Calculate position on the minimap
        Vector2 normalizedPosition = new Vector2((position.x - transform.position.x) / minimapSize.x,
                                                 (position.z - transform.position.z) / minimapSize.y);
        Vector3 minimapPosition = new Vector3(normalizedPosition.x * this.gameObject.GetComponent<RectTransform>().rect.width,
                                               normalizedPosition.y * this.gameObject.GetComponent<RectTransform>().rect.height,
                                               0f);

        // Instantiate symbol on the minimap
        Image symbol = Instantiate(symbolPrefab, transform);
        symbol.rectTransform.localPosition = minimapPosition;
    }
}