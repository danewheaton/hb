using UnityEngine;
using System.Collections;

public class LensRaycast : MonoBehaviour
{
	
	public Camera playerCam;
	public Collider lensCollider;
	//public Collider doorCollider;
	public string lensLayerName;
	public float maximumDistance;
	public bool inFrontOfLens;

	void Start ()
	{
		playerCam = Camera.main;
		inFrontOfLens = false;
	}

	void FixedUpdate() 
	{
		//Ray ray1 = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));
		//RaycastHit hit;

		if (/*lensCollider.Raycast (ray1, out hit, maximumDistance) && */inFrontOfLens == true)
		{
			LensShow ();
		} 

		else
		{
			LensHide ();
		}

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			LensToggle ();
		}
	}

	// Turn on the bit using an OR operation:
	private void LensShow()
	{
		playerCam.cullingMask |= 1 << LayerMask.NameToLayer(lensLayerName);
	}

	// Turn off the bit using an AND operation with the complement of the shifted int:
	private void LensHide()
	{
		playerCam.cullingMask &=  ~(1 << LayerMask.NameToLayer(lensLayerName));
	}

	// Toggle the bit using a XOR operation:
	private void LensToggle()
	{
		playerCam.cullingMask ^= 1 << LayerMask.NameToLayer(lensLayerName);
	}

	public void isInFrontOfLens()
	{
		inFrontOfLens = true;
	}

	public void isNotInFrontOfLens()
	{
		inFrontOfLens = false;
	}

}
