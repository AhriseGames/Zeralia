using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public CharacterStats characterStats;
    public Dictionary<string, List<GameObject>> allEnemies = new Dictionary<string, List<GameObject>>();
    public List<GameObject> forestEnemies = new List<GameObject>();
    public List<GameObject> dummyEnemy = new List<GameObject>();
    public List<GameObject> activeEnemies = new List<GameObject>();

    public GameObject Skeleton;
    public GameObject Orc;
    public GameObject Elf;
    public GameObject Dwarf;
    public GameObject Goblin;

    void Awake()
    {
        forestEnemies.Add(Skeleton);
        forestEnemies.Add(Orc);
        forestEnemies.Add(Elf);
        forestEnemies.Add(Dwarf);
        forestEnemies.Add(Goblin);
        allEnemies.Add("Forest", forestEnemies);
        dummyEnemy.Add(Skeleton);
        allEnemies.Add("Dummy", dummyEnemy);
    }
    void Start()
    {
        //StartCoroutine(SpawnEnemies("Forest"));
        StartCoroutine(SpawnEnemies("Dummy"));

    }

    public IEnumerator SpawnEnemies(string zoneName)
    {
        int spawnRandom = Random.Range(0, allEnemies[zoneName].Count);
        GameObject selectedEnemyPrefab = allEnemies[zoneName][spawnRandom];

        // Spawn enemy
        GameObject spawnedEnemy = Instantiate(selectedEnemyPrefab, new Vector3(Random.Range(25, 25), Random.Range(25, 25)), Quaternion.identity);
        activeEnemies.Add(spawnedEnemy);
        // Assign characterStats to EnemyCombat
        BaseCombat enemyCombatScript = spawnedEnemy.GetComponent<BaseCombat>();
        enemyCombatScript.characterStats = characterStats;

        yield return new WaitForSeconds(1f);
    }


}
