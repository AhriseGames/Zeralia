using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 8;
    public GameObject tilePrefab;
    Vector3 currentMousePosition;
    public GameObject[,] tileGrid;
    int oldValueX = 0;
    int oldValueY = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tileGrid = new GameObject[gridWidth, gridHeight];

        for (int i = 0; i < gridWidth; i++)
        {
            for (int x = 0; x < gridHeight; x++)
            {
                Vector3 spawnTileHere = new Vector3(i, x, 0);
                tileGrid[i,x] = Instantiate(tilePrefab, spawnTileHere, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 1. Convert mouse position
        currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int gridX = Mathf.FloorToInt(currentMousePosition.x + 0.5f);
        int gridY = Mathf.FloorToInt(currentMousePosition.y + 0.5f);




        // 3. Highlight hovered tile (if in bounds)
        if (gridX >= 0 && gridX < gridWidth && gridY >= 0 && gridY < gridHeight)
        {
            tileGrid[gridX, gridY].GetComponent<SpriteRenderer>().color = Color.yellow;
            int gridXHighlight = gridX;
            int gridYHighlight = gridY;
            checkCurrentHighlightedTile(gridX, gridY);
        }


    }

    public void checkCurrentHighlightedTile(int currentTileX, int currentTileY)
    {
        if (currentTileX != oldValueX || currentTileY != oldValueY)
        {
            tileGrid[oldValueX,oldValueY].GetComponent<SpriteRenderer>().color = Color.white;
            oldValueX = currentTileX;
            oldValueY = currentTileY;
        }
        
    }
}
