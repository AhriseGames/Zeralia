using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour
{
    public PriorityQueue<BaseCombat> priorityCombatant = new PriorityQueue<BaseCombat>();
    int characterMovementSpeed;
    public bool battle = true;
    public bool isTurnInProgress = false;
    public BaseCombat activeCombatant;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f); // Tiny delay to let everything initialize
        if (battle == true)
        {
            InitializeCombatants();
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (!battle) return;

        if (isTurnInProgress)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log(activeCombatant.characterName + " has acted!");
                activeCombatant.turnCounter = 0;

                // Check if someone else is still ready to act
                bool someoneElseReady = false;
                var allCombatants = priorityCombatant.GetAllElements();

                foreach (var combatant in allCombatants)
                {
                    if (combatant.Item != activeCombatant && combatant.Item.turnCounter >= 100)
                    {
                        activeCombatant = combatant.Item;
                        Debug.Log(activeCombatant.characterName + " is now up!");
                        someoneElseReady = true;
                        break;
                    }
                }

                if (!someoneElseReady)
                {
                    isTurnInProgress = false;
                }
            }

            return; // Don't tick counters if someone is acting
        }

        // No one is acting → Tick all combatants
        var tickCombatants = priorityCombatant.GetAllElements();
        foreach (var combatant in tickCombatants)
        {
            int moveSpeed = Mathf.RoundToInt(combatant.Item.characterStats.dCharacterStats[combatant.Item.characterName]["Movement Speed"]);
            combatant.Item.TurnCounter(moveSpeed);
        }
    }



    void InitializeCombatants()
    {
        BaseCombat[] combatantsFound = FindObjectsByType<BaseCombat>(FindObjectsSortMode.None);
        foreach (BaseCombat combatant in combatantsFound)
        {
            Debug.Log(combatant + " Is now a part of the battle");
            characterMovementSpeed = Mathf.RoundToInt(combatant.characterStats.dCharacterStats[combatant.characterName]["Movement Speed"]);
            priorityCombatant.Enqueue(combatant, characterMovementSpeed);
        }

    }
}
