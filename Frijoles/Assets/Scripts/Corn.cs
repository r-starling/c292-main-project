using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Corn : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI upgradeText;
    [SerializeField] float textDuration;

    // Start is called before the first frame update
    void Start()
    {
        upgradeText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (upgradeText.enabled == true && textDuration > 0)
        {
            textDuration -= Time.deltaTime;
        }
        else if (textDuration < 0)
        {
            upgradeText.enabled = false;
            gameObject.SetActive(false);
        }
    }

    public void Get(GameObject player)
    {
        player.GetComponent<Player>().GetUpgrade("Dive");
        upgradeText.text = "DIVE\nPress X to dive.\nDiving negates fall damage.";
        upgradeText.enabled = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
