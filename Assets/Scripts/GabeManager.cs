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
    [SerializeField] Transform player, leftMirror, rightMirror, pedestal, leftPillar, rightPillar;
    public LookatTarget lookScript;
    [SerializeField] Rigidbody shard;
    [SerializeField] Transform playerTracker;
    [SerializeField] OrbitScript rightEye, leftEye;
    [SerializeField] float timeBetweenPillars = .5f;

    bool followingPlayer, startedShaking;
    Vector3 targetPosition, originalPos1;

    private void Start()
    {
        originalPos1 = lookScript.transform.position;
    }

    private void Update()
    {
        if (followingPlayer) targetPosition = player.position;

        playerTracker.position = Vector3.Lerp(playerTracker.position, targetPosition, 3 * Time.deltaTime);

        if (currentlyActivatedMirrors == mirrorsActivated.BOTH_MIRRORS &&
            playerIsStandingOnPedestal &&
            !startedShaking &&
            Vector3.Angle(player.transform.position - lookScript.transform.position, player.transform.forward) > 120)
        {
            StartCoroutine(Shake());
        }

        if ((currentlyActivatedMirrors == mirrorsActivated.MIRROR1 ||
            currentlyActivatedMirrors == mirrorsActivated.MIRROR2) &&
            followingPlayer)
        {
            lookScript.transform.position = originalPos1 + Random.insideUnitSphere * .02f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        followingPlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        followingPlayer = false;

        switch (currentlyActivatedMirrors)
        {
            case mirrorsActivated.NONE:
                targetPosition = player.position;
                break;
            case mirrorsActivated.MIRROR1:
                targetPosition = leftMirror.position;
                break;
            case mirrorsActivated.MIRROR2:
                targetPosition = rightMirror.position;
                break;
            case mirrorsActivated.BOTH_MIRRORS:
                targetPosition = leftPillar.position;
                InvokeRepeating("SwitchTargetBetweenLeftAndRightPillar", 0, timeBetweenPillars);
                break;
            default:
                //targetPosition = player.position;
                break;
        }
    }

    public void ActivateMirror(mirrorsActivated mirror)
    {
        currentlyActivatedMirrors = mirror;
    }

    void SwitchTargetBetweenLeftAndRightPillar()
    {
        if (targetPosition == leftPillar.position)
        {
            targetPosition = rightPillar.position;
        }
        else if (targetPosition == rightPillar.position)
        {
            targetPosition = leftPillar.position;
        }

        StartCoroutine(ShakeALittleBit());
    }

    IEnumerator ShakeALittleBit()
    {
        float elapsedTime = 0;

        Vector3 originalPos = lookScript.transform.position;

        while (elapsedTime < timeBetweenPillars)
        {
            lookScript.transform.position = originalPos + Random.insideUnitSphere * .05f;

            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }

        lookScript.transform.position = originalPos;
    }

    public IEnumerator Shake()
    {
        CancelInvoke("SwitchTargetBetweenLeftAndRightPillar");
        targetPosition = pedestal.position;
        startedShaking = true;

        float timer = 2;
        float elapsedTime = 0;

        Vector3 originalPos = lookScript.transform.position;

        while (elapsedTime < timer)
        {
            lookScript.transform.position = originalPos + Random.insideUnitSphere * .2f;

            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }

        lookScript.transform.position = originalPos;

        shard.isKinematic = false;
        shard.transform.parent = null;

        currentlyActivatedMirrors = mirrorsActivated.PUZZLE_COMPLETE;

        playerTracker.position = player.position;
        lookScript.enabled = false;
        //lookScript.m_Target = playerTracker;
        rightEye.YDPS = 0;
        leftEye.YDPS = 0;
    }
}
