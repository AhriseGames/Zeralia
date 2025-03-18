using TMPro;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    float enemyHealth;
    public CharacterStats characterStats;
    public TextMeshProUGUI enemyHealthText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GrabEnemyStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GrabEnemyStats()
    {
        enemyHealth = characterStats.dCharacterStats["Skeleton"]["Health"];
    }

    public void TakeDamage(float damage)
    {
        if (enemyHealth > 0)
        {
            
            Debug.Log("Pre Damage Skeleton Health: " + enemyHealth);
            enemyHealth = enemyHealth - damage;
            Debug.Log("Post Damage Skeleton Health: " + enemyHealth);
            enemyHealthText.text = "" + enemyHealth;
            if (enemyHealth <= 0)
            {
                Debug.Log("The Skellington has DIED");
                enemyHealthText.text = "DEAD";
            }
        }
        else
        {
            Debug.Log("The Skeleton is Dead.");
        }
    }
}
