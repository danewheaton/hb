using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardZoom : MonoBehaviour
{
    bool canEnterSecretPortal;
    public bool CanEnterSecretPortal { get { return canEnterSecretPortal; } }

    [SerializeField] float range = 2;
    [SerializeField] vp_FPCamera cam;
    [SerializeField] TextAsset newCamPreset, originalCamPreset;
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
//            cam.States[0].Preset.LoadFromTextAsset(newCamPreset);

            if (inTrigger)
            {
                canEnterSecretPortal = true;

                //FPPlayer.Zoom.TryStart();

                if (colliders.Count != 0 && !colliders[0].enabled)
                    ToggleOtherColliders(true);
            }

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.gameObject.tag == "Secret" && hit.collider.isTrigger)
                {
                    //FPPlayer.Zoom.TryStart();

                    foreach (Collider c in hit.transform.GetComponentsInChildren<Collider>())
                    {
                        if (!colliders.Contains(c) && !c.isTrigger)
                            colliders.Add(c);
                    }
                }
            }
        }
        else
        {
            canEnterSecretPortal = false;
//            cam.States[0].Preset.LoadFromTextAsset(originalCamPreset);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Secret") inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        FPPlayer.Zoom.TryStop();
        ToggleOtherColliders(false);
    }

    void ToggleOtherColliders(bool activateObject)
    {
        foreach (Collider c in colliders)
            c.enabled = activateObject ? true : false;

        if (!activateObject) colliders.Clear();
    }
}
