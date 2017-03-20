using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]

public class RandomPanelImage : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    void Start()
    {
        GetComponent<Image>().sprite = sprites[Random.Range(0, sprites.Length)];
    }
}
