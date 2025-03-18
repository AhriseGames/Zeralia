using TMPro;
using UnityEngine;
using System.Collections;

public class BaseCombat : MonoBehaviour
{
    public string characterName; // ADD THIS!!
    float enemyCurrentHealth;
    float enemyMaxHealth;
    public CharacterStats characterStats;
    public TextMeshProUGUI enemyHealthText;

    void Start()
    {
        characterStats = FindFirstObjectByType<CharacterStats>();
        GrabEnemyStats();
        UpdateHealthText();
    }

    public void GrabEnemyStats()
    {
        enemyCurrentHealth = characterStats.dCharacterStats[characterName]["Health"];
        enemyMaxHealth = characterStats.dCharacterStats[characterName]["Health"];
    }

    public void TakeDamage(float damage)
    {
        if (enemyCurrentHealth > 0)
        {
            Debug.Log("Pre Damage Skeleton Health: " + enemyCurrentHealth);
            enemyCurrentHealth -= damage;
            Debug.Log("Post Damage Skeleton Health: " + enemyCurrentHealth);
            UpdateHealthText();

            if (enemyCurrentHealth <= 0)
            {
                StartCoroutine(CharacterDeath());
            }
        }
    }

    void UpdateHealthText()
    {
        enemyHealthText.text = enemyCurrentHealth + " / " + enemyMaxHealth;
    }

    public IEnumerator CharacterDeath()
    {
        enemyHealthText.text = "DEAD";
        yield return new WaitForSeconds(.5f);
        GetComponent<Collider2D>().enabled = false;
        enemyHealthText.text = "";
        Destroy(gameObject);
    }
}
