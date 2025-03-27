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
    public float characterSpeed;
    public int turnCounter;
    private TurnManager turnManager;
    public bool isReadyToAct = false;
    public Sprite portraitSprite;
    public TextMeshProUGUI portraitSpeed;


    void Start()
    {
        turnManager = FindFirstObjectByType<TurnManager>();

        characterStats = FindFirstObjectByType<CharacterStats>();
        GrabEnemyStats();
        UpdateHealthText();
    }


    public void GrabEnemyStats()
    {
        enemyCurrentHealth = characterStats.dCharacterStats[characterName]["Health"];
        enemyMaxHealth = characterStats.dCharacterStats[characterName]["Health"];
        characterSpeed = characterStats.dCharacterStats[characterName]["Movement Speed"];


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

    public void TurnCounter(int moveSpeed)
    {
        if (turnCounter < 100)
        {
            turnCounter += moveSpeed;

        }
        if (turnCounter >= 100 && !turnManager.isTurnInProgress)
        {
            Debug.Log("It is: " + characterName + "'s turn to act! " + turnCounter);
            turnManager.isTurnInProgress = true;
            turnManager.activeCombatant = this;
        }
    }
}
