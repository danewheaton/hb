using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardZoom : MonoBehaviour
{
    [SerializeField] float range = 2;
    vp_FPPlayerEventHandler FPPlayer;
    Transform otherObject = null;
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

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.tag == "Secret" &&
                hit.collider.isTrigger &&
                FPPlayer.CurrentWeaponName.Get() == "2Lens")
                FPPlayer.Zoom.TryStart();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Secret")
        {
            inTrigger = true;
            otherObject = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        FPPlayer.Zoom.TryStop();
    }

    public void StopZooming()
    {
        inTrigger = false;
        FPPlayer.Zoom.TryStop();
    }
}
