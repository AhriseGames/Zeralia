using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, float>> dAbilityStats = new Dictionary<string, Dictionary<string, float>>();
    public Dictionary<string, Dictionary<string, string>> dAbilityInfo = new Dictionary<string, Dictionary<string, string>>();
    void Awake()
    {
        dAbilityStats.Add("Fire Lance", new Dictionary<string, float>
        {
            { "Base Damage", 40f },
            { "Damage Multiplier", 40f }, //this is a percentage of the character's total strength, agility, etc stat, so 40 base damage + 40% of int
            { "Mana Cost", 15f },
            { "Health Cost", 4f },//double edged sword? hp damage sounds cool
            { "Cooldown", 4f },
            { "Range", 4f },//range in tiles

        });
        dAbilityInfo.Add("Fire Lance", new Dictionary<string, string>
        {
            { "Ability Shape", "Line" },
            { "Ability Origin", "Emitting" },
            { "Ability Type", "Piercing" },//AOE? piercing? single target? DOT? delayed?
            { "Damage Type", "Magic Damage" },//physical, magic, true, mental
            { "Ability Scaling", "Wisdom" },//each ability has base damage and scaling damage like league
            { "Unlocked?", "Yes" }, //like, is it unlocked in the skill tree?
        });

        dAbilityStats.Add("Magic Missle", new Dictionary<string, float>
        {
            { "Base Damage", 60f },
            { "Damage Multiplier", 20f }, //this is a percentage of the character's total strength, agility, etc stat, so 40 base damage + 40% of int
            { "Mana Cost", 15f },
            { "Health Cost", 0f },//double edged sword? hp damage sounds cool
            { "Cooldown", 3f },
            { "Range", 6f },//range in tiles

        });
        dAbilityInfo.Add("Magic Missle", new Dictionary<string, string>
        {
            { "Ability Shape", "Line" },
            { "Ability Origin", "Emitting" },
            { "Ability Type", "First Target Hit" },//AOE? piercing? single target? DOT? delayed?
            { "Damage Type", "Magic Damage" },//physical, magic, true, mental
            { "Ability Scaling", "Wisdom" },//each ability has base damage and scaling damage like league
            { "Unlocked?", "Yes" }, //like, is it unlocked in the skill tree?
        });


    }
}
