using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public PriorityQueue<BaseCombat> priorityCombatant = new PriorityQueue<BaseCombat>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeCombatants()
    {
        var combatantsFound = FindObjectsByType<BaseCombat>(FindObjectsSortMode.None);
        foreach (BaseCombat combatant in combatantsFound)
        {

        }
        
    }
}
