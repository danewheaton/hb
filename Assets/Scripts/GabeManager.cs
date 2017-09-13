using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.ImageEffects;

public enum mirrorsActivated { NONE, MIRROR1, MIRROR2, BOTH_MIRRORS, PUZZLE_COMPLETE }

public class GabeManager : MonoBehaviour
{
    public bool playerIsStandingOnPedestal;
    mirrorsActivated currentlyActivatedMirrors = mirrorsActivated.NONE;
    public mirrorsActivated CurrentlyActivatedMirrors { get { return currentlyActivatedMirrors; } }

    public GameObject gabeSpeaker, alsoYouLeftMirror, alsoYouRightMirror, alsoAlsoYouLeftMirror, alsoAlsoYouRightMirror, controlsText;
    [SerializeField] Transform player, leftMirror, rightMirror, pedestal, leftPillar, rightPillar;
    public LookatTarget lookScript;
    [SerializeField] Rigidbody shard;
	[SerializeField] GameObject shardicles;
    [SerializeField] Transform playerTracker;
    [SerializeField] OrbitScript rightEye, leftEye, deadEyeRight, deadEyeLeft;
    [SerializeField] float timeBetweenPillars = .5f;
    [SerializeField] KeyCode gabeSkipButton = KeyCode.O;
    [SerializeField] AudioSource popNoise;

    bool followingPlayer, startedShaking;
    Vector3 targetPosition, originalPos1;

	public MonoBehaviour[] gabeZoneEffects;
	private float dofCurrentAperture;
	public float dofTargetAperture;
	public float dofLerpRate;

    private void Start()
    {
        originalPos1 = lookScript.transform.position;
    }

    private void Update()
    {
        if (followingPlayer) targetPosition = player.position;

		if (followingPlayer == true)
		{
			GabeZoneActivate ();
		}

		else
		{
			GabeZoneDeactivate ();	
		}

        playerTracker.position = Vector3.Lerp(playerTracker.position, targetPosition, 3 * Time.deltaTime);

        if (currentlyActivatedMirrors == mirrorsActivated.BOTH_MIRRORS &&
            playerIsStandingOnPedestal &&
            !startedShaking &&
            Vector3.Angle(player.transform.position - lookScript.transform.position, player.transform.forward) > 120)
        {
            StartCoroutine(DeathThroes());
            //Invoke("PlayPopNoise", 2);
        }

        if ((currentlyActivatedMirrors == mirrorsActivated.MIRROR1 ||
            currentlyActivatedMirrors == mirrorsActivated.MIRROR2) &&
            followingPlayer)
        {
            lookScript.transform.position = originalPos1 + Random.insideUnitSphere * .02f;
        }

        if (Application.isEditor && Input.GetKeyDown(gabeSkipButton))
        {
            StartCoroutine(DeathThroes());
           // Invoke("PlayPopNoise", 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            followingPlayer = true;
            GabeZoneActivate();
        }
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

		GabeZoneDeactivate ();
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

    public IEnumerator DeathThroes()
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

        popNoise.Play();

        lookScript.transform.position = originalPos;

        shard.isKinematic = false;
        shard.transform.parent = null;
		shardicles.SetActive (true);
        gabeSpeaker.SetActive(false);

        currentlyActivatedMirrors = mirrorsActivated.PUZZLE_COMPLETE;

        playerTracker.position = player.position;
        lookScript.enabled = false;
        //lookScript.m_Target = playerTracker;
        deadEyeLeft.YDPS = 0;
        deadEyeRight.YDPS = 0;

        rightEye.gameObject.layer = LayerMask.NameToLayer("Gabe");
        leftEye.gameObject.layer = LayerMask.NameToLayer("Gabe");
        rightEye.GetComponent<Renderer>().enabled = false;
        leftEye.GetComponent<Renderer>().enabled = false;
        deadEyeLeft.GetComponent<Renderer>().enabled = true;
        deadEyeRight.GetComponent<Renderer>().enabled = true;
    }

	void GabeZoneActivate()
	{
//		for (int i = 0; i < (gabeZoneEffects.Length); i++)
//		{
//			gabeZoneEffects [i].enabled = true;
//		}
		dofCurrentAperture = Camera.main.GetComponent<DepthOfField>().aperture;
		Camera.main.GetComponent<DepthOfField>().aperture = Mathf.Lerp(dofCurrentAperture, dofTargetAperture, dofLerpRate);
	}

	void GabeZoneDeactivate()
	{
//		for (int i = 0; i < (gabeZoneEffects.Length); i++)
//		{
//			gabeZoneEffects [i].enabled = false;
//		}
		dofCurrentAperture = Camera.main.GetComponent<DepthOfField>().aperture;
		Camera.main.GetComponent<DepthOfField>().aperture = Mathf.Lerp(dofCurrentAperture, 0, dofLerpRate);

	}

    //void PlayPopNoise()
    //{
    //    popNoise.Play();
    //}
}
