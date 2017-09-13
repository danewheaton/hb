using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_ChairPuzzle : MonoBehaviour
{
    [SerializeField] List<Renderer> InvisibleChairs = new List<Renderer>();
    [SerializeField] Transform table;
    [SerializeField] float angle = 60;
    [SerializeField] GameObject winObject, winObject2;
    [SerializeField] Collider frontTrigger, rearTrigger, frontTrigger2, rearTrigger2;

    vp_FPPlayerEventHandler FPPlayer;
    Credits creditsPanel;

    float timer;
    bool solvedPuzzle, inTrigger;

    private void Start()
    {
        FPPlayer = FindObjectOfType<vp_FPPlayerEventHandler>();
        creditsPanel = FindObjectOfType<Credits>();
    }

    void Update ()
    {
        if (!solvedPuzzle && inTrigger)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) timer += Time.deltaTime;
            else timer = 0;

            Vector3 tableDirection = table.position - transform.position;

            if (Vector3.Angle(tableDirection, transform.forward) <= angle &&
                    FPPlayer.CurrentWeaponName.Get() == "2Lens" &&
                    (Input.GetMouseButton(0) || Input.GetMouseButton(1)) &&
                    timer > 2)
            {
                winObject.SetActive(true);
                winObject2.SetActive(true);
                solvedPuzzle = true;
                StartCoroutine(creditsPanel.FlashWhite());
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other == frontTrigger || other == rearTrigger || other == frontTrigger2 || other == rearTrigger2)
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == frontTrigger || other == rearTrigger || other == frontTrigger2 || other == rearTrigger2)
        {
            inTrigger = false;
        }
    }
}
