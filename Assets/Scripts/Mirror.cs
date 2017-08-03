using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour
{
    public Renderer mirrorDoor2Blocker, flashingSurface;
    public Color mirrorFlashColor = Color.white;
    public float flashTimer = 2;
    Camera cam;
    Color originalColorTop;
    Color originalColorBottom;
    Color originalMirrorMaterialColor;

    void Start()
    {
        cam = GetComponent<Camera>();
        originalColorTop = mirrorDoor2Blocker.material.GetColor("_TopColor");
        originalColorBottom = mirrorDoor2Blocker.material.GetColor("_BottomColor");

        cam.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) |
                    (1 << LayerMask.NameToLayer("UI")) | (1 << LayerMask.NameToLayer("Glass1")) |
                    (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) |
                    (1 << LayerMask.NameToLayer("Door02")) | (1 << LayerMask.NameToLayer("Door02Blocker")) |
                    (1 << LayerMask.NameToLayer("OldCourtyard")) | (1 << LayerMask.NameToLayer("NewChurch")) |
                    (1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("MirrorDoor1")) |
			(1 << LayerMask.NameToLayer("MirrorDoor2Blocker")) | (1 << LayerMask.NameToLayer("NotEdgy")));
    }

    public void PlayerIsOnFarSideOfTableOrLookingAway()
    {
        StopCoroutine(FadeMirrorImage());
        mirrorDoor2Blocker.material.SetColor("_TopColor", originalColorTop);
        mirrorDoor2Blocker.material.SetColor("_BottomColor", originalColorBottom);
        cam.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) |
                    (1 << LayerMask.NameToLayer("UI")) | (1 << LayerMask.NameToLayer("Glass1")) |
                    (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) |
                    (1 << LayerMask.NameToLayer("Door02")) | (1 << LayerMask.NameToLayer("Door02Blocker")) |
                    (1 << LayerMask.NameToLayer("OldCourtyard")) | (1 << LayerMask.NameToLayer("NewChurch")) |
                    (1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("MirrorDoor1")) |
			(1 << LayerMask.NameToLayer("MirrorDoor2Blocker")));
    }

    public void PlayerIsCloseAndLookingAtMirror()
    {
        StartCoroutine(Flash());
        cam.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) |
                    (1 << LayerMask.NameToLayer("UI")) | (1 << LayerMask.NameToLayer("Glass1")) |
                    (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) |
                    (1 << LayerMask.NameToLayer("Door02")) | (1 << LayerMask.NameToLayer("Door02Blocker")) |
                    (1 << LayerMask.NameToLayer("OldCourtyard")) | (1 << LayerMask.NameToLayer("NewChurch")) |
			(1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("MirrorDoor1")));
    }

    IEnumerator Flash()
    {
        flashingSurface.material.color = mirrorFlashColor;

        float elapsedTime = 0, timer = 1;
        while (elapsedTime < timer)
        {
            flashingSurface.material.color = Color.Lerp(mirrorFlashColor, originalMirrorMaterialColor, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        flashingSurface.material.color = originalMirrorMaterialColor;
    }

    IEnumerator FadeMirrorImage()
    {
        yield return new WaitForEndOfFrame();
        
        float elapsedTime = 0;
        while (elapsedTime < flashTimer)
        {
            mirrorDoor2Blocker.material.SetColor("_TopColor", Color.Lerp(originalColorTop, Color.clear, elapsedTime / flashTimer));
            mirrorDoor2Blocker.material.SetColor("_BottomColor", Color.Lerp(originalColorBottom, Color.clear, elapsedTime / flashTimer));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        mirrorDoor2Blocker.material.SetColor("_TopColor", Color.clear);
        mirrorDoor2Blocker.material.SetColor("_BottomColor", Color.clear);

        cam.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) |
                    (1 << LayerMask.NameToLayer("UI")) | (1 << LayerMask.NameToLayer("Glass1")) |
                    (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) |
                    (1 << LayerMask.NameToLayer("Door02")) | (1 << LayerMask.NameToLayer("Door02Blocker")) |
                    (1 << LayerMask.NameToLayer("OldCourtyard")) | (1 << LayerMask.NameToLayer("NewChurch")) |
			(1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("MirrorDoor1")));
    }
}
