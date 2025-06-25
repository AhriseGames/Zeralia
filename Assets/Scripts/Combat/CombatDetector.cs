using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatDetector : MonoBehaviour
{
    public Transform playerTransform;
    private List<BaseCombat> activeEnemies = new List<BaseCombat>();
    public bool battle = false;
    public bool stillInCombat = true;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (battle == false)
        {
            CheckForCombatStart();
        }

        if (battle == true)
        {
            CheckForCombatEnd();
        }

    }

    public void CheckForCombatStart()
    {
        foreach (BaseCombat enemy in activeEnemies)
        {
            Vector2Int enemyPos = new Vector2Int(Mathf.RoundToInt(enemy.transform.position.x), Mathf.RoundToInt(enemy.transform.position.y));
            Vector2Int playerPos = new Vector2Int(Mathf.RoundToInt(playerTransform.transform.position.x), Mathf.RoundToInt(playerTransform.transform.position.y));
            int dx = Mathf.Abs(enemyPos.x - playerPos.x);
            int dy = Mathf.Abs(enemyPos.y - playerPos.y);
            float euclidean = Vector2Int.Distance(enemyPos, playerPos);

            Debug.Log($"Checking enemy {enemy.name}: dx={dx}, dy={dy}, dist={euclidean}");

            if (dx <= 10 && dy <= 10 && euclidean <= 8f)
            {
                Debug.Log("Combat triggered!");
                battle = true;
                return;
            }
        }
    }


    public void CheckForCombatEnd()
    {
        foreach (BaseCombat enemy in activeEnemies)
        {
            Vector2Int enemyPos = new Vector2Int(Mathf.RoundToInt(enemy.transform.position.x), Mathf.RoundToInt(enemy.transform.position.y));
            Vector2Int playerPos = new Vector2Int(Mathf.RoundToInt(playerTransform.transform.position.x), Mathf.RoundToInt(playerTransform.transform.position.y));
            int dx = Mathf.Abs(enemyPos.x - playerPos.x);
            int dy = Mathf.Abs(enemyPos.y - playerPos.y);
            if (dx <= 10 && dy <= 10 && Vector2Int.Distance(enemyPos, playerPos) <= 10f)

            {
                return;
            }
        }
        battle = false;
        return;
    }

    public void TrackEnemy (BaseCombat spawnedEnemy)
    {
        if (!activeEnemies.Contains(spawnedEnemy))
            activeEnemies.Add(spawnedEnemy);
    }
}
