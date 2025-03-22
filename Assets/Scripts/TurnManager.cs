using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public PriorityQueue<BaseCombat> priorityCombatant = new PriorityQueue<BaseCombat>();
    int characterMovementSpeed;
    public bool battle = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeCombatants();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeCombatants()
    {
        BaseCombat[] combatantsFound = FindObjectsByType<BaseCombat>(FindObjectsSortMode.None);
        foreach (BaseCombat combatant in combatantsFound)
        {
            characterMovementSpeed = Mathf.RoundToInt(combatant.characterStats.dCharacterStats[combatant.characterName]["Movement Speed"]);
            priorityCombatant.Enqueue(combatant, characterMovementSpeed);
        }

    }
}
