using UnityEngine;
using System.Collections;

public class Wink : MonoBehaviour
{
    public Material closedEyelid;
    public float timeBetweenWinksLow;
    public float timeBetweenWinksHigh;
    public float winkDuration;
    Material openEyelid;

    void OnEnable ()
    {
        openEyelid = GetComponent<Renderer>().material;
        Invoke("RandomlyWinkEveryOnceInAWhile", Random.Range(timeBetweenWinksLow, timeBetweenWinksHigh));
	}

    void RandomlyWinkEveryOnceInAWhile()
    {
        if (isActiveAndEnabled) StartCoroutine(WaitToWink());
    }

    IEnumerator WaitToWink()
    {
        GetComponent<Renderer>().material = closedEyelid;
        yield return new WaitForSeconds(winkDuration);
        GetComponent<Renderer>().material = openEyelid;

        Invoke("RandomlyWinkEveryOnceInAWhile", Random.Range(timeBetweenWinksLow, timeBetweenWinksHigh));
    }
}
