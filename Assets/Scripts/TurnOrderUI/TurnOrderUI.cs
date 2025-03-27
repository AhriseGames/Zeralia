using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TurnOrderUI : MonoBehaviour
{
    public TurnManager turnManager;
    public List<UnityEngine.UI.Image> turnOrder = new List<UnityEngine.UI.Image>();
    public List<TextMeshProUGUI> turnSpeedTurnCounter = new List<TextMeshProUGUI>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var combatantsUI = turnManager.priorityCombatant.GetAllElements();
        combatantsUI = combatantsUI.OrderBy(combatant =>(100f - combatant.Item.turnCounter) /combatant.Item.characterStats.dCharacterStats[combatant.Item.characterName]["Movement Speed"]).ToList();

        for (int i = 0; i < Mathf.Min(5, combatantsUI.Count); i++)
        {
            var combatant = combatantsUI[i].Item;
            turnSpeedTurnCounter[i].text = "Spd/TC:\n" + combatant.characterSpeed + " / " + combatant.turnCounter;
            var baseCombatRefPortraits = combatantsUI[i].Item;
            Sprite characterPortraitSprite = baseCombatRefPortraits.portraitSprite;
            turnOrder[i].sprite = characterPortraitSprite;
        }
    }
}
