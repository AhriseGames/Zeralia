using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, float>> dCharacterStats = new Dictionary<string, Dictionary<string, float>>();

    void Awake()
    {
        dCharacterStats.Add("Player", new Dictionary<string, float>
        {
            { "Health", 100f },
            { "Mana", 50f },
            { "Movement Speed", 25f },
            { "Agility", 5f },
            { "Wisdom", 7f },
            { "Ego", 6f },
            { "Toughness", 8f },
            { "Willpower", 9f },
            { "Critical Chance", 5f },
            { "Critical Damage", 1.5f },
            { "Dodge Chance", 3f },
            { "Perception", 4f },
            { "Block Chance", 10f },
            { "Armor Penetration", .1f },
            { "Magic Penetration", .1f },
            { "Armor", .2f },
            { "Magic Resist", .3f }
        });
        
                dCharacterStats.Add("Orc", new Dictionary<string, float>
        {
            { "Health", 200f },
            { "Mana", 50f },
            { "Movement Speed", 15f },
            { "Agility", 5f },
            { "Wisdom", 7f },
            { "Ego", 6f },
            { "Toughness", 8f },
            { "Willpower", 9f },
            { "Critical Chance", 5f },
            { "Critical Damage", 1.5f },
            { "Dodge Chance", 3f },
            { "Perception", 4f },
            { "Block Chance", 10f },
            { "Armor Penetration", .1f },
            { "Magic Penetration", .1f },
            { "Armor", .2f },
            { "Magic Resist", .3f }
        });

        dCharacterStats.Add("Skeleton", new Dictionary<string, float>
        {
            { "Health", 100f },
            { "Mana", 50f },
            { "Movement Speed", 7f },
            { "Strength", 10f },
            { "Agility", 5f },
            { "Wisdom", 7f },
            { "Ego", 6f },
            { "Toughness", 8f },
            { "Willpower", 9f },
            { "Critical Chance", 5f },
            { "Critical Damage", 1.5f },
            { "Dodge Chance", 3f },
            { "Perception", 4f },
            { "Block Chance", 10f },
            { "Armor Penetration", .1f },
            { "Magic Penetration", .1f },
            { "Armor", .2f },
            { "Magic Resist", .3f }
        });

        dCharacterStats.Add("Dwarf", new Dictionary<string, float>
        {
            { "Health", 150f },
            { "Mana", 50f },
            { "Movement Speed", 5f },
            { "Agility", 5f },
            { "Wisdom", 7f },
            { "Ego", 6f },
            { "Toughness", 8f },
            { "Willpower", 9f },
            { "Critical Chance", 5f },
            { "Critical Damage", 1.5f },
            { "Dodge Chance", 3f },
            { "Perception", 4f },
            { "Block Chance", 10f },
            { "Armor Penetration", .1f },
            { "Magic Penetration", .1f },
            { "Armor", .2f },
            { "Magic Resist", .3f }
        });

        dCharacterStats.Add("Elf", new Dictionary<string, float>
        {
            { "Health", 50f },
            { "Mana", 50f },
            { "Movement Speed", 35f },
            { "Agility", 5f },
            { "Wisdom", 7f },
            { "Ego", 6f },
            { "Toughness", 8f },
            { "Willpower", 9f },
            { "Critical Chance", 5f },
            { "Critical Damage", 1.5f },
            { "Dodge Chance", 3f },
            { "Perception", 4f },
            { "Block Chance", 10f },
            { "Armor Penetration", .1f },
            { "Magic Penetration", .1f },
            { "Armor", .2f },
            { "Magic Resist", .3f }
        });

        dCharacterStats.Add("Goblin", new Dictionary<string, float>
        {
            { "Health", 50f },
            { "Mana", 50f },
            { "Movement Speed", 35f },
            { "Agility", 5f },
            { "Wisdom", 7f },
            { "Ego", 6f },
            { "Toughness", 8f },
            { "Willpower", 9f },
            { "Critical Chance", 5f },
            { "Critical Damage", 1.5f },
            { "Dodge Chance", 3f },
            { "Perception", 4f },
            { "Block Chance", 10f },
            { "Armor Penetration", .1f },
            { "Magic Penetration", .1f },
            { "Armor", .2f },
            { "Magic Resist", .3f }
        });


    }
}
