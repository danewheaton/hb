using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinetyFive : MonoBehaviour
{

    [SerializeField]
    private GameObject[] NinetyFiveObjects;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            for (int i = 0; i < NinetyFiveObjects.Length; i++)
            {
                NinetyFiveObjects[i].SetActive(true);
            }
        }
	}
}
