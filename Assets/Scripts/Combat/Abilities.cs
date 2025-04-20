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
            { "Status Chance", 0f },
            { "Range", 15f },//range in tiles

        });
        dAbilityInfo.Add("Fire Lance", new Dictionary<string, string>
{
            { "Target Shape", "Line" },              // How you aim: Line from player, point on mouse, etc.
            { "Ability Origin", "Emitting" },        // "Emitting" = from player, "Mouse" = from mouse pos
            { "Area Pattern", "Line" },              // The shape of the effect
            { "Area Size", "4" },                    // How far or big (e.g., 4 tiles long)
            { "Ability Type", "AOE" },          // Target logic: Piercing, First Hit, All, etc.
            { "Damage Type", "Magic Damage" },       // Physical, Magic, True, Mental
            { "Ability Scaling", "Wisdom" },         // Stat used to scale damage
            { "Status", "Burn" },                    // Optional effect: Burn, Freeze, etc.
            { "Unlocked?", "Yes" }                   // For your skill tree
        });

        dAbilityStats.Add("Magic Missle", new Dictionary<string, float>
        {
            { "Base Damage", 60f },
            { "Damage Multiplier", 20f }, //this is a percentage of the character's total strength, agility, etc stat, so 40 base damage + 40% of int
            { "Mana Cost", 15f },
            { "Health Cost", 0f },//double edged sword? hp damage sounds cool
            { "Cooldown", 3f },
            { "Status Chance", 0f },
            { "Range", 6f },//range in tiles

        });
        dAbilityInfo.Add("Magic Missle", new Dictionary<string, string>
        {
            { "Ability Shape", "Line" },
            { "Ability Origin", "Emitting" },
            { "Ability Type", "Single" },//AOE? piercing? single target? DOT? delayed?
            { "Damage Type", "Magic Damage" },//physical, magic, true, mental
            { "Ability Scaling", "Wisdom" },//each ability has base damage and scaling damage like league
            { "Status", "Burn" },
            { "Unlocked?", "Yes" }, //like, is it unlocked in the skill tree?
        });


    }
}
