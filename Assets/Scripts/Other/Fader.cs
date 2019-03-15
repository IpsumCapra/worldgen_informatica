using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public float fadeTime = 2.5f;
    public Canvas imgCanvas;

    public YieldInstruction fadeInstruction = new YieldInstruction();
    public IEnumerator FadeOut()
    {
        Image img = imgCanvas.transform.GetChild(0).GetComponent<Image>();
        float elapsedTime = 0.0f;
        Color c = imgCanvas.transform.GetChild(0).GetComponent<Image>().color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
            img.color = c;
        }
        if (c.a == 0)
            imgCanvas.gameObject.SetActive(false);
    }

    public IEnumerator FadeIn()
    {
        imgCanvas.gameObject.SetActive(true);
        Image img = imgCanvas.transform.GetChild(0).GetComponent<Image>();
        float elapsedTime = 0.0f;
        Color c = img.color;
        while (elapsedTime < fadeTime)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / fadeTime);
            img.color = c;
        }
    }


}



