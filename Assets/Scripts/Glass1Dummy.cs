using UnityEngine;
using System.Collections;

public class Glass1Dummy : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = player.transform.position + new Vector3(100, 100, 100);
       // transform.rotation = player.transform.rotation;
    }
}
