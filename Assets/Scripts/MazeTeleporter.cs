using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTeleporter : MonoBehaviour
{
    [SerializeField] Transform mazePlayerStart;

    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = new Vector3
            (other.transform.position.x, mazePlayerStart.position.y, other.transform.position.z);
    }
}
