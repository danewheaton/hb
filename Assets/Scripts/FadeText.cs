using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    private void OnEnable()
    {
        vp_ItemPickup.OnPickedUpObject += CallFadeOut;
    }
    private void OnDisable()
    {
        vp_ItemPickup.OnPickedUpObject -= CallFadeOut;
    }

    void CallFadeOut()
    {
        StartCoroutine(FadeOutText());
    }

    public IEnumerator FadeOutText()
    {
        yield return new WaitForSeconds(10);

        Color transparent = new Color(1, 1, 1, 0);

        float elapsedTime = 0, timer = 2;
        while (elapsedTime < timer)
        {
            GetComponent<Text>().color =
                Color.Lerp(Color.white, transparent, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        GetComponent<Text>().color = Color.clear;
    }
}
