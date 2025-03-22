using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public CharacterStats characterStats;
    public Dictionary<string, List<GameObject>> allEnemies = new Dictionary<string, List<GameObject>>();
    public List<GameObject> forestEnemies = new List<GameObject>();

    public GameObject Skeleton;

    void Awake()
    {
        forestEnemies.Add(Skeleton);
        allEnemies.Add("Forest", forestEnemies);

    }
    void Start()
    {
        StartCoroutine(SpawnEnemies("Forest"));
        StartCoroutine(SpawnEnemies("Forest"));
    }

    public IEnumerator SpawnEnemies(string zoneName)
    {
        int spawnRandom = Random.Range(0, allEnemies[zoneName].Count);
        GameObject selectedEnemyPrefab = allEnemies[zoneName][spawnRandom];

        // Spawn enemy
        GameObject spawnedEnemy = Instantiate(selectedEnemyPrefab, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5)), Quaternion.identity);
        // Assign characterStats to EnemyCombat
        BaseCombat enemyCombatScript = spawnedEnemy.GetComponent<BaseCombat>();
        enemyCombatScript.characterStats = characterStats;

        yield return new WaitForSeconds(1f);
    }


}
