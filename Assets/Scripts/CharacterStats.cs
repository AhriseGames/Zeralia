using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, float>> dCharacterStats = new Dictionary<string, Dictionary<string, float>>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dCharacterStats.Add("Player", new Dictionary<string, float>
{
    { "Health", 100f },
    { "Mana", 50f },
    { "Strength", 10f },
    { "Agility", 5f },
    { "Wisdom", 7f },
    { "Ego", 6f },
    { "Toughness", 8f },
    { "Willpower", 9f },
    { "Critical Chance", 5f },
    { "Critical Damage", 150f },
    { "Perception", 4f },
    { "Block Chance", 10f },
    { "Armor Penetration", 2f },
    { "Magic Penetration", 3f },
    { "Armor", 2f },
    { "Magic Resist", 3f }
});

        dCharacterStats.Add("Skeleton", new Dictionary<string, float>
{
    { "Health", 100f },
    { "Mana", 50f },
    { "Strength", 10f },
    { "Agility", 5f },
    { "Wisdom", 7f },
    { "Ego", 6f },
    { "Toughness", 8f },
    { "Willpower", 9f },
    { "Critical Chance", 5f },
    { "Critical Damage", 150f },
    { "Perception", 4f },
    { "Block Chance", 10f },
    { "Armor Penetration", 2f },
    { "Magic Penetration", 3f },
    { "Armor", 2f },
    { "Magic Resist", 3f }
});

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
