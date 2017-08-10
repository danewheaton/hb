using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTest : MonoBehaviour
{
	public KeyCode menuDropTestKey = KeyCode.M;
	public GameObject menuFloor;
    public Transform flamingo;
    public float stareTime = 3, flamingoAngle = 170;

    float timer;

	void Start ()
	{
        FindObjectOfType<vp_FPController>().MotorAcceleration = 0;
    }

    void Update ()
	{
        Vector3 targetDirection = Camera.main.transform.position - flamingo.transform.position;

        if (Vector3.Angle(targetDirection, Camera.main.transform.forward) > flamingoAngle)
		{
            timer += Time.deltaTime;

            if (timer >= stareTime)
            {
                FindObjectOfType<vp_FPController>().MotorAcceleration = 0.12f;
                menuFloor.GetComponent<MeshCollider>().enabled = false;
            }
		}
	}
}
