using UnityEngine;

public class PlayerCombat : BaseCombat
{
    public bool abilitySelected = false;
    public string selectedAbility = "";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            abilitySelected = true;
            selectedAbility = "Fire Lance";
            AbilityHighlight();
        }
    }

    public void AbilityHighlight()
    {

    }
}
