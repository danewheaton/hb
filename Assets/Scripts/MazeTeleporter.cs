using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTeleporter : MonoBehaviour
{
    [SerializeField] GameObject maze;
    [SerializeField] Transform mazePlayerStart;

    private void OnTriggerEnter(Collider other)
    {
        maze.SetActive(true);

        other.transform.position = new Vector3
            (other.transform.position.x, mazePlayerStart.position.y, other.transform.position.z);
    }
}
