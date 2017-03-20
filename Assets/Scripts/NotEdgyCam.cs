using UnityEngine;
using System.Collections;

public class NotEdgyCam : MonoBehaviour
{
	void Update ()
	{
		//transform.position = Camera.main.transform.position;
		transform.rotation = Camera.main.transform.rotation;
	}
		
}
