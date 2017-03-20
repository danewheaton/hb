using UnityEngine;
using System.Collections;

public class LensTrigger : MonoBehaviour
{
	public LensRaycast lensRay;

	void Start()
	{
		
	}

	void OnTriggerStay(Collider player)
	{

		if (player.gameObject.tag == "Player")
		{
			lensRay.isInFrontOfLens ();
		}
	}

	void OnTriggerExit(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			lensRay.isNotInFrontOfLens ();
		}
	}

}
