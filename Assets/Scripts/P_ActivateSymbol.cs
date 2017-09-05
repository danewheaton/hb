using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_ActivateSymbol : MonoBehaviour
{
    bool inCourtyard;
    public bool InCourtyard { get { return inCourtyard; } }

    [SerializeField] GameObject mazeTeleporter;
    [SerializeField] Transform flamingo;
    [SerializeField] float stareTime = 3, flamingoAngle = 175;
    [SerializeField] Color clearWhite = new Color(1, 1, 1, 0), mirrorFlashColor = Color.white;
    [SerializeField] Renderer flashingSurface;

    vp_PlayerEventHandler FPPlayer;

    SpriteRenderer flamingoRenderer;
    Color originalColor;

    float timer;
    bool staredLongEnough;

    void Start()
    {
        FPPlayer = FindObjectOfType<vp_PlayerEventHandler>();
        flamingoRenderer = flamingo.GetComponent<SpriteRenderer>();
        mazeTeleporter.GetComponent<Collider>().enabled = false;
        originalColor = flamingoRenderer.color;
    }

    void Update()
    {
        Vector3 targetDirection = Camera.main.transform.position - flamingo.transform.position;

        if (Vector3.Angle(targetDirection, Camera.main.transform.forward) > flamingoAngle &&
            FPPlayer.CurrentWeaponName.Get() == "2Lens" && !staredLongEnough)
        {
            timer += Time.deltaTime;

            flamingoRenderer.color = Color.Lerp(flamingoRenderer.color, clearWhite, Time.deltaTime);

            if (timer >= stareTime && !staredLongEnough)
            {
                mazeTeleporter.GetComponent<Collider>().enabled = true;
                flamingoRenderer.color = clearWhite;
                StartCoroutine(FlashDoorway());
                staredLongEnough = true;
            }
        }
        //else if (staredLongEnough) flamingoRenderer.color = clearWhite;
        else
        {
            flamingoRenderer.color = Color.white;
            staredLongEnough = false;
            timer = 0;
        }
    }

    IEnumerator FlashDoorway()
    {
        flashingSurface.material.color = mirrorFlashColor;

        float elapsedTime = 0, timer = 1;
        while (elapsedTime < timer)
        {
            flashingSurface.material.color = Color.Lerp(mirrorFlashColor, clearWhite, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        flashingSurface.material.color = clearWhite;
    }
}
