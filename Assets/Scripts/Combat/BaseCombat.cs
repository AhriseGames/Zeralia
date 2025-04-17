using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        CurrentMagicResist = liveStats["Magic Resist"];
    }

    public void CombatCalculation(BaseCombat DamageReceiver, string abilityUsed)
    {
        // 🧮 Pull numeric ability stats
        float abilityBaseDamage = abilities.dAbilityStats[abilityUsed]["Base Damage"];
        float abilityDamageMultiplier = abilities.dAbilityStats[abilityUsed]["Damage Multiplier"];
        float abilityManaCost = abilities.dAbilityStats[abilityUsed]["Mana Cost"];
        float abilityHealthCost = abilities.dAbilityStats[abilityUsed]["Health Cost"];

        // 🔤 Pull ability type info
        string abilityDamageType = abilities.dAbilityInfo[abilityUsed]["Damage Type"];
        string abilityAbilityType = abilities.dAbilityInfo[abilityUsed]["Ability Type"];
        string abilityScalingStat = abilities.dAbilityInfo[abilityUsed]["Ability Scaling"];

        // 📈 Grab attacker’s stat that scales this ability
        float characterMultiplierStat = characterStats.dCharacterStats[characterName][abilityScalingStat];

        // 🎯 Roll for critical strike
        int critRandomValue = Random.Range(0, 101);

        // 💥 Calculate total raw damage
        float damageCalculation = abilityBaseDamage + (abilityDamageMultiplier * characterMultiplierStat);

        // 💢 Apply crit bonus if landed
        if (critRandomValue <= CurrentCritChance)
        {
            damageCalculation *= CurrentCritDamage; // Example: 1.5 = +50%
        }

        // 💀 TRUE DAMAGE — ignores all defenses
        if (abilityDamageType == "True Damage")
        {
            DamageReceiver.CurrentHealth -= damageCalculation;
        }

        // 🛡️ PHYSICAL DAMAGE — reduced by armor minus **attacker's** armor pen
        if (abilityDamageType == "Physical Damage")
        {
            float effectiveArmor = DamageReceiver.CurrentArmor - CurrentArmorPen;
            DamageReceiver.CurrentHealth -= damageCalculation * (1 - effectiveArmor);
        }

        // 🧙 MAGIC DAMAGE — reduced by magic resist minus **attacker's** magic pen
        if (abilityDamageType == "Magic Damage")
        {
            float effectiveResist = DamageReceiver.CurrentMagicResist - CurrentMagicPen;
            DamageReceiver.CurrentHealth -= damageCalculation * (1 - effectiveResist);
        }

        // ✅ Optional: Clamp health to 0 to avoid negatives
        DamageReceiver.CurrentHealth = Mathf.Max(0, DamageReceiver.CurrentHealth);

        // ✅ Update their HP bar / status text
        DamageReceiver.UpdateHealthText();
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
