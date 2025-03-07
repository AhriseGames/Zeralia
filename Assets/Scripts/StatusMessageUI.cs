using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class StatusMessageUI : MonoBehaviour
{
    public TextMeshProUGUI statusMessage;
    public GameObject statusMessageCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ShowStatusMessage(string statusEffect)
    {
        Debug.Log("we're at the coroutine");
        if (statusEffect == "Blocked")
        {
            statusMessageCanvas.gameObject.SetActive(true);
            statusMessage.text = "Blocked!";
            yield return new WaitForSeconds(3f);
            statusMessage.text = "";
            statusMessageCanvas.gameObject.SetActive(false);

        }
        if (statusEffect == "Poisoned")
        {
            statusMessageCanvas.gameObject.SetActive(true);
            statusMessage.text = "";
            yield return new WaitForSeconds(3f);
        }
    }
}


