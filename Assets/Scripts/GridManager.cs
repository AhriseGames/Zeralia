using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridWidth = 64;
    public int gridHeight = 64;
    public GameObject tilePrefab;
    public GameObject mouseHighlightPrefab;
    public GameObject currentMouseTile;
    public GameObject currentAbilityMouseTile;
    public Sprite highlightAbilityRange;

    [Header("Refs")]
    public PlayerCombat playerCombat;

    private Vector3 currentMousePosition;
    public int gridX, gridY;
    public Vector2Int vMousePosition;
    private GameObject[,] tileGrid;
    private int oldValueX = 0, oldValueY = 0;

    private List<GameObject> activeHighlightTiles = new List<GameObject>();
    private List<Vector2Int> validAbilityTargetTiles = new List<Vector2Int>();

    private int abilityRange;
    private string abilityShape;
    private string abilityOrigin;
    private string abilityType;
    private string abilitySize;
    public Vector2Int abilityStartPos;
    public Vector2Int abilityEndPos;
    public Vector2Int playerPosition;
    public List<Vector2Int> playerToMousePathTileCoords = new List<Vector2Int>();
    Vector2Int lastStart = new Vector2Int();
    Vector2Int lastEnd = new Vector2Int();
    public List<GameObject> currentTrajectoryTiles = new List<GameObject>();

    void Start()
    {
        tileGrid = new GameObject[gridWidth, gridHeight];
        for (int i = 0; i < gridWidth; i++)
        {
            for (int x = 0; x < gridHeight; x++)
            {
                Vector3 spawnTileHere = new Vector3(i, x, 0);
                tileGrid[i, x] = Instantiate(tilePrefab, spawnTileHere, Quaternion.identity);
            }
        }

        currentMouseTile = Instantiate(mouseHighlightPrefab, Vector3.zero, Quaternion.identity);
        currentMouseTile.SetActive(false);

        currentAbilityMouseTile = Instantiate(currentAbilityMouseTile, Vector3.zero, Quaternion.identity);
        currentAbilityMouseTile.SetActive(false);
        Debug.Log("Total Tiles: " + (gridWidth * gridHeight));
    }

    void Update()
    {
        currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gridX = Mathf.FloorToInt(currentMousePosition.x + 0.5f);
        gridY = Mathf.FloorToInt(currentMousePosition.y + 0.5f);
        vMousePosition = new Vector2Int(gridX, gridY);

        if (!playerCombat.abilitySelected)
        {
            if (gridX >= 0 && gridX < gridWidth && gridY >= 0 && gridY < gridHeight)
            {
                Vector3 mouseHighlight = new Vector3(gridX, gridY, 0);
                currentMouseTile.transform.position = mouseHighlight;
                currentMouseTile.SetActive(true);
            }
            else
            {
                currentMouseTile.SetActive(false);
            }

            currentAbilityMouseTile.SetActive(false);
        }
        else
        {
            validAbilityTargetTiles.Clear();

            foreach (GameObject tile in activeHighlightTiles)
            {
                Vector2Int tileGridPos = new Vector2Int(Mathf.RoundToInt(tile.transform.position.x), Mathf.RoundToInt(tile.transform.position.y));
                validAbilityTargetTiles.Add(tileGridPos);
            }

            if (gridX >= 0 && gridX < gridWidth && gridY >= 0 && gridY < gridHeight && validAbilityTargetTiles.Contains(vMousePosition))
            {
                highlightAbilityAffectArea(abilityRange, abilityShape, abilityOrigin, abilityType, vMousePosition, playerPosition, abilitySize);
            }
            else
            {
                currentAbilityMouseTile.SetActive(false);
            }

            currentMouseTile.SetActive(false);
        }
    }

    public void checkCurrentHighlightedTile(int currentTileX, int currentTileY)
    {
        if (currentTileX != oldValueX || currentTileY != oldValueY)
        {
            tileGrid[oldValueX, oldValueY].GetComponent<SpriteRenderer>().color = Color.white;
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

    public void hightlightAbilityTiles(int aRange, string aShape, string aOrigin, string aType, Vector2Int pPos, string aSize)
    {
        abilityRange = aRange;
        abilityShape = aShape;
        abilityOrigin = aOrigin;
        abilityType = aType;
        abilitySize = aSize;
        playerPosition = pPos;

        hightlightAbilityRangeTiles(aRange, aShape, aOrigin, aType, pPos, aSize);
    }

    public void hightlightAbilityRangeTiles(int aRange, string aShape, string aOrigin, string aType, Vector2Int pPos, string aSize)
    {
        aRange += 1;

        for (int x = -aRange + 1; x < aRange; x++)
        {
            for (int y = -aRange + 1; y < aRange; y++)
            {
                if (Mathf.Abs(x) + Mathf.Abs(y) <= aRange - 1)
                {
                    int newX = pPos.x + x;
                    int newY = pPos.y + y;

                    if (newX < 0 || newY < 0 || newX >= gridWidth || newY >= gridHeight)
                    {

                    }
                    else
                    {
                        Vector3 spawnPos = new Vector3(newX, newY, 0);
                        GameObject highlightTile = Instantiate(tilePrefab, spawnPos, Quaternion.identity);
                        highlightTile.GetComponent<SpriteRenderer>().sprite = highlightAbilityRange;
                        activeHighlightTiles.Add(highlightTile);
                    }
                }
            }
        }
    }

    public void highlightAbilityAffectArea(int aRange, string aShape, string aOrigin, string aType, Vector2Int targetPos, Vector2Int playerPositionAbilityCast, string aSize)
    {
        Vector2Int start = playerPositionAbilityCast;
        Vector2Int end = targetPos;
        abilityStartPos = start;
        abilityEndPos = end;


        int dx = Mathf.Abs(end.x - start.x);
        int dy = Mathf.Abs(end.y - start.y);

        int stepX = (start.x < end.x) ? 1 : -1;
        int stepY = (start.y < end.y) ? 1 : -1;

        int err = dx - dy;

        int currentX = start.x;
        int currentY = start.y;

        if (lastStart != playerPositionAbilityCast || lastEnd != targetPos)
        {
            ClearMouseTrajectoryVector();
            playerToMousePathTileCoords.Clear();

            for (int i = 0; i <= abilityRange; i++)
            {
                Vector3 spawnPos = new Vector3(currentX, currentY, 0);
                currentAbilityMouseTile.transform.position = spawnPos;
                currentAbilityMouseTile.SetActive(true);

                if (currentX == end.x && currentY == end.y) break;

                int e2 = 2 * err;

                if (e2 > -dy)
                {
                    err -= dy;
                    currentX += stepX;
                }

                if (e2 < dx)
                {
                    err += dx;
                    currentY += stepY;
                }
                Vector2Int currentCoords = new Vector2Int(currentX, currentY);
                playerToMousePathTileCoords.Add(currentCoords);
            }

            foreach (Vector2Int tile in playerToMousePathTileCoords)
            {
                Vector3 vect3Tile = new Vector3(tile.x, tile.y, 0);
                GameObject holdActiveMouseTiles = Instantiate(currentAbilityMouseTile, vect3Tile, Quaternion.identity);
                currentTrajectoryTiles.Add(holdActiveMouseTiles);
            }

            lastStart = playerPositionAbilityCast;
            lastEnd = targetPos;
            Debug.Log(string.Join(", ", playerToMousePathTileCoords));
        }
    }

    public void ClearMouseTrajectoryVector()
    {
        foreach (GameObject oldMouseHighligtedTiles in currentTrajectoryTiles)
        {
            Destroy(oldMouseHighligtedTiles);
        }
        currentTrajectoryTiles.Clear();
    }

    public List<Vector2Int> GetCurrentAbilityPath()
    {
        return new List<Vector2Int>(playerToMousePathTileCoords);
    }
}
