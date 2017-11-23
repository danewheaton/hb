using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_ChangeFOV : MonoBehaviour
{
    private Transform player;
    private vp_FPCamera camScript;
    private bool hasZoomedOut;
    public Transform title;
    public float titleAngle = 60;
    public float newFov;

	void Start ()
    {
        player = GetComponentInParent<Transform>();
        camScript = GetComponent<vp_FPCamera>();
	}
	
	void Update ()
    {
        Vector3 targetDirection = title.position - Camera.main.transform.position;

        print (Vector3.Angle(targetDirection, Camera.main.transform.forward));

        if (Vector3.Angle(targetDirection, Camera.main.transform.forward) > titleAngle && !hasZoomedOut)
        {
            camScript.RenderingFieldOfView = newFov;
            Camera.main.fieldOfView = newFov;
            hasZoomedOut = true;
        }
    }
}
