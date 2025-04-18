using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool abilitySelected = false;
    public string selectedAbility = "";
    public Abilities abilities;
    public GridManager gridManager;
    void Start()
    {
        // Inherit all base setup if needed
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            abilitySelected = true;
            selectedAbility = "Fire Lance";
            AbilityHighlight();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            abilitySelected = true;
            selectedAbility = "Magic Missle";
            AbilityHighlight();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            abilitySelected = false;
            selectedAbility = "";
            gridManager.ClearAbilityHighlights();
        }
    }

    public void AbilityHighlight()
    {
        Debug.Log("Highlighting tiles for: " + selectedAbility);
        int abilityRange = Mathf.RoundToInt(abilities.dAbilityStats[selectedAbility]["Range"]);
        string abilityShape = abilities.dAbilityInfo[selectedAbility]["Ability Shape"];
        string abilityOrigin = abilities.dAbilityInfo[selectedAbility]["Ability Origin"];
        string abilityType = abilities.dAbilityInfo[selectedAbility]["Ability Type"];
        Vector2Int playerPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        gridManager.hightlightAbilityTiles(abilityRange, abilityShape, abilityOrigin, abilityType, playerPos);
    }

}
