using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] GameObject blackSquare;

    [SerializeField] float fadeRate;
    [SerializeField] float fadeDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(FadeToBlackAndBack());
        }
    }

    // Fade to black and back out
    public IEnumerator FadeToBlackAndBack()
    {
        Debug.Log("Starting coroutine");
        Color color = blackSquare.GetComponent<SpriteRenderer>().color;
        float fadeAmount;

        while (blackSquare.GetComponent<SpriteRenderer>().color.a < 1)
        {
            Debug.Log("Fading out");

            fadeAmount = color.a + (fadeRate * Time.deltaTime);

            Color newColor = new Color(color.r, color.g, color.b, fadeAmount);
            blackSquare.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }

        Debug.Log("Finished fading out");

        yield return new WaitForSeconds(fadeDuration);

        while (blackSquare.GetComponent<SpriteRenderer>().color.a > 0)
        {
            Debug.Log("Fading in");

            fadeAmount = color.a - (fadeRate * Time.deltaTime);

            Color newColor = new Color(color.r, color.g, color.b, fadeAmount);
            blackSquare.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
    }
}
