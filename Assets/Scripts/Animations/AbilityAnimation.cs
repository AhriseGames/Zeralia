using System.Collections;
using UnityEngine;

public class AbilityAnimation : MonoBehaviour
{
    public GameObject fireLance;
    public GameObject firedAbility;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator animateSpell(string spellCasted, Vector2Int playerPosition, Vector2Int spellDestination, string typeOfSpell)
    {
        Debug.Log("Casting: " + spellCasted);
        Vector3 pPos = new Vector3(playerPosition.x, playerPosition.y);
        Vector3 sPos = new Vector3(spellDestination.x, spellDestination.y);
        GameObject firedAbility = Instantiate(fireLance, pPos, Quaternion.identity);

        RotateSpell(firedAbility, spellCasted, playerPosition, spellDestination, typeOfSpell);
        fireSpell(firedAbility, pPos, sPos);
        yield return null;
    }

    public void RotateSpell(GameObject firedAbility, string spellCasted, Vector2Int playerPosition, Vector2Int spellDestination, string typeOfSpell)
    {
        Vector2 direction = spellDestination - playerPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firedAbility.transform.rotation = Quaternion.Euler(0, 0, angle + 180); // 180 if prefab faces west
    }

    public void fireSpell(GameObject spellToFire, Vector3 playerPosition, Vector3 finalSpellPosition)
    {
        StartCoroutine(MoveSpellOverTime(spellToFire, finalSpellPosition));
    }

    public IEnumerator MoveSpellOverTime(GameObject spell, Vector3 targetPos, float speed = 10f)
    {
        while (Vector3.Distance(spell.transform.position, targetPos) > 0.01f)
        {
            spell.transform.position = Vector3.MoveTowards(spell.transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        // Snap exactly to position at the end
        spell.transform.position = targetPos;
    }

}
