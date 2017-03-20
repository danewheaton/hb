using UnityEngine;
using System.Collections;

public class Glass : MonoBehaviour
{
    GameObject player;
    Renderer rend;
    Color originalColor;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rend = GetComponent<Renderer>();
        originalColor = rend.materials[0].color;
    }

    void Update()
    {
        //Vector3 targetDirection = transform.position - player.transform.position;
        //float lookAngle = Vector3.Angle(targetDirection, Camera.main.transform.eulerAngles);
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);

        float newAlpha = playerDistance - 2;
        if (newAlpha <= 0) newAlpha = 0;
        Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

        rend.materials[0].color = newColor;
    }
}
