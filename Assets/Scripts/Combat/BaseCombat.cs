using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;

public class BaseCombat : MonoBehaviour
{
    public string characterName; // ADD THIS!!

    public float characterSpeed;

    public float MaxHealth, MaxMana, MaxMovementSpeed, MaxAgility, MaxWisdom, MaxEgo, MaxToughness, MaxWillpower, MaxCritChance, MaxCritDamage, MaxDodgeChance, MaxPerception, MaxBlockChance, MaxArmorPen, MaxMagicPen, MaxArmor, MaxMagicResist;
    public float CurrentHealth, CurrentMana, CurrentMovementSpeed, CurrentAgility, CurrentWisdom, CurrentEgo, CurrentToughness, CurrentWillpower, CurrentCritChance, CurrentCritDamage,CurrentDodgeChance, CurrentPerception, CurrentBlockChance, CurrentArmorPen, CurrentMagicPen, CurrentArmor, CurrentMagicResist;



    public CharacterStats characterStats;
    public Abilities abilities;
    public TextMeshProUGUI HealthText;

    public int turnCounter;
    private TurnManager turnManager;
    public bool isReadyToAct = false;
    public Sprite portraitSprite;
    public TextMeshProUGUI portraitSpeed;
    public Dictionary<string, List<GameObject>> allEnemies = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, float> liveStats = new Dictionary<string, float>();



    void Start()
    {
        turnManager = FindFirstObjectByType<TurnManager>();
        characterStats = FindFirstObjectByType<CharacterStats>();
        InitializeStats();
        GrabCharacterStats();
        UpdateHealthText();
    }

    public void InitializeStats()
    {
        foreach (var stat in characterStats.dCharacterStats[characterName])
        {
            liveStats.Add(stat.Key, stat.Value);
        }
        Debug.Log("Stats added for:" + characterName);
    }

    public void GrabCharacterStats()
    {
        CurrentHealth = liveStats["Health"];
        MaxHealth = liveStats["Health"];
        MaxMana = liveStats["Mana"];
        CurrentMana = liveStats["Mana"];
        CurrentMovementSpeed = liveStats["Movement Speed"];
        CurrentCritChance = liveStats["Critical Chance"];
        CurrentMagicResist = liveStats["Magic Resistence"];
    }

    public void CombatCalculation(BaseCombat DamageReciever, string abilityUsed)
    {
        float abilityBaseDamage = abilities.dAbilityStats[abilityUsed]["Base Damage"];
        float abilityDamageMultiplier = abilities.dAbilityStats[abilityUsed]["Damage Multiplier"];
        float abilityManaCost = abilities.dAbilityStats[abilityUsed]["Mana Cost"];
        float abilityHealthCost = abilities.dAbilityStats[abilityUsed]["Health Cost"];

        string abilityDamageType = abilities.dAbilityInfo[abilityUsed]["Damage Type"];
        string abilityAbilityType = abilities.dAbilityInfo[abilityUsed]["Ability Type"];
        string abilityAbilityScaling = abilities.dAbilityInfo[abilityUsed]["Ability Scaling"];

        float characterMultiplierStat = characterStats.dCharacterStats[characterName][abilityAbilityScaling];

        int critRandomValue = Random.Range(0, 101);

        float damageCalculation = abilityBaseDamage + (abilityDamageMultiplier * characterMultiplierStat);

        if (critRandomValue <= CurrentCritChance)
        {
            damageCalculation *= CurrentCritDamage;
        }


        if (abilityDamageType == "True Damage")
        {
            DamageReciever.CurrentHealth -= damageCalculation;
        }
        if (abilityDamageType == "Physical Damage")
        {
            DamageReciever.CurrentHealth -= damageCalculation * ((1 -( DamageReciever.CurrentArmor - DamageReciever.CurrentArmorPen)));
        }
        if (abilityDamageType == "Magic Damage")
        {
            DamageReciever.CurrentHealth -= damageCalculation * ((1 - (DamageReciever.CurrentArmor - DamageReciever.CurrentMagicPen)));
        }

    }
    
    public void TakeDamage(BaseCombat DamageReciever, int damage)
    {
        if (CurrentHealth > 0)
        {
            Debug.Log("Pre Damage Health: " + CurrentHealth);
            CurrentHealth -= damage;
            Debug.Log("Post Damage Health: " + CurrentHealth);
            UpdateHealthText();

            if (CurrentHealth <= 0)
            {
                StartCoroutine(CharacterDeath());
            }
        }
    }
    void UpdateHealthText()
    {
        HealthText.text = CurrentHealth + " / " + MaxHealth;
    }

    public IEnumerator CharacterDeath()
    {
        HealthText.text = "DEAD";
        yield return new WaitForSeconds(.5f);
        GetComponent<Collider2D>().enabled = false;
        HealthText.text = "";
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
