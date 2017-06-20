using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public enum mirrorsActivated { NONE, MIRROR1, MIRROR2, BOTH_MIRRORS, PUZZLE_COMPLETE }

public class GabeManager : MonoBehaviour
{
    public bool playerIsStandingOnPedestal;
    mirrorsActivated currentlyActivatedMirrors = mirrorsActivated.NONE;
    public mirrorsActivated CurrentlyActivatedMirrors { get { return currentlyActivatedMirrors; } }

    public GameObject alsoYouLeftMirror, alsoYouRightMirror, alsoAlsoYouLeftMirror, alsoAlsoYouRightMirror;
    [SerializeField] Transform player, leftMirror, rightMirror, pedestal;
    public LookatTarget lookScript;
    [SerializeField] Rigidbody shard;
    [SerializeField] Transform playerTracker;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject && !playerIsStandingOnPedestal)
        {
            lookScript.m_Target = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (currentlyActivatedMirrors)
        {
            case mirrorsActivated.NONE:
                playerTracker.position = player.position;
                lookScript.m_Target = playerTracker;
                break;
            case mirrorsActivated.MIRROR1:
                lookScript.m_Target = leftMirror;
                break;
            case mirrorsActivated.MIRROR2:
                lookScript.m_Target = rightMirror;
                break;
            case mirrorsActivated.BOTH_MIRRORS:
                lookScript.m_Target = pedestal;
                break;
            default:
                playerTracker.position = player.position;
                lookScript.m_Target = playerTracker;
                break;
        }
    }

    public void ActivateMirror(mirrorsActivated mirror)
    {
        currentlyActivatedMirrors = mirror;
    }

    public IEnumerator Shake()
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
        shard.transform.parent = null;

        currentlyActivatedMirrors = mirrorsActivated.PUZZLE_COMPLETE;
        lookScript.m_Target = player;
    }
}
