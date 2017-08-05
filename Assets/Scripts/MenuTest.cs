using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTest : MonoBehaviour
{
	public KeyCode menuDropTestKey = KeyCode.M;
	public GameObject menuFloor;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		if (Input.GetKeyDown(menuDropTestKey))
		{
			FindObjectOfType<vp_FPController> ().MotorAcceleration = 0.12f;
			menuFloor.GetComponent<MeshCollider> ().enabled = false;
		}
	}
}
