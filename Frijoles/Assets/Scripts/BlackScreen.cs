using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{

    [SerializeField] GameObject blackScreen;
    [SerializeField] int fadeSpeed;
    [SerializeField] float totalBlackDelay;

    public void StartFade()
    {
        StartCoroutine(FadeOutAndIn());
    }

    private IEnumerator FadeOutAndIn()
    {
        Color color = blackScreen.GetComponent<Image>().color;
        float fadeAmount;

        while (blackScreen.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = color.a + (fadeSpeed * Time.deltaTime);

            color = new Color(color.r, color.g, color.b, fadeAmount);
            blackScreen.GetComponent<Image>().color = color;
            yield return null;
        }

        yield return new WaitForSeconds(totalBlackDelay);

        while (blackScreen.GetComponent<Image>().color.a > 0)
        {
            fadeAmount = color.a - (fadeSpeed * Time.deltaTime);

            color = new Color(color.r, color.g, color.b, fadeAmount);
            blackScreen.GetComponent<Image>().color = color;
            yield return null;
        }
    }

}
