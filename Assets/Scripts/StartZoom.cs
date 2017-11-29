using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartZoom : MonoBehaviour
{
    [SerializeField] vp_FPCamera cam;

    private bool hasClicked;
    private bool isClicking;

    private void Start()
    {
        cam = GetComponentInChildren<vp_FPCamera>();
        cam.SetState("StartZoom");
    }

    private void Update()
    {
        GetClickInput();
        UpdateFOV();
    }

    private void GetClickInput()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            hasClicked = true;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            isClicking = true;
        }

        else
        {
            isClicking = false;
        }
    }

    private void UpdateFOV()
    {
        //if (!hasClicked)
        //{
        //    //cam.RenderingFieldOfView = 30;
        //}

        if (hasClicked && !isClicking)
        {
            //cam.RenderingFieldOfView = 60;
            cam.StateManager.Reset();
        }
    }
}
