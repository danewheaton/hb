using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public enum mirrorsActivated { NONE, MIRROR1, MIRROR2, BOTH_MIRRORS }

public class GabeManager : MonoBehaviour
{
    public bool playerIsStandingOnPedestal;
    mirrorsActivated currentlyActivatedMirrors = mirrorsActivated.NONE;
    public mirrorsActivated CurrentlyActivatedMirrors { get { return currentlyActivatedMirrors; } }

    [SerializeField] Transform player, mirror1, mirror2, pedestal;
    [SerializeField] LookatTarget lookScript;
    [SerializeField] Rigidbody shard;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            
            if (!playerIsStandingOnPedestal) lookScript.m_Target = player;
            else StartCoroutine(Shake());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (currentlyActivatedMirrors)
        {
            case mirrorsActivated.NONE:
                lookScript.m_Target = null;
                break;
            case mirrorsActivated.MIRROR1:
                lookScript.m_Target = mirror1;
                break;
            case mirrorsActivated.MIRROR2:
                lookScript.m_Target = mirror2;
                break;
            case mirrorsActivated.BOTH_MIRRORS:
                lookScript.m_Target = pedestal;
                break;
            default:
                lookScript.m_Target = null;
                break;
        }
    }

    public void ActivateMirror(mirrorsActivated mirror)
    {
        currentlyActivatedMirrors = mirror;
    }

    IEnumerator Shake()
    {
        float timer = 2;
        float elapsedTime = 0;

        Vector3 originalPos = lookScript.transform.position;

        while (elapsedTime < timer)
        {
            lookScript.transform.position = originalPos + Random.insideUnitSphere * .2f;

            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }

        shard.isKinematic = false;
    }
}
