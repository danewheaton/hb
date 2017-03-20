using UnityEngine;
using System.Collections;

public class LensDummy : MonoBehaviour
{

	public GameObject player1;
	public GameObject lensDummy;
	public GameObject lensCamera;

	public float playerX;
	public float playerY;
	public float playerZ;

	public float lensX;
	public float lensY;
	public float lensZ;

	public float lensDummyX;
	public float lensDummyY;
	public float lensDummyZ;

	void Start ()
	{
		player1 = GameObject.FindGameObjectWithTag ("Player");
		lensX = lensCamera.transform.position.x;
		lensY = lensCamera.transform.position.y;
		lensZ = lensCamera.transform.position.z;
	}
	
	void Update ()
	{
		LocatePlayer ();
		ReverseIt ();
	}

	void LocatePlayer ()
	{
		playerX = player1.transform.position.x;
		playerY = player1.transform.position.y;
		playerZ = player1.transform.position.z;
	}
		
	void ReverseIt()
	{
		lensDummyX = (lensX) - (playerX);
		lensDummyY = (lensY) - (playerY);
		lensDummyZ = (lensZ) - (playerZ);

//		lensDummy.transform.position = new Vector3 (-playerX, playerY, -playerZ);
		lensDummy.transform.position = new Vector3 (lensDummyX, lensDummyY, lensDummyZ);
	}

}

