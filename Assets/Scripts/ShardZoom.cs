﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardZoom : MonoBehaviour
{
    bool canEnterSecretPortal;
    public bool CanEnterSecretPortal { get { return canEnterSecretPortal; } }

    [SerializeField] float range = 2;
    [SerializeField] vp_FPCamera cam;
    List<Collider> colliders = new List<Collider>();
    vp_FPPlayerEventHandler FPPlayer;
    bool inTrigger;

    private void Start()
    {
        FPPlayer = GetComponent<vp_FPPlayerEventHandler>();
    }

    private void Update()
    {
        RaycastHit[] hits;
        Ray ray = new Ray(Camera.main.transform.position, transform.forward);
        hits = Physics.RaycastAll(ray, range);

        if (FPPlayer.CurrentWeaponName.Get() == "2Lens")
        {
            if (inTrigger)
            {
                canEnterSecretPortal = true;

                //FPPlayer.Zoom.TryStart();
                //cam.RenderingFieldOfView = 45;

                if (colliders.Count != 0 && !colliders[0].enabled)
                    ToggleOtherColliders(true);
            }

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.gameObject.tag == "Secret" && hit.collider.isTrigger)
                {
                    FPPlayer.Zoom.TryStart();
                    cam.RenderingFieldOfView = 45;

                    foreach (Collider c in hit.transform.GetComponentsInChildren<Collider>())
                    {
                        if (!colliders.Contains(c) && !c.isTrigger)
                            colliders.Add(c);
                    }
                }
            }
        }
        else canEnterSecretPortal = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Secret") inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        FPPlayer.Zoom.TryStop();
        cam.RenderingFieldOfView = 60;
        ToggleOtherColliders(false);
    }

    void ToggleOtherColliders(bool activateObject)
    {
        foreach (Collider c in colliders)
            c.enabled = activateObject ? true : false;

        if (!activateObject) colliders.Clear();
    }
}
