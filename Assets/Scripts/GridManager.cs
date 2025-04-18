using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 30;
    public int gridHeight = 30;
    public GameObject tilePrefab;
    Vector3 currentMousePosition;
    public GameObject[,] tileGrid;
    int oldValueX = 0;
    int oldValueY = 0;
    public PlayerCombat playerCombat;
    public int gridX;
    public int gridY;
    public Sprite highlightAbilityRange;
    List<GameObject> activeHighlightTiles = new List<GameObject>();


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
        gridX = Mathf.FloorToInt(currentMousePosition.x + 0.5f);
        gridY = Mathf.FloorToInt(currentMousePosition.y + 0.5f);




        // 3. Highlight hovered tile (if in bounds)
        if (gridX >= 0 && gridX < gridWidth && gridY >= 0 && gridY < gridHeight && playerCombat.abilitySelected == false)
        {
            tileGrid[gridX, gridY].GetComponent<SpriteRenderer>().color = Color.yellow;
            int gridXHighlight = gridX;
            int gridYHighlight = gridY;
            checkCurrentHighlightedTile(gridX, gridY);
        }

        if(playerCombat.abilitySelected == true)
        {
            tileGrid[oldValueX, oldValueY].GetComponent<SpriteRenderer>().color = Color.white;
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

    public void ClearAbilityHighlights()
    {
        foreach (GameObject tile in activeHighlightTiles)
        {
            Destroy(tile);
        }
        activeHighlightTiles.Clear();
    }

    public void hightlightAbilityTiles(int aRange, string aShape, string aOrigin, string aType, Vector2Int pPos)
    {
        Vector2Int playerPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        Debug.Log("PLayer's position at the time of casting: " + pPos);
        if (aOrigin == "Emitting")
        {
            aRange += 1;
            for (int x = -aRange+1; x < aRange; x++)
            {
                for (int y = -aRange+1; y < aRange; y++)
                {
                    if (Mathf.Abs(x) + Mathf.Abs(y) <= aRange - 1)
                    {
                        if (pPos.x + x < 0 || pPos.y + y < 0 || pPos.x + x >= gridWidth || pPos.y + y >= gridHeight)
                        {
                            Debug.Log("Some tiles are out of the border but that's ok i made sure to handle that :^)");
                        }
                        else
                        {
                            // 1️ Calculate the spawn position for the highlight tile
                            Vector3 spawnPos = new Vector3(pPos.x + x, pPos.y + y, 0);

                            // 2️ Spawn the tile prefab (a red overlay or whatever)
                            GameObject highlightTile = Instantiate(tilePrefab, spawnPos, Quaternion.identity);

                            // 3️ Set the tile’s sprite to the special highlight sprite
                            highlightTile.GetComponent<SpriteRenderer>().sprite = highlightAbilityRange;

                            // 4 Store it in the list so we can clean it up later
                            activeHighlightTiles.Add(highlightTile);
                        }
                    }
                }
            }
        }
            
    }
}
