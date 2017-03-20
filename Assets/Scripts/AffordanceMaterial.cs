using UnityEngine;
using System.Collections;

public class AffordanceMaterial : MonoBehaviour
{
    GameObject player;
    Renderer myRenderer;
    //Color originalColor;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myRenderer = GetComponent<Renderer>();
        //originalColor = myRenderer.material.color;
    }

    void Update()
    {
        float playerAngle = Vector3.Angle(transform.position, player.transform.eulerAngles);

        if (playerAngle <= Camera.main.fieldOfView)
            myRenderer.material.color = Color.Lerp(myRenderer.material.color, Color.clear, Time.deltaTime * playerAngle);
    }
}
