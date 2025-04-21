using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool abilitySelected = false;
    public string selectedAbility = "";
    public Abilities abilities;
    public GridManager gridManager;
    private BaseCombat baseCombat;
    public AbilityAnimation abilityAnimation;
    public List<BaseCombat> lNpcsHit = new List<BaseCombat>();
    [SerializeField] private EnemyManager enemyManager;

    void Start()
    {
        baseCombat = GetComponent<BaseCombat>();
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
            gridManager.ClearMouseTrajectoryVector();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && abilitySelected == true)
        {
            StartCoroutine(DelayedFireSpell());
        }
    }

    private IEnumerator DelayedFireSpell()
    {
        yield return new WaitForEndOfFrame(); // ensures Update finishes, path is filled
        List<Vector2Int> cachedPath = gridManager.GetCurrentAbilityPath();
        Debug.Log("We're in Delayed Fire Spell");
        StartCoroutine(abilityAnimation.animateSpell(selectedAbility, gridManager.abilityStartPos, gridManager.abilityEndPos, "vectorAToB"));
        yield return StartCoroutine(FireSpell(cachedPath));
    }

    public IEnumerator FireSpell(List<Vector2Int> lAbilityPath)
    {
        yield return new WaitForSeconds(0.05f);
        foreach (Vector2Int tileToCheckForEnemy in lAbilityPath)
        {
            foreach (GameObject activeEnemy in enemyManager.activeEnemies)
            {
                Vector2Int activeEnemyPosition = new Vector2Int(
                    Mathf.RoundToInt(activeEnemy.transform.position.x),
                    Mathf.RoundToInt(activeEnemy.transform.position.y));

                Debug.Log("Tile: " + tileToCheckForEnemy + " vs Enemy Pos: " + activeEnemyPosition);
                if (activeEnemyPosition == tileToCheckForEnemy)
                {
                    BaseCombat enemyHit = activeEnemy.GetComponent<BaseCombat>();
                    baseCombat.CombatCalculation(enemyHit, selectedAbility);
                }
            }
        }

        abilitySelected = false;
        selectedAbility = "";
        gridManager.ClearAbilityHighlights();
        gridManager.ClearMouseTrajectoryVector();
    }

    public void AbilityHighlight()
    {
        Debug.Log("Highlighting tiles for: " + selectedAbility);

        int abilityRange = Mathf.RoundToInt(abilities.dAbilityStats[selectedAbility]["Range"]);
        string targetShape = abilities.dAbilityInfo[selectedAbility]["Target Shape"];
        string abilityOrigin = abilities.dAbilityInfo[selectedAbility]["Ability Origin"];
        string areaPattern = abilities.dAbilityInfo[selectedAbility]["Area Pattern"];
        string areaSize = abilities.dAbilityInfo[selectedAbility]["Area Size"];
        string abilityType = abilities.dAbilityInfo[selectedAbility]["Ability Type"];

        Vector2Int playerPos = new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y)
        );

        gridManager.hightlightAbilityTiles(abilityRange, areaPattern, abilityOrigin, abilityType, playerPos, areaSize);
    }
}
