using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTest : MonoBehaviour
{
    [SerializeField] Color newColor = new Color(1, 1, 1, 0);
    public KeyCode menuDropTestKey = KeyCode.M;
	public GameObject menuFloor;
    public Transform flamingo;
    public float stareTime = 3, flamingoAngle = 170;

    SpriteRenderer flamingoRenderer;

    float timer;

	void Start ()
	{
        FindObjectOfType<vp_FPController>().MotorAcceleration = 0;
        flamingoRenderer = flamingo.GetComponent<SpriteRenderer>();
    }

    void Update ()
	{
        Vector3 targetDirection = Camera.main.transform.position - flamingo.transform.position;

        if (Vector3.Angle(targetDirection, Camera.main.transform.forward) > flamingoAngle)
		{
            timer += Time.deltaTime;

            flamingoRenderer.color = Color.Lerp(flamingoRenderer.color, newColor, Time.deltaTime);

            if (timer >= stareTime)
            {
                FindObjectOfType<vp_FPController>().MotorAcceleration = 0.12f;
                menuFloor.GetComponent<MeshCollider>().enabled = false;
            }
		}
	}
}
