using UnityEngine;
using System.Collections;

public class Donut : MonoBehaviour
{
    [SerializeField] float donutRotationSpeed = 30;
	void Update ()
    {
        if (!Credits.Won) transform.Rotate(Vector3.right, donutRotationSpeed * Time.deltaTime);
    }
}
